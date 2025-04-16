using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "Moves", menuName = "Enemies/Create new move")]
    public class Moves:ScriptableObject
    {
        [SerializeField]string moveName;
        [SerializeField] private int power;
        [SerializeField] private int accuracy;
        [SerializeField] private Type type;
        public int Power { get => power; set => power = value; }
        public int Accuracy { get => accuracy; set => accuracy = value; }
        public string MoveName { get => moveName; set => moveName = value; }
    }
}