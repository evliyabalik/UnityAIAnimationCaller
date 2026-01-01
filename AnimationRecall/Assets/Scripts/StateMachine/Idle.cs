using UnityEngine;

public class Idle : MonoBehaviour, IState
{
    private readonly Caller _caller;

    public Idle(Caller caller) => _caller = caller;

    public void Enter() => _caller.m_signals = Signals.On_Idle;
  

    void IState.Update()
    {
        if (_caller.IsMoving())
            _caller.machine.ChangeState(_caller.On_Move);
    }

    public void Exit() { }
  

}
