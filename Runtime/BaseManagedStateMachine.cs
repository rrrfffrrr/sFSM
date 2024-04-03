using System;
using System.Collections.Generic;

namespace ProfTroller.sFSM
{
    public abstract class BaseManagedStateMachine<TState> : BaseStateMachine<TState> where TState : BaseState
    {
        readonly Dictionary<Type, TState> states = new();
        protected IEnumerable<TState> States
        {
            get
            {
                foreach(var state in states)
                {
                    yield return state.Value;
                }
            }
        }

        public override void ChangeState<T>()
        {
            if (!states.TryGetValue(typeof(T), out var state))
            {
                state = ActivateInternal<T>();
                states[typeof(T)] = state;
            }
            ChangeStateInternal(state);
        }
        protected abstract T ActivateInternal<T>() where T : TState;

        public override void Dispose()
        {
            foreach(var state in states)
            {
                state.Value.Dispose();
            }
            states.Clear();
        }
    }
}