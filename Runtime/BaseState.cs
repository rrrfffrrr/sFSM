namespace ProfTroller.sFSM
{
    public abstract class BaseState
    {
        protected abstract BaseStateMachine<BaseState> stateMachine { get; }

        public abstract void Dispose();
        public abstract void Enter();
        public abstract void Exit();
    }
}