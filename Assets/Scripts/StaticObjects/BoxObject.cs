using Inventory;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using Weapons;

namespace StaticObjects
{
    public class Box : MonoBehaviour
    {
        public static Box Instance;
        [FormerlySerializedAs("inside_object")] public GameObject insideObject;
        public Sprite sprite;
        public bool opened;

        public GameObject player;
        //de vazut cu animatia;
        //cometariii

        public void open_box()
        {
            if (opened == false)
            {
                opened = true;
               // this.GetComponent<SpriteRenderer>().sprite = sprite;
                UpdateUI();
                insideObject.SetActive(true);
                //StartCoroutine(PlayerManager.Instance.Notification.notification_show("Opening Treasure Chest\n",2f));
                InventoryManager.Instance.add_object(insideObject);
                PlayerManager.Instance.IsMoving = true;
            }
        }

        private void Awake()
        {
            Instance = this;
            if (opened)
            {
                insideObject.SetActive(true);
                this.GetComponent<SpriteRenderer>().sprite = sprite;
                PlayerManager.Instance.IsMoving = true;
            }
            else
            {
                insideObject.SetActive(false);
            }
        }

        // Start is called before the first frame update
        private void Start()
        {
            insideObject.SetActive(false);
            if (insideObject.activeSelf)
            {
                insideObject.GetComponent<SpriteRenderer>().color = Color.clear;
                // inside_object.SetActive(false);
            }
        }

        // Update is called once per frame
        private void UpdateUI()
        {
            if (opened)
            {
                if(insideObject!=null)
                    insideObject.SetActive(true);
                this.GetComponent<SpriteRenderer>().sprite = sprite;
                PlayerManager.Instance.IsMoving = true;
            }
        }
    }
}