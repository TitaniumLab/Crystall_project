using CrystalProject.Loss;
namespace CrystalProject.Loss
{
    public class LossStateMachine
    {
        public State CurrentState { get; private set; }

        public void Initialize(State state)
        {
            CurrentState = state;
            CurrentState.Enter();
        }

        public void ChangetState(State state)
        {
            CurrentState.Exit();
            Initialize(state);
        }
    }
}