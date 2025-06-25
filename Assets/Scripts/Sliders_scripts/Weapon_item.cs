using System.Collections;
using Inventory;
using Player;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Weapons;

namespace Sliders_scripts
{
    public class WeaponsMenu : MenuCountdown
    {
        [SerializeField] private WeaponB weapon;
        [SerializeField]private string weaponName;
        public Image img;

        public Image fade;
        public TextMeshProUGUI description;
        public GameObject canvasDescription;
        //public Sprite weaponSprite;

        protected override void OnTimerComplete()
        {
            if (weapon != null)
            {
                SelectWeapon();
                UpdateUI();
                open_description();
                UpdateUI();
                StartCoroutine(close_description());

            }

            menuOption.value = 1;
              StartCoroutine(Deselect());
        }

        private void open_description()
        {
            canvasDescription.GameObject().SetActive(true);
            description.GameObject().SetActive(true);
            StartCoroutine(close_description());
        }

        private IEnumerator close_description()
        {
            yield return new WaitForSeconds(3f);
            canvasDescription.GameObject().SetActive(false);
            description.GameObject().SetActive(false);
        }

    public void FindWeaponsInInventory()
        {
            if (InventoryManager.Instance != null)
            {
                weapon = InventoryManager.Instance.Weapons.Find(w => w.WeaponName == weaponName);
                if (description != null && weapon != null)
                    description.text = weapon.Description;
            }
        }



        // Start is called before the first frame update
        private void Start()
        {
            //weapon = w.gameObject.GetComponent<Weapons>();
            FindWeaponsInInventory();
            if(!PlayerMovement.Instance.CurrentControl.get_action().name.Equals("KeyboardMove")) 
                UpdateUI();
            if(canvasDescription!=null)
                canvasDescription.SetActive(false);
            if(description!=null)
                description.GameObject().SetActive(false);
        
        }

        // Update is called once per frame

        public void SelectWeapon()
        {
            if (weapon.WeaponName != InventoryManager.Instance.CurrentWeapon.WeaponName)
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
                
                
                InventoryManager.Instance.SelectWeapon(weapon);
               
            }
            else
                Debug.Log("Weapons selected already");
        }
        public void UpdateUI()
        {
           
                if (weapon != null)
                {
                    img.sprite = weapon.Image;
                    description.text = weapon.Description;
                    if (weapon.WeaponName == InventoryManager.Instance.CurrentWeapon.WeaponName)
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
    }
}
