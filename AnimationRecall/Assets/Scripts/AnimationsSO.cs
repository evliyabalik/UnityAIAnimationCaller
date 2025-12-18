using UnityEngine;

[CreateAssetMenu(fileName = "New Animation SO", menuName = "Scriptable Objects/AnimationsSO")]
public class AnimationsSO : ScriptableObject
{
    public AnimationClip clip;
    public Signals signals;
}
