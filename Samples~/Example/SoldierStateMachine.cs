using UnityEngine;

namespace ProfTroller.sFSM.Example
{
    public class Soldier : MonoBehaviour
    {
        readonly SoldierStateMachine StateMachine = new();
        bool seenEnemy;

        private void Awake()
        {
            StateMachine.Initialize();
        }

        private void Update()
        {
            UpdateEye();
        }

        private void OnDestroy()
        {
            StateMachine.Dispose();
        }

        private void UpdateEye()
        {
            if (Random.value > 0.1f)
            {
                if (seenEnemy)
                {
                    seenEnemy = false;
                    StateMachine.OnEnemyDisapear();
                }
            } else
            {
                if (!seenEnemy)
                {
                    seenEnemy = true;
                    StateMachine.OnContactEnemy("Some enemy");
                }
            }
        }
    }

    public interface IStateEvent
    {
        void OnContactEnemy(string enemyName);
        void OnEnemyDisapear();
    }
    public class SoldierStateMachine : ManagedStateMachine<SoldierStateMachine.BaseSoldierState>, IStateEvent
    {
        public void Initialize()
        {
            ChangeState<AwaitingState>();
        }

        protected override T CreateState<T>()
        {
            var state = base.CreateState<T>();
            if (state is BaseSoldierState stateT)
            {
                stateT.StateMachine = this;
            }
            return state;
        }

        public virtual void OnContactEnemy(string enemyName) {
            CurrentState?.OnContactEnemy(enemyName);
        }
        public virtual void OnEnemyDisapear() {
            CurrentState?.OnEnemyDisapear();
        }

        #region States
        public abstract class BaseSoldierState : IState, IStateEvent
        {
            public SoldierStateMachine StateMachine;

            public virtual void Dispose() { }

            public abstract void Enter();
            public abstract void Exit();

            public virtual void OnContactEnemy(string enemyName) { }
            public virtual void OnEnemyDisapear() { }
        }

        private class AwaitingState : BaseSoldierState
        {
            public override void Enter()
            {
                Debug.Log("I'm awaiting!");
            }
            public override void Exit()
            {
            }
            public override void OnContactEnemy(string enemyName)
            {
                base.OnContactEnemy(enemyName);
                StateMachine.ChangeState<AttackState>();
            }
        }
        private class AttackState : BaseSoldierState
        {
            public override void Enter()
            {
                Debug.Log("I'm attacking!");
            }
            public override void Exit()
            {
            }
            public override void OnEnemyDisapear()
            {
                base.OnEnemyDisapear();
                StateMachine.ChangeState<AwaitingState>();
            }
        }
        #endregion
    }
}