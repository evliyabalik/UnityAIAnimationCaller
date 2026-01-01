using System.ComponentModel;
using UnityEngine;

public enum Signals
{
    [Category("Hareket")]
    On_Idle,

    [Category("Hareket")]
    On_Move,

    [Category("Hareket")]
    On_Jump,

    [Category("Saldýrý")]
    On_Attack,
    [Category("Saldýrý")]
    On_Defence

}

public class CallerSignalsHelper : MonoBehaviour
{

}
