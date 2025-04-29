using UnityEngine;

namespace Inventory
{
    public class WeaponItem:MonoBehaviour
    {
        public WeaponBase Weapon;
        private SpriteRenderer _spriteRenderer;

        // private void Start()
        // {
        //     _spriteRenderer = GetComponent<SpriteRenderer>();
        //     UpdateWeaponSprite();
        // }
        //
        // private void UpdateWeaponSprite()
        // {
        //     _spriteRenderer.sprite = Weapon.GetDisplayImage();
        // }
        // public void Interact()
        // {
        //     //de vazut cu slider
        //
        // }
    }
}