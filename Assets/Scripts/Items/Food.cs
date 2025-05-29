using Player;
using Sliders_scripts;
using UnityEngine;

namespace Items
{
    public class Food:MonoBehaviour
    {
        [SerializeField] private FoodBase _base;
        [SerializeField] private GameObject _food;
        [SerializeField] private int _amount;
        [SerializeField] public bool Found;
        [SerializeField]private Rigidbody2D Rb;
        public FoodBase Base { get => _base; set => _base = value; }
        private void Awake()
        {
            Rb=_food.GetComponent<Rigidbody2D>();  
        }
    }
}