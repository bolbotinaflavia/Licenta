using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory
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
                Update();
                insideObject.SetActive(true);
                insideObject.GetComponent<SpriteRenderer>().color = Color.white;
               // Invoke(nameof(check_object),2f);
               
                //pentru alte obiecte
            }
        }

        private void check_object()
        {
            if (insideObject != null)
            {
                if (insideObject.CompareTag("Weapons"))
                {
                    var weapon = insideObject.gameObject.GetComponent<WeaponBase>();
                    if (weapon == null) return;
                    Debug.Log("Finding weapon");
                    insideObject.SetActive(false);
                    insideObject.GetComponent<SpriteRenderer>().color = Color.clear;
                    PlayerManager.Instance.isMoving = true;
                    InventoryManager.Instance.FindWeapon(insideObject);
                }
                PlayerManager.Instance.isMoving = true;
            }
        }

        private void Awake()
        {
            Instance = this;
            if (opened)
            {
                insideObject.SetActive(true);
                this.GetComponent<SpriteRenderer>().sprite = sprite;
                PlayerManager.Instance.isMoving = true;
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
        private void Update()
        {
            if (opened)
            {
                if(insideObject!=null)
                    insideObject.SetActive(true);
                this.GetComponent<SpriteRenderer>().sprite = sprite;
                PlayerManager.Instance.isMoving = true;
            }
        }
    }
}