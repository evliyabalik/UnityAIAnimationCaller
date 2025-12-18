using UnityEditor.Animations;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public static AnimationController instance;


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

    public void PlayAnimation(Signals signal, Animator anim, ref AnimationsSO currentSO , AnimationsSO[] animationsSO)
    {
        AnimationsSO an = FindAnimation(animationsSO, signal);
        print(an);

        if (an != null && an != currentSO)
        {
            currentSO = an;
            anim.CrossFade(an.clip.name, .1f);
        }
    }

    public AnimationsSO FindAnimation(AnimationsSO[] animationSO, Signals signal)
    {
         return System.Array.Find(animationSO, x => x.signals == signal);

    }

  
}
