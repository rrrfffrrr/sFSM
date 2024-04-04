using VContainer;

namespace ProfTroller.sFSM.VContainer
{
    public class StateMachine<TState> : ManagedStateMachine<TState> where TState : BaseState<TState>
    {
        private IScopedObjectResolver scope;

        [Inject]
        public void InjectDependency(Container container)
        {
            if (scope != null)
            {
                return;
            }
            scope = container.CreateScope(BuildDependency);
            void BuildDependency(IContainerBuilder builder)
            {
                builder.RegisterInstance(this).As<StateMachine<TState>>();
            }
        }

        protected override T CreateState<T>()
        {
            var state = base.CreateState<T>();
            scope.Inject(state);
            return state;
        }

        public override void Dispose()
        {
            base.Dispose();
            scope?.Dispose();
            scope = null;
        }
    }
}
