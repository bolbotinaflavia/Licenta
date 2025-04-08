using UnityEngine;
using UnityEngine.Serialization;

namespace Items
{
    [CreateAssetMenu(fileName = "Food", menuName = "Food/Create new item")]
    public class FoodBase:ScriptableObject
    {
        [FormerlySerializedAs("name")] [SerializeField] private string foodName;
        [SerializeField] private string description;
        [SerializeField] private int hp; //creste hp
        [SerializeField] private int defense; //creste defense ul
        [SerializeField] private Sprite icon;
    }
}