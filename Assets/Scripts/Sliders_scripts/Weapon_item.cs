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

        public Image fade;
        //public Sprite weaponSprite;

        protected override void OnTimerComplete()
        {
            if (_weapon != null)
            {
               
                    SelectWeapon();
                    UpdateUI();
               
            }
            menuOption.value = 1;
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
            if(!PlayerMovement.Instance.CurrentControl.get_action().name.Equals("KeyboardMove")) 
                UpdateUI();
        
        }

        // Update is called once per frame

        public void SelectWeapon()
        {
            if (_weapon.WeaponName != InventoryManager.Instance.CurrentWeapon.WeaponName)
            {
                Color c = new Color(0.9568627f, 0.7058824f, 0.1058824f);

                foreach (var s in FindObjectsOfType<Slider>())
                {
                    if (s.IsActive())
                    {
                        if(s.GetComponent<WeaponsMenu>()!=null)
                            s.GetComponent<WeaponsMenu>().fade.color = new Color(0.4901961f, 0.4392157f, 0.4431373f);
                    }
                }

                //weapon.InUse = true;
                fade.color = new Color(0.2234294f, 0.4823529f, 0.2666667f);
                    //menuOption.fillRect.GetComponent<Image>().color = new Color(0.2234294f, 0.4823529f, 0.2666667f);
                
                
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
                    if (_weapon.WeaponName == InventoryManager.Instance.CurrentWeapon.WeaponName)
                    {
                        fade.color = new Color(0.2234294f, 0.4823529f, 0.2666667f);
                       // menuOption.fillRect.GetComponent<Image>().color = new Color(0.2234294f, 0.4823529f, 0.2666667f);
                    }
                    else
                    {
                        menuOption.fillRect.GetComponent<Image>().color =
                            menuOption.GetComponent<MenuCountdown>().baseColor;
                    }
                }
                else
                {
                    menuOption.fillRect.GetComponent<Image>().color =
                        menuOption.GetComponent<MenuCountdown>().baseColor;
                }
        }

        public void Update()
        {
            
                if (_weapon != null)
                {
                    img.sprite = _weapon.Image;
                    if (_weapon.WeaponName == InventoryManager.Instance.CurrentWeapon.WeaponName)
                    {

                        fade.color = new Color(0.2234294f, 0.4823529f, 0.2666667f);
                        //menuOption.fillRect.GetComponent<Image>().color = new Color(0.2234294f, 0.4823529f, 0.2666667f);
                    }
                     else
                     {
                         //menuOption.fillRect.GetComponent<Image>().color = new Color(0.9568627f, 0.7058824f, 0.1058824f);
                     }
                
                // else
                // {
                //     menuOption.fillRect.GetComponent<Image>().color = new Color(0.9568627f, 0.7058824f, 0.1058824f);
                // }
            }
        }
    }
}
