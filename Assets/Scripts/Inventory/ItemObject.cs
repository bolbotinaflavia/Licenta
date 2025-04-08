using Player;
using Sliders_scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory
{
    public class ItemObject:MonoBehaviour
    {
        private static ItemObject _instance;
        [FormerlySerializedAs("name")] public string itemName;
        public int number;
        public Sprite image;
        [FormerlySerializedAs("finded")] public bool found;
        public float hp;
        public ItemObject(string itemName, string imagePath, float hp)
        {
            this.itemName = itemName;
            number = 0;
            image=Resources.Load<Sprite>(imagePath);
            found = false;
            this.hp = hp;
        }
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                // DontDestroyOnLoad(gameObject);
            }
            else
            {
                //Destroy(gameObject);
            }
       
        }

        public void find_first_item()
        {
            number += 1;
            found = true;
        }
        public void find_item()
        {
            number += 1;
        }

        public void Consume()
        {
            if (number <= 0) return;
            PlayerManager.Instance.isEating = true;
            HpSlider.Instance.UpdateUI(); 
            number -= 1;
            Invoke(nameof(eating_animation),2f);
        }
        private void eating_animation()
        {
            Debug.Log("Animation started");
            PlayerManager.Instance.isEating = false;
            //IsMoving = true;
        
        }
        public string get_name()
        {
            return itemName;
        }

    }
}
