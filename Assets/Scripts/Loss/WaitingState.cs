namespace CrystalProject.Loss
{
    public class WaitingState : State
    {
        public WaitingState(LossController lossController, LossStateMachine lossStateMachine) : base(lossController, lossStateMachine) { }

        public override void LogicUpdate()
        {
            if (_lossController.IncreasingValue != 0)
                _stateMachine.ChangetState(_lossController.UnstableState);
        }
    }
}

