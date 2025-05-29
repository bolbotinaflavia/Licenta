using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using Notification = DefaultNamespace.Notification;

namespace Enemies
{
    public class MagicianDialogue:MonoBehaviour
    {
        public static MagicianDialogue Instance;
        [SerializeField] private Animator animator;
        [SerializeField] private Notification dialogue;
        [SerializeField] private GameObject fire_sword;

        public void Start()
        {
            if(Instance == null)
                Instance = this;
            animator = GetComponent<Animator>();
            StartCoroutine(speak_start());
        }
        private IEnumerator speak_start()
        {
            animator.SetBool("Speaking", true);
            StartCoroutine(dialogue.notification_show("You are an intruder in my forest, my monsters will destroy you!!",2f));
            yield return new WaitForSeconds(3f);
            StartCoroutine(
                dialogue.notification_show("Good luck in finding my castle and getting the fire sword!!", 2f));
            fire_sword.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("Gameplay");
        }
    }
}