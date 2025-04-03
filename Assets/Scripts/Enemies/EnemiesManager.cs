using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemiesManager:MonoBehaviour
    {
        public static EnemiesManager Instance;
        public int nr_Enemies;
        [SerializeField] List<EnemieBase> enemies = new List<EnemieBase>();

        void Start()
        {
            
        }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }
    }
}