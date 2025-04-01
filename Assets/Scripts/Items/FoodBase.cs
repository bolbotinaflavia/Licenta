using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    [CreateAssetMenu(fileName = "Food", menuName = "Food/Create new item")]
    public class FoodBase:ScriptableObject
    {
        [SerializeField] private string name;
        [SerializeField] private string description;
        [SerializeField] private int hp; //creste hp
        [SerializeField] private int defense; //creste defense ul
        [SerializeField] private Sprite icon;
    }
}