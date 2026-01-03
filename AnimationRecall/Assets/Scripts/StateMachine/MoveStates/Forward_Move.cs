using UnityEngine;
using UnityEngine.UIElements;

public class Forward_Move : MonoBehaviour, IState
{
    private readonly Caller _caller;

    public Forward_Move(Caller caller) => _caller = caller;

    public void Enter() { 
        _caller.m_signals = Signals.On_Move;
    }


    void IState.Update()
    {
        if (Input.GetMouseButton(0))
            OnMove();


        if (!_caller.IsMoving() || _caller.m_agent.isOnOffMeshLink)
            _caller.machine.ChangeState(_caller.idleState);

    }

    void OnMove()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f))
        {
            _caller.m_agent.SetDestination(hit.point);
        }
    }

    public void Exit() {  }
}
