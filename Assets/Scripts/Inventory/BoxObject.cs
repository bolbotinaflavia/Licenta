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
                this.GetComponent<SpriteRenderer>().sprite = sprite;
                Update();
                if (insideObject.CompareTag("Weapons"))
                {
                    var weapon = insideObject.gameObject.GetComponent<WeaponBase>();
                    if (weapon == null) return;
                    Debug.Log("Finding weapon");
                    insideObject.SetActive(true);
                    insideObject.GetComponent<SpriteRenderer>().color = Color.white;
                    PlayerManager.Instance.FindWeapon(weapon);
                }
                //pentru alte obiecte
            }
        }

        private void Awake()
        {
            Instance = this;
            if (opened)
            {
                insideObject.SetActive(true);
                this.GetComponent<SpriteRenderer>().sprite = sprite;
            }
        }

        // Start is called before the first frame update
        private void Start()
        {
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
                insideObject.SetActive(true);
                this.GetComponent<SpriteRenderer>().sprite = sprite;
            }
        }
    }
}