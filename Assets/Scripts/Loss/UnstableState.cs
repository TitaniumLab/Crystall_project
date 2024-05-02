namespace CrystalProject.Loss
{
    public class UnstableState : State
    {
        public UnstableState(LossController lossController, LossStateMachine lossStateMachine) : base(lossController, lossStateMachine) { }

        public override void LogicUpdate()
        {
            if (_lossController.CurrentValue == 0)
                _stateMachine.ChangetState(_lossController.WaitingState);
            else if (_lossController.CurrentValue >= _lossController.LossDelay)
                _stateMachine.ChangetState(_lossController.LossState);
        }

        public override void BehaviorUpdate()
        {
            _lossController.ChangeValue();
        }
    }
}
