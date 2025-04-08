using Inventory;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Sliders_scripts
{
    public class SpellSelect : MenuCountdown
    {
        [FormerlySerializedAs("S")] public Spell s;
        [FormerlySerializedAs("name")] public string spellName;
        private Scene _scene;
        public TextMeshProUGUI text;
        public TextMeshProUGUI description;
        [FormerlySerializedAs("canvas_description")] public GameObject canvasDescription;
        protected override void OnTimerComplete()
        {
            if (s != null)
            {
                if (_scene.name.Equals("Fight"))
                {
                    use_spell();
                }
                else
                {
                    open_description();
                    UpdateUI();
                }
            
            }
            else
            {
                Debug.Log("Spell is not available yet");
            }
        
        }

        public void open_description()
        {
            canvasDescription.GameObject().SetActive(true);
            description.GameObject().SetActive(true);
        }
        public void FindSpellsInGame()
        {
            s= GameObject.Find(spellName).ConvertTo<Spell>();
        
        }
        public void use_spell()
        {
            //DE VAZUT LA FIGHT
        }

        public void UpdateUI()
        {
            if (s != null)
            {
                if (s.get_magic_level()==0)
                {
                    menuOption.fillRect.GetComponent<Image>().color = Color.gray;
                    text.text = "-1@3~%%$@";
                    description.text="No information available";
                }
                else
                {
                    menuOption.fillRect.GetComponent<Image>().color = new Color(0.9568627f, 0.7058824f, 0.1058824f);
              
                    if (s.get_magic_level().Equals(0))
                    {
                        text.text = s.name;
                    }

                    if (s.get_magic_level().Equals(1))
                    {
                        text.text = spellName;
                        description.text="No information available";
                    }

                    if (s.get_magic_level().Equals(2))
                    {
                        text.text = spellName;
                        description.text = s.Description2;
                        Debug.Log(description.text);
                    }

                    if (s.get_magic_level().Equals(3))
                    {
                        text.text = spellName;
                        description.text = s.Description2+"/n"+s.Description3;
                    }
                }
            }
        }
        // Start is called before the first frame update
        private void Start()
        {
            FindSpellsInGame();
            // canvas_description.GetComponent<Sprite>();
            // text = GetComponent<TextMeshProUGUI>();
            // description = GetComponent<TextMeshProUGUI>();
            if(canvasDescription!=null)
                canvasDescription.SetActive(false);
            if(description!=null)
                description.GameObject().SetActive(false);
            _scene=SceneManager.GetActiveScene();
            UpdateUI();
        
        }

        // Update is called once per frame
    }
}
