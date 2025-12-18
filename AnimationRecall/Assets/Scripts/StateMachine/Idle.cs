using UnityEngine;

public class Idle : MonoBehaviour, IState
{
    private readonly Caller _caller;

    public Idle(Caller caller) => _caller = caller;

    public void Enter() => _caller.m_signals = Signals.On_Idle;
  

    void IState.Update()
    {
        if (Input.GetKey(KeyCode.W))
            _caller.machine.ChangeState(_caller.forward_moveState);
        else if (Input.GetKey(KeyCode.A))
            _caller.machine.ChangeState(_caller.left_moveState);
        else if (Input.GetKey(KeyCode.D))
            _caller.machine.ChangeState(_caller.Right_moveState);
        else if (Input.GetKey(KeyCode.S))
            _caller.machine.ChangeState(_caller.backward_moveState);
    }

    public void Exit() { }
  

}
