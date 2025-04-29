using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    public class EnemiesManager:MonoBehaviour
    {
        private static EnemiesManager _instance;
        [FormerlySerializedAs("nr_Enemies")] public int nrEnemies;
        //[SerializeField] private List<EnemieBase> enemies_s = new List<EnemieBase>();

        private void Awake()
        {
            if (_instance != null) return;
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }
}