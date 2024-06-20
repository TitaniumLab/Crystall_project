using CrystalProject.EventBus;
using CrystalProject.EventBus.Signals;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CrystalProject.Loss
{
    public class GameOverRectAnim : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private float _startAnimSize = 0;
        [SerializeField] private float _animDuration = 0.5f;
        private CustomEventBus _customEventBus;

        [Inject]
        private void Construct(CustomEventBus customEventBus)
        {
            _customEventBus = customEventBus;
        }

        private void Awake()
        {
            _customEventBus.Subscribe<GameOverSignal>(OnGameOver);
        }

        private void OnDestroy()
        {
            _customEventBus.Unsubscribe<GameOverSignal>(OnGameOver);
        }

        private void OnGameOver(GameOverSignal gameOverSignal)
        {
            PlayBounceOutAnim();
        }

        private void PlayBounceOutAnim()
        {
            var defaultSize = _rectTransform.localScale.x;
            _rectTransform.localScale = new Vector3(_startAnimSize, _startAnimSize, _startAnimSize);
            _rectTransform.DOScale(defaultSize, _animDuration).SetEase(Ease.InOutBounce);
        }
    }
}

