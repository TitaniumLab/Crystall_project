using UnityEngine;
using UnityEngine.UI;

namespace CrystalProject.Loss
{
    public class LossController : MonoBehaviour
    {
        [field: SerializeField] public float LossDelayValue { get; private set; }
        [field: SerializeField] public float CurrentValue { get; private set; }
        [field: SerializeField] public float MinCurrentValue { get; private set; }
        [field: SerializeField] public float IncrementerValue { get; private set; }
        [field: SerializeField] public float MinIncrementerValue { get; private set; }
        [field: SerializeField] public float DecreasingValue { get; private set; } = 1;
        [field: SerializeField] public Slider Slider { get; private set; } //the slider will be replaced with a separate functionality-----------

        private LossStateMachine _stateMachine;
        public WaitingState WaitingState { get; private set; }
        public UnstableState UnstableState { get; private set; }
        public LossState LossState { get; private set; }

        #region MonoBeh
        private void Awake()
        {
            ILossSender.LossCountChanged += ChangeLossIncrementer;

            _stateMachine = new LossStateMachine();
            WaitingState = new WaitingState(this, _stateMachine);
            UnstableState = new UnstableState(this, _stateMachine);
            LossState = new LossState(this, _stateMachine);
            _stateMachine.Initialize(WaitingState);

            Slider.maxValue = LossDelayValue;
        }

        private void OnDestroy()
        {
            ILossSender.LossCountChanged -= ChangeLossIncrementer;
        }

        private void FixedUpdate()
        {
            _stateMachine.CurrentState.LogicUpdate();
            _stateMachine.CurrentState.BehaviorUpdate();
        }
        #endregion

        #region Methods
        private void ChangeLossIncrementer(float value) =>
            IncrementerValue += value;

        public void ChangeValue()
        {
            if (IncrementerValue > MinIncrementerValue)
                CurrentValue += Time.fixedDeltaTime * IncrementerValue;
            else
            {
                CurrentValue -= Time.fixedDeltaTime * DecreasingValue;
                if (CurrentValue < MinCurrentValue)
                    CurrentValue = MinCurrentValue;
            }
            Slider.value = CurrentValue;
        }

        public void TriggerLoss()
        {
            Time.timeScale = 0;
        }
        #endregion
    }
}