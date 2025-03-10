using UnityEngine;
using UnityEngine.UI;
public class WeaponItem:MonoBehaviour
{
    public Weapons weapon;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateWeaponSprite();
    }
    public void UpdateWeaponSprite()
    {
        spriteRenderer.sprite = weapon.GetDisplayImage();
    }
    public void Interact()
    {
        //de vazut cu slider
        
    }
}