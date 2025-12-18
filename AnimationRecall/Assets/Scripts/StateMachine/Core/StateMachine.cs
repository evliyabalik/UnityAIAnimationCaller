using UnityEngine;

public class StateMachine : MonoBehaviour
{
    IState currentState;

    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    // Update is called once per frame
    void Update() => currentState?.Update();

}
