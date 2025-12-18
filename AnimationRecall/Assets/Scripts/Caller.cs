using UnityEngine;
using UnityEngine.AI;

public class Caller : MonoBehaviour
{
    public AnimationsSO[] animationSO;
    Animator m_anim;
    NavMeshAgent m_agent;
    AnimationsSO currentSO;

    Signals m_signals;

    private void Start()
    {
        m_anim = GetComponent<Animator>();
        m_agent = GetComponent<NavMeshAgent>();

        m_signals = Signals.On_Idle;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
            print(m_signals = Signals.On_Forward);
        else if (Input.GetKey(KeyCode.A))
            print(m_signals = Signals.On_Right);
        else if (Input.GetKey(KeyCode.D))
            print(m_signals = Signals.On_Left);
        else if (Input.GetKey(KeyCode.S))
            print(m_signals = Signals.On_Backward);
        else
            print(m_signals = Signals.On_Idle);
        
        
        IsSignal();

    }


    void IsSignal()
    {
        AnimationsSO an = System.Array.Find(animationSO, x => x.signals == m_signals);
        if(an!=null && an != currentSO)
        {
            currentSO = an;
            m_anim.CrossFade(an.clip.name, .25f);
        }
        
    }

    //public bool IsMoving()
    //{
    //    /*  Henüz yol hesaplanmadýysa "hareket etmiyor" say */
    //    if (m_agent.pathPending) return false;

    //    /*  Kullanýcý veya kod durdurduysa */
    //    if (m_agent.isStopped) return false;

    //    /*  Kalan mesafe < durma toleransý  =>  hedefe vardý */
    //    const float stopTreshold = 0.1f;
    //    return m_agent.hasPath && m_agent.remainingDistance > stopTreshold;
    //}
}
