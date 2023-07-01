namespace Scripts.Infrastructure.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }

    public interface IPayLoadedState <TPayload> : IExitableState
    {
        void Enter(TPayload payload);
        void Exit();
    }

    public interface IExitableState
    {
        void Exit();
    }
}