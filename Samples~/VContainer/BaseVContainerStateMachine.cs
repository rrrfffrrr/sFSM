using VContainer;

namespace ProfTroller.sFSM
{
    public abstract class BaseVContainerStateMachine<TState> : BaseManagedStateMachine<TState> where TState : BaseState, new()
    {
        IObjectResolver dependencyInjector;

        [Inject]
        public void InjectDependency(Container container)
        {
            if (dependencyInjector != null)
            {
                return;
            }
            dependencyInjector = container.CreateScope();
        }
        public override void Dispose()
        {
            base.Dispose();
            dependencyInjector?.Dispose();
            dependencyInjector = null;
        }
        protected override T ActivateInternal<T>()
        {
            var state = System.Activator.CreateInstance<T>();
            dependencyInjector.Inject(state);
            return state;
        }
    }
}