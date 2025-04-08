using UnityEngine;

namespace Battle
{
    public class HpBarAnimation: MonoBehaviour
    {
        private static readonly int Damaged = Animator.StringToHash("isDamaged");
        private static readonly int Healing = Animator.StringToHash("isHealing");
        public Animator animator;
        private bool _isDamaged;

        
        public bool IsDamaged
        {
            get => _isDamaged;
            set
            {
                _isDamaged = value;
                if (animator != null)
                {
                    animator.SetBool(Damaged, value);
              
                }
                else
                {
                    Debug.LogError("Animator is missing! Make sure the Animator component is attached to the Player.");
                }
            }
        }

        private bool _isHealing;

        public bool IsHealing
        {
            get => _isHealing;
            set
            {
                _isHealing = value;
                if (animator != null)
                {
                    animator.SetBool(Healing, value);
                
                }
                else
                {
                    Debug.LogError("Animator is missing! Make sure the Animator component is attached to the Player.");
                }
            }
        }

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void healing_animation()
        {
            Debug.Log("Healing anaimation started");
          //  animator.PlayInFixedTime(animator.GetCurrentAnimatorStateInfo(0).fullPathHash,0,4);
           // new WaitForSeconds(4);
           Invoke(nameof(end_animation), 2f);
            //IsMoving = true;
        
        }

        public void damaging_animation()
        {
            IsDamaged = true;
          // animator.PlayInFixedTime(animator.GetCurrentAnimatorStateInfo(0).fullPathHash,0,4);
          // new WaitForSeconds(4);
          //  Debug.Log("damage animation started");
          Invoke(nameof(end_animation), 5f);
     
        }

        public void end_animation()
        {
            IsDamaged = false;
            IsHealing = false;
        }
        
        
    }
}