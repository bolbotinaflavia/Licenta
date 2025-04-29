using Player;
using Sliders_scripts;
using UnityEngine;

namespace Items
{
    public class Food:MonoBehaviour
    {
        [SerializeField] private FoodBase _base;
        [SerializeField] private int _amount;
        [SerializeField] public bool Found;
        public FoodBase Base { get => _base; set => _base = value; }
    }
}