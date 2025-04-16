using UnityEngine;

namespace Items
{
    public class ItemBase:ScriptableObject
    {
        [SerializeField] private string itemName;
        [SerializeField] private Sprite icon;
        [SerializeField]private string description;
    }
}