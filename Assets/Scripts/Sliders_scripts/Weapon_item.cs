using Inventory;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

namespace Sliders_scripts
{
    public class WeaponsMenu : MenuCountdown
    {
        public string weaponName;
        private WeaponB _weapon;
        public Image img;
        //public Sprite weaponSprite;

        protected override void OnTimerComplete()
        {
            if (_weapon != null)
            {
                SelectWeapon();
                UpdateUI();
            }
        }

        public void FindWeaponsInInventory()
        {
            _weapon = InventoryManager.Instance.Weapons.Find(w => w.WeaponName == weaponName);
        }



        // Start is called before the first frame update
        private void Start()
        {
            //weapon = w.gameObject.GetComponent<Weapons>();
            FindWeaponsInInventory();
            UpdateUI();
        
        }

        // Update is called once per frame

        public void SelectWeapon()
        {
            if (_weapon.WeaponName != InventoryManager.Instance.CurrentWeapon.WeaponName)
            {
            
                Color c = new Color(0.9568627f, 0.7058824f, 0.1058824f);
                
                foreach(var s in FindObjectsOfType<Slider>())
                {
                    if (s.IsActive())
                    {
                        if (!s.name.Equals("Back") && !s.name.Equals("HP")&&!s.tag.Equals("HP"))
                            s.fillRect.GetComponent<Image>().color = c;
                    }
                }
                //weapon.InUse = true;
                menuOption.fillRect.GetComponent<Image>().color = new Color(0.9019608f,0.282353f,0.1803922f);
                InventoryManager.Instance.SelectWeapon(_weapon);
               
            }
            else
                Debug.Log("Weapons selected already");
        }
        public void UpdateUI()
        {
            if (_weapon != null)
            {
                img.sprite = _weapon.Image;
                if(_weapon.WeaponName==InventoryManager.Instance.CurrentWeapon.WeaponName)
                    menuOption.fillRect.GetComponent<Image>().color = new Color(0.9019608f,0.282353f,0.1803922f);
                else
                {
                    menuOption.fillRect.GetComponent<Image>().color = new Color(0.9568627f, 0.7058824f, 0.1058824f);
                }
            }
            else
            {
                menuOption.fillRect.GetComponent<Image>().color = Color.gray;
            }
        }

        public void Update()
        {
            if (_weapon != null)
            {
                img.sprite = _weapon.Image;
                if(_weapon.WeaponName==InventoryManager.Instance.CurrentWeapon.WeaponName)
                    menuOption.fillRect.GetComponent<Image>().color = new Color(0.9019608f,0.282353f,0.1803922f);
                else
                {
                    menuOption.fillRect.GetComponent<Image>().color = new Color(0.9568627f, 0.7058824f, 0.1058824f);
                }
            }
            else
            {
                menuOption.fillRect.GetComponent<Image>().color = Color.gray;
            }
        }
    }
}
