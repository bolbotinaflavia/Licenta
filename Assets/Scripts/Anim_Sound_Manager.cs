using UnityEngine;
using UnityEngine.Serialization;

public class AnimSoundManager : StateMachineBehaviour
{
    private Animator _animator;
    [FormerlySerializedAs("name")] public string animationName;

    public AudioSource audio;

    [FormerlySerializedAs("audio_idle")] public AudioSource audioIdle;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
    {
        audioIdle=GameObject.Find("idle_song").GetComponent<AudioSource>();
        audio=GameObject.Find(animationName).GetComponent<AudioSource>();
        audioIdle.Stop();
        audio.Play();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

     //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
    {
        audio.Stop();
        
        audioIdle.Play();
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    // override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     audio.Stop();
    //     audio_idle.Play();
    // }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
