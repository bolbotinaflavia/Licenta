using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Spell_select : Menu_countdown
{
    public String name;
    private Spell s;
    private Scene scene;
    protected override void OnTimerComplete()
    {
        if (s != null)
        {
            if (scene.name.Equals("Fight"))
            {
                use_spell();
            }
            else
            {
                Debug.Log("Is not a Fight!!");
            }
            
        }
        else
        {
            Debug.Log("Spell is not available yet");
        }
        
    }
    public void FindSpellsInGame()
    {
        s= GameObject.Find(name).ConvertTo<Spell>();
        
    }
    public void use_spell()
    {
        //DE VAZUT LA FIGHT
    }

    public void UpdateUI()
    {
        Color c = new Color(0.9568627f, 0.7058824f, 0.1058824f);
        if (s != null)
        {
            if(s.isDiscovered()==false)
                menu_option.fillRect.GetComponent<Image>().color = Color.gray;
            else
            {
                menu_option.fillRect.GetComponent<Image>().color = new Color(0.9568627f, 0.7058824f, 0.1058824f);;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        FindSpellsInGame();
        scene=SceneManager.GetActiveScene();
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }
}
