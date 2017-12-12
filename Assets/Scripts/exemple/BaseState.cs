public abstract class BaseState 
{
   // public BaseState() { }    

    public abstract void Update();
    public abstract void TransitionIn();
    public abstract void TransitionOut();

    public abstract BaseAI CanTransition();
    public abstract BaseState NextState();
}
