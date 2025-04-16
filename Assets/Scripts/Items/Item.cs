using UnityEngine;

namespace Items
{
    public class Item:MonoBehaviour
    {
        [SerializeField] private ItemBase itemBase;
        public ItemBase ItemBase { get => itemBase; set => itemBase = value; }
    }
}