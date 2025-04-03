using UnityEngine;

namespace Battle
{
    public class BattleUnit : MonoBehaviour
    {
        [SerializeField] private EnemieBase _base;
        public EnemieBase enemy { get; set; }
        [SerializeField] private int hp_actual;
        public int hp { get; set; }

        public void Setup()
        {
            enemy=EnemieBase.Instantiate(_base);
            hp_actual = enemy.Hp_max;
        }
        
    }
}