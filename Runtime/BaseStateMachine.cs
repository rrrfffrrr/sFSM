namespace ProfTroller.sFSM
{
    public abstract class BaseStateMachine<TState> where TState : BaseState
    {
        protected TState currentState { get; private set; }

        protected void ChangeStateInternal(TState state)
        {
            currentState?.Exit();
            currentState = state;
            currentState?.Enter();
        }
        public abstract void ChangeState<T>() where T : TState;

        public void Dispose(bool exitState)
        {
            if (exitState)
            {
                ChangeStateInternal(null);
            }
            Dispose();
        }
        public abstract void Dispose();
    }
}