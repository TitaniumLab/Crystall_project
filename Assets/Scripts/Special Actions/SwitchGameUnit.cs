using CrystalProject.Dropper;
using CrystalProject.EventBus;
using CrystalProject.EventBus.Signals;
using CrystalProject.Score;
using CrystalProject.UI;
using UnityEngine;
using Zenject;

namespace CrystalProject.SpecialActions
{
    public class SwitchGameUnit : MonoBehaviour
    {
        [SerializeField] private DropController _dropController;
        [SerializeField] private DropAnimator _dropAnimator;
        [SerializeField] private UISpecialActions _uIActions;
        [SerializeField] private int _currentAttempt = 0;
        [SerializeField] private int _maxAttempts = 4;

        private int _currentTier;
        private CustomEventBus _customEventBus;


        #region Internal
        [Inject]
        private void Construct(CustomEventBus customEventBus, IScore score)
        {
            _customEventBus = customEventBus;
        }

        private void Awake()
        {
            _customEventBus.Subscribe<ScoreThresholdSignal>(OnScoreThresholdReach);
            _dropAnimator.OnAppearAnimating += _uIActions.InteractableCencelButton;
        }

        private void Start()
        {
            _uIActions.InteractableSwitchButton(false);
            SetAttemptsText();
            SetAttemptsText();
        }

        private void OnDestroy()
        {
            _customEventBus.Unsubscribe<ScoreThresholdSignal>(OnScoreThresholdReach);
            _dropAnimator.OnAppearAnimating -= _uIActions.InteractableCencelButton;
        }
        #endregion

        #region Switch
        public void EnableSwitchState(bool isEnabled)
        {
            _uIActions.EnableMainUIButtons(!isEnabled);
            _uIActions.EnableOkButton(isEnabled, SwitchAccept);
            _uIActions.EnableCencelButton(isEnabled, OnSwitchCencel);
            _uIActions.EnableSwitchArrowButtons(isEnabled);
            _dropController.CanDrop = !isEnabled;
            if (isEnabled)
            {
                ShowAd.ShowAdWithChance();
                _currentTier = _dropController.GetCurrentTier();
            }
        }


        private void OnSwitchCencel()
        {
            _dropController.PoolCurrentUnit();
            _dropController.SetUnitOfTier(_currentTier);
            EnableSwitchState(false);
        }


        private void SwitchAccept()
        {
            _currentAttempt++;
            SetAttemptsText();
            EnableSwitchState(false);
        }


        private void OnScoreThresholdReach(ScoreThresholdSignal signal)
        {
            if (_dropController.GetCurrentUnitTiers().Length > 1 && _currentAttempt < _maxAttempts)
            {
                _uIActions.InteractableSwitchButton(true);
            }
        }


        private void SetAttemptsText()
        {
            _uIActions.SetSwitchAttemptsText(_currentAttempt, _maxAttempts);
            if (_currentAttempt >= _maxAttempts)
            {
                _uIActions.InteractableSwitchButton(false);
            }
        }
        #endregion
    }
}