using System.ComponentModel;
using UnityEngine;

public enum Signals
{
    [Category("Hareket")]
    On_Idle,
    [Category("Hareket")]
    On_Forward,
    [Category("Hareket")]
    On_Backward,
    [Category("Hareket")]
    On_Left,
    [Category("Hareket")]
    On_Right,
    [Category("Hareket")]
    On_Up,
    [Category("Hareket")]
    On_Down,

    [Category("Saldýrý")]
    On_Attack,
    [Category("Saldýrý")]
    On_RightAttack,
    [Category("Saldýrý")]
    On_LeftAttack,

}

public class CallerSignalsHelper: MonoBehaviour
{
   
}
