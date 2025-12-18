using UnityEngine;

public class Left_Move : MonoBehaviour, IState
{
    private readonly Caller _caller;

    public Left_Move(Caller caller) => _caller = caller;

    public void Enter() => _caller.m_signals = Signals.On_Left;


    void IState.Update()
    {
        if (!Input.GetKey(KeyCode.A))
            _caller.machine.ChangeState(_caller.idleState);

    }

    public void Exit() { }
}
