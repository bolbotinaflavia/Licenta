using Inventory;
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
        private ItemObject _i;
        public Text t;
        protected override void OnTimerComplete()
        {
       
            if (_i != null)
            {
                _i.Consume();
                if (PlayerManager.Instance.hp + _i.hp < 100f)
                {
                    PlayerManager.Instance.hp += _i.hp;
                }
                else
                {
                    Debug.Log("HP is already full->100");
                }
            
            }

            Debug.Log("Item selected");
        }
    

        // Start is called before the first frame update
        private void Start()
        {
            _i=PlayerManager.Instance.objects.Find(item => item.name.Equals(itemName));
            t.text = _i!=null ? _i.number.ToString() : "0";
        }

        // Update is called once per frame
        private void Update()
        {
            if (_i != null)
            {
                t.text = _i.number.ToString();
            }
        }
    }
}
