using UnityEngine;
using UnityEngine.Serialization;

namespace Items
{
    [CreateAssetMenu(fileName = "Food", menuName = "Food/Create new item")]
    public class FoodBase:ScriptableObject
    {
        [FormerlySerializedAs("name")] [SerializeField] private string foodName;
        [SerializeField] private int hp;
        [SerializeField] private Sprite img;
        public string FoodName { get => foodName; set => foodName = value; }
        public int Hp { get => hp; set => hp = value; }
        public Sprite Img { get => img; set => img= value; }
    }
}