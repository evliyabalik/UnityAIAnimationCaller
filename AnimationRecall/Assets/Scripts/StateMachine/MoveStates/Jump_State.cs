using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Jump_State : MonoBehaviour, IState
{
    Rigidbody m_rigid;
    Collider m_col;

    private readonly Caller _caller;

    public Jump_State(Caller caller) => _caller = caller;

    public void Enter()
    {
        _caller.m_signals = Signals.On_Jump;
        if (m_rigid == null) m_rigid = _caller.m_agent.GetComponent<Rigidbody>();
        if (m_col == null) m_col = _caller.m_agent.GetComponent<Collider>();
        _caller.onChange = true;
    }


    void IState.Update()
    {
        if (_caller.m_agent.isOnOffMeshLink)
        {
            _caller.StartCoroutine(On_Jump(_caller.m_agent.currentOffMeshLinkData));
        }


        if (!_caller.IsMoving())
            _caller.machine.ChangeState(_caller.idleState);

    }

    public void Exit() { }


    IEnumerator On_Jump(OffMeshLinkData data)
    {
        Vector3 corner = _caller.m_agent.path.corners[Mathf.Min(_caller.m_agent.path.corners.Length - 1, 1)];
        _caller.m_agent.enabled = false;        // NavMesh'i b�rak
        m_rigid.isKinematic = false;
        m_col.enabled = true;

        Vector3 start = data.startPos;
        Vector3 end = data.endPos;
        Vector3 dir = (end - start).normalized;
        float dist = Vector3.Distance(start, end);
        float g = Physics.gravity.magnitude;
        float h = end.y - start.y;
        float v0 = Mathf.Sqrt(g * dist * dist / (2 * (dist * Mathf.Tan(45 * Mathf.Deg2Rad) - h)));

        Vector3 vel = dir * dist + Vector3.up * v0;
        m_rigid.linearVelocity = vel;
        
        // z�plama bitene bekle
        yield return new WaitUntil(() => m_rigid.linearVelocity.y <= 0 && m_rigid.transform.position.y <= end.y + 0.1f);

       

        m_rigid.isKinematic = true;
        _caller.m_agent.enabled = true;
        m_col.enabled = false;
        _caller.m_agent.CompleteOffMeshLink(); // link'i kapat
        _caller.m_agent.SetDestination(corner);
        _caller.onChange = false;

        yield return null;

    }


}
