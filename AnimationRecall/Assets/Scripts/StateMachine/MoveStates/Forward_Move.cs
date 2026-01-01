using UnityEngine;

public class Forward_Move : MonoBehaviour, IState
{
    private readonly Caller _caller;

    public Forward_Move(Caller caller) => _caller = caller;

    public void Enter() => _caller.m_signals = Signals.On_Move;


    void IState.Update()
    {
        //if (!Input.GetKey(KeyCode.W))
        //    _caller.machine.ChangeState(_caller.idleState);

        if (!_caller.IsMoving())
            _caller.machine.ChangeState(_caller.idleState);

    }

    public void Exit() { }
}
