namespace CrystalProject.Loss
{
    public class State
    {
        protected LossController _lossController;
        protected LossStateMachine _stateMachine;

        protected State(LossController lossController, LossStateMachine machine)
        {
            _lossController = lossController;
            _stateMachine = machine;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void LogicUpdate() { }
        public virtual void BehaviorUpdate() { }
    }
}

