
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    public class Enemy:MonoBehaviour
    {
        [SerializeField] private EnemieBase enemieBase;
        public EnemieBase EnemieBase => enemieBase;
        [SerializeField] private GameObject enemy;
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject insideObject;
        public GameObject InsideObject
        {
            get { return insideObject; }
        }
        public Animator Animator { get; set; }
        private static readonly int Idle = Animator.StringToHash("isIdle");

        private void Awake()
        {
            insideObject.SetActive(false);
            Animator = GetComponent<Animator>();
            
            Animator.runtimeAnimatorController = enemieBase.Animator;
            if (Animator != null)
            {
                Animator.SetBool(Idle, true);
            }
        }

        private void Update()
        {
            StartCoroutine(move_left());
            StartCoroutine(move_right());
        }

        private IEnumerator move_left()
        {
            var i = 0;
            while (i < 10)
            {
                enemy.transform.position= Vector3.MoveTowards(
                    enemy.transform.position,
                    new Vector2(enemy.transform.position.x+i*10,enemy.transform.position.y), Time.deltaTime * 50f);
                new WaitForSeconds(1f);
                i++;
            }

            yield return new WaitForSeconds(2f);
        }

        private IEnumerator move_right()
        {
            var i = 0;
            while (i < 10)
            {
                enemy.transform.position= Vector3.MoveTowards(
                    enemy.transform.position,
                    new Vector2(enemy.transform.position.x-i*10,enemy.transform.position.y), Time.deltaTime * 50f);
                new WaitForSeconds(1f);
                i++;
            }
            yield return new WaitForSeconds(2f);
        }

     
    }
}