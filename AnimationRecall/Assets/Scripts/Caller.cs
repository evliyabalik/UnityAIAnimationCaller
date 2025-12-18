using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;

public class Caller : MonoBehaviour
{
    public AnimationsSO[] animationSO;
    public StateMachine machine;

    Animator m_anim;
    NavMeshAgent m_agent;
    AnimationsSO currentSO;
    

    public Signals m_signals;

    #region States
    [Header("States")]
    [HideInInspector] public Idle idleState;
    [HideInInspector] public Forward_Move forward_moveState;
    [HideInInspector] public Backward_Move backward_moveState;
    [HideInInspector] public Left_Move left_moveState;
    [HideInInspector] public Right_Move Right_moveState;
    #endregion

    private void Awake()
    {
        machine = GetComponent<StateMachine>();
        DescribeStuation();


        machine.ChangeState(idleState);
    }

    private void Start()
    {
        m_anim = GetComponent<Animator>();
        m_agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        IsSignal();
    }


    void IsSignal()
    {
        AnimationController.instance.PlayAnimation(m_signals, m_anim, ref currentSO, animationSO);

    }

    void DescribeStuation()
    {
        idleState = new Idle(this);
        forward_moveState = new Forward_Move(this);
        backward_moveState = new Backward_Move(this);
        left_moveState = new Left_Move(this);
        Right_moveState = new Right_Move(this);
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
