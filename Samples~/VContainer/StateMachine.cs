using VContainer;
using VContainer.Unity;

namespace ProfTroller.sFSM.VContainer
{
    public class StateMachine<TState> : ManagedStateMachine<TState> where TState : BaseState<TState>
    {
        private LifetimeScope scope;

        [Inject]
        public void InjectDependency(LifetimeScope parent)
        {
            if (scope != null)
            {
                return;
            }
            scope = parent.CreateChild(BuildDependency);
            void BuildDependency(IContainerBuilder builder)
            {
                builder.RegisterInstance(this).As<StateMachine<TState>>();
            }
        }

        protected override T CreateState<T>()
        {
            var state = base.CreateState<T>();
            scope.Container.Inject(state);
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
