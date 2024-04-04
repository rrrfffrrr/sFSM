using System.Collections.Generic;

namespace ProfTroller.sFSM
{
    public abstract class StateMachine<TState> where TState : class, IState
    {
        private TState currentState;
        protected TState CurrentState => currentState;
        protected abstract IEnumerable<TState> States { get; }

        protected abstract T GetState<T>() where T : TState;
        private void ChangeStateInternal(TState state)
        {
            currentState?.Exit();
            currentState = state;
            currentState?.Enter();
        }
        public void ChangeState<T>() where T : TState
        {
            var state = GetState<T>();
            ChangeStateInternal(state);
        }

        public void Dispose(bool exitState)
        {
            if (exitState)
            {
                ChangeStateInternal(null);
            }
            Dispose();
        }
        public virtual void Dispose() { }
    }
}