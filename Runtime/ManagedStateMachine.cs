using System;
using System.Collections.Generic;

namespace ProfTroller.sFSM
{
    public class ManagedStateMachine<TState> : StateMachine<TState> where TState : class, IState
    {
        readonly Dictionary<Type, TState> states = new();
        protected override IEnumerable<TState> States => states.Values;

        protected override T GetState<T>()
        {
            if (!states.TryGetValue(typeof(T), out var state) || state is not T stateT)
            {
                stateT = CreateState<T>();
                states[typeof(T)] = stateT;
            }
            return stateT;
        }
        protected virtual T CreateState<T>() where T : IState
        {
            return Activator.CreateInstance<T>();
        }

        public override void Dispose()
        {
            base.Dispose();
            foreach (var state in states)
            {
                state.Value.Dispose();
            }
            states.Clear();
        }
    }
}
