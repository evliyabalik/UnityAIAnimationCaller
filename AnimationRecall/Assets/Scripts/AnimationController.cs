using UnityEditor.Animations;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public static AnimationController instance;
    Animation anima;
    bool isRunning=true;

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
        else
            Destroy(instance);

        DontDestroyOnLoad(gameObject);
    }

    public void PlayAnimation(Signals signal, Animator anim, AnimationsSO animationsSO)
    {

        if (signal == animationsSO.signals)
        {
            anim.CrossFade(animationsSO.clip.name,.25f);
            
        }

       
    }

  
}
