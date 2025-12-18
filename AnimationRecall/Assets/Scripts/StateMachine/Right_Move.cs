using UnityEngine;

public class Right_Move : MonoBehaviour, IState
{
    private readonly Caller _caller;

    public Right_Move(Caller caller) => _caller = caller;

    public void Enter() => _caller.m_signals = Signals.On_Right;


    void IState.Update()
    {
        if (!Input.GetKey(KeyCode.D))
            _caller.machine.ChangeState(_caller.idleState);

    }

    public void Exit() { }
}
