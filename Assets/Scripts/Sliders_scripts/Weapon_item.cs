using Inventory;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Sliders_scripts
{
    public class WeaponsMenu : MenuCountdown
    {
        public string weaponName;
        private WeaponBase _weapon;
        public Image img;
        //public Sprite weaponSprite;

        protected override void OnTimerComplete()
        {
            if (_weapon != null&&_weapon.IsDiscovered())
            {
                SelectWeapon();
                UpdateUI();
            }
        }

        public void FindWeaponsInGame()
        {
            _weapon= GameObject.Find(weaponName).ConvertTo<WeaponBase>();
        }



        // Start is called before the first frame update
        private void Start()
        {
            //weapon = w.gameObject.GetComponent<Weapons>();
            FindWeaponsInGame();
            UpdateUI();
        
        }

        // Update is called once per frame

        public void SelectWeapon()
        {
            if (_weapon.InUse == false)
            {
            
                Color c = new Color(0.9568627f, 0.7058824f, 0.1058824f);

                foreach (var w in PlayerManager.Instance.Weapons)
                {
                    w.SetInhUse(false);
               
                }
                foreach(var s in FindObjectsOfType<Slider>())
                {
                    if (s.IsActive())
                    {
                        if (!s.name.Equals("Back")&&!s.name.Equals("HP"))
                            s.fillRect.GetComponent<Image>().color = c;
                    }
                }
                //weapon.InUse = true;
                menuOption.fillRect.GetComponent<Image>().color = new Color(0.9019608f,0.282353f,0.1803922f);
                _weapon.SetInhUse(true);
            }
            else
                Debug.Log("Weapons selected already");
        }
        public void DiscoverWeapon()
        {
            _weapon.Discover();
            UpdateUI();
        }
        public void UpdateUI()
        {
            if (_weapon != null)
            {
                if (_weapon.GetDisplayImage() != null)
                {
                    img.sprite = _weapon.GetDisplayImage();
                }
                if(_weapon.InUse)
                    menuOption.fillRect.GetComponent<Image>().color = new Color(0.9019608f,0.282353f,0.1803922f);
            }
        }
    }
}
