using UnityEngine;

namespace Enemies
{
    public class Enemy:MonoBehaviour
    {
        [SerializeField] private EnemieBase enemieBase;
        public EnemieBase EnemieBase => enemieBase;
        
    }
}