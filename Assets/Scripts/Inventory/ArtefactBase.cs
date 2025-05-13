using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "Artefact", menuName = "Artefact/Create new artefact")]
    public class ArtefactBase:ScriptableObject
    {
        [SerializeField]private string artefactName;
        [SerializeField]private  string description;
        [SerializeField]private  bool discovered;
        [SerializeField]private Sprite image;
        public string ArtefactName=> artefactName;
        public string Description=> description;
        public bool Discovered
        {
            get => discovered;
            set => discovered = value;
        }
        public Sprite Image => image;
        
        public Sprite GetDisplayImage()
        {
            return Discovered ? Image : null;
        }
    }
}