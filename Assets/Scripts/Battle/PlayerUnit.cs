using UnityEngine;

namespace Battle
{
    public class PlayerUnit:MonoBehaviour
    {
        [SerializeField] private PlayerManager _playerUnit;

        public void Setup()
        {
            _playerUnit = GetComponent<PlayerManager>();
        }

        public void current_weapon()
        {
               
        }
    }
}