using VContainer;

namespace ProfTroller.sFSM.VContainer
{
    public abstract class BaseState<TState> : IState where TState : BaseState<TState>
    {
        [Inject]
        private StateMachine<TState> stateMachine;
        public void ChangeState<T>() where T : TState => stateMachine.ChangeState<T>();

        public virtual void Dispose() {}
        public abstract void Enter();
        public abstract void Exit();
    }
}
