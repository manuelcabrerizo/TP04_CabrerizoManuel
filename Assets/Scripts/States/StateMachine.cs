using System.Collections.Generic;

public class StateMachine
{
    private Stack<IState> _states;

    public StateMachine()
    {
        _states = new Stack<IState>();
    }

    public IState PeekState()
    {
       return _states.Peek();
    }

    public bool IsEmpty()
    {
        return _states.Count == 0;
    }

    public void PushState(IState state)
    {
        state.Enter();
        _states.Push(state);
    }

    public void PopState()
    {
        IState state = _states.Pop();
        if (state != null)
        {
            state.Exit();
        }
    }

    public void ChangeState(IState state)
    {
        PopState();
        PushState(state);
    }

    public void Clear()
    {
        while (_states.Count > 0)
        {
            PopState();
        }
    }

    public void Update(float dt)
    {
        IState currentState = _states.Peek();
        if (currentState != null)
        {
            currentState.Process(dt);
        }
    }

}
