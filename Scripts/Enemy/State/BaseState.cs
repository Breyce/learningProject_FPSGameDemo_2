public abstract class BaseState
{
    public Enemy enemy;
    public StateMechine stateMechine;

    public abstract void Enter();
    public abstract void Perform();
    public abstract void Exit();
}
