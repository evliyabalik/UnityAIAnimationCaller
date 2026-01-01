using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AiController : MonoBehaviour
{
    NavMeshAgent m_agent;

    Rigidbody m_rigid;
    Collider m_col;

    Vector3 m_position;
    Vector3 m_currentPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_rigid = GetComponent<Rigidbody>();
        m_col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_agent.isOnOffMeshLink)
        {
            StartCoroutine(On_Jump(m_agent.currentOffMeshLinkData));
            m_currentPos = transform.position;
            return;
        }

       

        if (Input.GetMouseButton(0) && !m_agent.isOnOffMeshLink)
        { 

            RaycastHit hit;
            
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit, 100f))
            {
                m_position = hit.point;
                m_agent.SetDestination(m_position);
            }
           
        }
    }

    IEnumerator On_Jump(OffMeshLinkData data)
    {
        m_agent.enabled = false;        // NavMesh'i b�rak
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
        m_agent.enabled = true;
        m_col.enabled = false;
        m_agent.CompleteOffMeshLink(); // link'i kapat
        m_agent.SetDestination(m_position);

    }
}
