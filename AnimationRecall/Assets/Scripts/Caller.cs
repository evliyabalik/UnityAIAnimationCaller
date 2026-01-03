using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;

public class Caller : MonoBehaviour
{
    public AnimationsSO[] animationSO;
    public StateMachine machine;

    Animator m_anim;
    public NavMeshAgent m_agent;
    AnimationsSO currentSO;


    public Signals m_signals;
    public bool onChange = false;

    #region States
    [Header("States")]
    [HideInInspector] public Idle idleState;
    [HideInInspector] public Forward_Move On_Move;
    [HideInInspector] public Jump_State Jump_State;

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

    #region States Method
    void DescribeStuation()
    {
        idleState = new Idle(this);
        On_Move=new Forward_Move(this);
        Jump_State=new Jump_State(this);
    }

    #endregion

    public bool IsMoving()
    {
        return m_agent.velocity != Vector3.zero;
    }
}
