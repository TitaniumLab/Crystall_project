namespace CrystalProject.Loss
{
    public class LossState : State
    {
        public LossState(LossController lossController, LossStateMachine lossStateMachine) : base(lossController, lossStateMachine) { }

        public override void Enter() =>
            _lossController.TriggerLoss();
    }
}
