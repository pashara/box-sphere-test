
namespace ThirdParty.StateMachine.States
{
    public interface IStatePayload
    {
    }
    
    public interface IPayloadableState<T> where T : IStatePayload
    {
        void Configure(T payload);
    }
    public interface IState
    {
        void Enter();
        void Exit();
    }
    
    public interface IProcessableState
    {
        void Process();
    }

    public interface IStateMachine<T> where T : IState
    {
        void Enter<T>();
    }

    public interface IStateMachineTickable<T> where T : IState
    {
        void Tick();
    }
}
