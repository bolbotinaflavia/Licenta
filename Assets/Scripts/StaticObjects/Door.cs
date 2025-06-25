using Inventory;
using Player;
using UnityEngine;

namespace StaticObjects
{
    public class Door:MonoBehaviour
    {
        public static Door Instance;
        [SerializeField]private Rigidbody2D doorBody;
        [SerializeField]private Animator doorAnimator;
        [SerializeField]private bool doorOpen;
        [SerializeField] private bool opened;
       [SerializeField] private Sprite sprite;
        public bool Opened { get => opened; set => opened = value; }

        public bool DoorOpen
        {
            get => doorOpen;
            set
            {
                doorOpen = value;
                if (doorAnimator != null)
                {
                    doorAnimator.SetBool("DoorOpen", value);
                }
                else
                {
                    Debug.LogError("Animator is missing! Make sure the Animator component is attached to the Door.");
                }
            }
        }

        private void Start()
        {
            if(Instance == null)
                Instance = this;
            doorBody = GetComponent<Rigidbody2D>();
            doorAnimator = GetComponent<Animator>();
            opened = false;
        }

        public void Open_Door()
        {
            if (InventoryManager.Instance.getArtefact("Key"))
            {
                StartCoroutine(PlayerManager.Instance.Notification.notification_show("You opened the door!!",2f));
                PlayerManager.Instance.IsMoving = false;
                DoorOpen = true;
                Invoke(nameof(stop_animation),2f);
            }
            else
            {
                PlayerManager.Instance.IsMoving = false;
                StartCoroutine(PlayerManager.Instance.Notification.notification_show("Door closed!!\n Hint: An enemy may give you the key...",2f));
                PlayerManager.Instance.player.transform.position=new Vector3(PlayerManager.Instance.player.transform.position.x-200f,PlayerManager.Instance.player.transform.position.y,0);
            }
        }

        private void stop_animation()
        {
            DoorOpen = false;
            opened = true;
            PlayerManager.Instance.player.transform.position=new Vector3(doorBody.transform.position.x+400f,PlayerManager.Instance.player.transform.position.y,0);
            PlayerManager.Instance.IsMoving = true;
            this.GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }
}