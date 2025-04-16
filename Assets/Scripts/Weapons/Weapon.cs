using UnityEngine;
using UnityEngine.Serialization;

namespace Weapons
{
    
    public class Weapon:MonoBehaviour
    {
        [FormerlySerializedAs("weaponBase")] [SerializeField] private WeaponB weaponB;
        [SerializeField] private GameObject w;
        [SerializeField]private Rigidbody2D Rb;

        public WeaponB WeaponB
        {
            get => weaponB;
            set => weaponB = value;
        }
        private void Awake()
        {
            Rb=w.GetComponent<Rigidbody2D>();  
        }
    }
}