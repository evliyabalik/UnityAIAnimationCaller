using UnityEngine;

public class Idle : MonoBehaviour, IState
{
    private readonly Caller _caller;

    public Idle(Caller caller) => _caller = caller;

    public void Enter() { if (!_caller.onChange) _caller.m_signals = Signals.On_Idle; }


    void IState.Update()
    {

        if (_caller.IsMoving() || Input.GetMouseButton(0) && !_caller.m_agent.isOnOffMeshLink)
            _caller.machine.ChangeState(_caller.On_Move);

        if(_caller.m_agent.isOnOffMeshLink)
            _caller.machine.ChangeState(_caller.Jump_State);


    }

    public void Exit() { }


}
