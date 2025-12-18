using UnityEngine;

public class Backward_Move : MonoBehaviour, IState
{
    private readonly Caller _caller;

    public Backward_Move(Caller caller) => _caller = caller;

    public void Enter() => _caller.m_signals = Signals.On_Backward;


    void IState.Update()
    {
        print("Backward çalýþtý");
        if (!Input.GetKey(KeyCode.S))
            _caller.machine.ChangeState(_caller.idleState);

    }

    public void Exit() { }
}
