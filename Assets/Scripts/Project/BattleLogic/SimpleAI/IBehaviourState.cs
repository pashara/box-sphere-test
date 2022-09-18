using System;

namespace Project.BattleLogic.SimpleAI
{
    public interface IBehaviourCheckTrigger : IDisposable
    {
        bool ShouldProcessState();
    }
    
    public interface IEnterState : IState
    {
        void Enter();
        void Exit();
    }
    public interface IState
    {
    }

    public interface IParallelState : IState
    {
        bool Process(float deltaTime);
    }
    
    public interface IExecutableState : IState
    {
        bool Process(float deltaTime);
    }
    
    public interface IGizmosableState : IState
    {
        void OnDrawGizmos();
    }

    public interface INotInterruptableBehaviour
    {
    }

    public interface IBehaviourState : IState, IDisposable
    {
    }
}