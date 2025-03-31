using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Anim_Sound_Manager : StateMachineBehaviour
{
    private Animator animator;
    public string name;

    public AudioSource audio;

    public AudioSource audio_idle;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        audio_idle=GameObject.Find("idle_song").GetComponent<AudioSource>();
        audio=GameObject.Find(name).GetComponent<AudioSource>();
        audio_idle.Stop();
        audio.Play();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

     //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        audio.Stop();
        
        audio_idle.Play();
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
