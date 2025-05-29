using System.Collections;
using Inventory;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Sliders_scripts
{
    public class Artefact_select:MenuCountdown
    {
        [SerializeField] private ArtefactBase artefact;
        [SerializeField] private string artefactName;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private Image image;
        public GameObject canvasDescription;

        public string ArtefactName
        {
            get => artefactName;
            set => artefactName = value;
        }
        
        protected override void OnTimerComplete()
        {
            open_description();
            UpdateUI();
            StartCoroutine(close_description());
        }
        private void open_description()
        {
            canvasDescription.GameObject().SetActive(true);
            description.GameObject().SetActive(true);
        }

        private IEnumerator close_description()
        {
            yield return new WaitForSeconds(3f);
            canvasDescription.GameObject().SetActive(false);
            description.GameObject().SetActive(false);
        }
        private void UpdateUI()
        {
            if (artefact != null)
            {
                image.sprite = artefact.Image;
                description.text = artefact.Description;
            }
        }
        public void FindArtefactsInInventory()
        {
            if (InventoryManager.Instance != null)
            {
                artefact = InventoryManager.Instance.getArtefact(artefactName);
            }
        }
        private void Start()
        {
            FindArtefactsInInventory();
            // canvas_description.GetComponent<Sprite>();
            // text = GetComponent<TextMeshProUGUI>();
            // description = GetComponent<TextMeshProUGUI>();
            if(canvasDescription!=null)
                canvasDescription.SetActive(false);
            if(description!=null)
                description.GameObject().SetActive(false);
            UpdateUI();
        
        }
    }
}