using UnityEngine;

namespace Artefacts
{
    public class Artefact:MonoBehaviour
    {
        [SerializeField] private ArtefactBase artefactBase;
        [SerializeField] private GameObject a;
        [SerializeField]private Rigidbody2D Rb;
        
        public ArtefactBase ArtefactBase
        {
            get => artefactBase;
            set => artefactBase = value;
        }
        private void Awake()
        {
            Rb=a.GetComponent<Rigidbody2D>();  
        }
    }
}