using UnityEngine;
using UnityEngine.UI;

namespace CrystalProject.Loss
{
    public class LossController : MonoBehaviour
    {
        [field: SerializeField] public float LossDelay { get; private set; }
        [field: SerializeField] public float CurrentValue { get; private set; }
        [field: SerializeField] public float IncreasingValue { get; private set; }
        [field: SerializeField] public float DecreasingValue { get; private set; } = 1;
        [field: SerializeField] public Slider Slider { get; private set; } //the slider will be replaced with a separate functionality-----------

        private LossStateMachine _stateMachine;
        public WaitingState WaitingState { get; private set; }
        public UnstableState UnstableState { get; private set; }
        public LossState LossState { get; private set; }


        private void Awake()
        {
            ILossSender.LossCountChanged += ChangeLossIncrementer;

            _stateMachine = new LossStateMachine();
            WaitingState = new WaitingState(this, _stateMachine);
            UnstableState = new UnstableState(this, _stateMachine);
            LossState = new LossState(this, _stateMachine);
            _stateMachine.Initialize(WaitingState);

            Slider.maxValue = LossDelay;
        }

        private void FixedUpdate()
        {
            _stateMachine.CurrentState.LogicUpdate();
            _stateMachine.CurrentState.BehaviorUpdate();
        }

        private void ChangeLossIncrementer(float value) =>
            IncreasingValue += value;

        public void ChangeValue()
        {
            if (IncreasingValue > 0)
                CurrentValue += Time.fixedDeltaTime * IncreasingValue;
            else
            {
                CurrentValue -= Time.fixedDeltaTime * DecreasingValue;
                if (CurrentValue < 0)
                    CurrentValue = 0;
            }
            Slider.value = CurrentValue;
        }

        public void TriggerLoss()
        {
            Time.timeScale = 0;
        }
    }
}