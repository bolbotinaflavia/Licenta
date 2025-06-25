using Food;
using Inventory;
using Items;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Sliders_scripts
{
    public class SelectItem : MenuCountdown
    {
        // public static Select_item Instance;
        [FormerlySerializedAs("name")] public string itemName;
        [SerializeField]private FoodBase _f;
        private Sprite img;
        public Text t;
        protected override void OnTimerComplete()
        {


            if (_f != null)
            {
                InventoryManager.Instance.Consume(_f);
                if (PlayerManager.Instance.hp + _f.Hp < 100f)
                {
                    PlayerManager.Instance.hp += _f.Hp;
                    UpdateUI();
                }
                else
                {
                    PlayerManager.Instance.hp = 100;
                    Debug.Log("HP is already full->100");
                }
            }
            menuOption.value = 1;
                  StartCoroutine(Deselect());
        }
    

        // Start is called before the first frame update
        private void Start()
        {
            img = GetComponent<Sprite>();
           
            var food=Resources.Load<FoodBase>($"Food/{_f.FoodName}");
            _f=food;
            img = food.Img;
            t.text = _f!=null ? InventoryManager.Instance.get_number_of_food_item(food.FoodName).ToString() : "0";
        }

        // Update is called once per frame
        private void UpdateUI()
        {
            if (_f != null)
            {
                t.text = InventoryManager.Instance.get_number_of_food_item(_f.FoodName).ToString();
            }
        }
    }
}
