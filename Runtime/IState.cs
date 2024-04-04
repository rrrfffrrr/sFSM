using System;

namespace ProfTroller.sFSM
{
    public interface IState : IDisposable {
        void Enter();
        void Exit();
    }
}