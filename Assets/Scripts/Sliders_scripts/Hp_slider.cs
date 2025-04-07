using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;

public class Hp_slider : MonoBehaviour
{
    public static Hp_slider Instance;
    public UnityEngine.UI.Slider hp_slider;

    void Awake()
    {
        if(Instance==null)
            Instance = this;
       // animator = GetComponent<Animator>();
    }
    void Hp_Slider_zero()
    {
        Debug.Log("HP is zero, you are DEAD!!");
    }

    // Start is called before the first frame update
    void Start()
    {
        hp_slider = this.GetComponent<UnityEngine.UI.Slider>();
        if (hp_slider != null)
        {
            hp_slider.value = PlayerManager.Instance.HP;
            UpdateUI();
        }
        else
        {
            Debug.Log("slider is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void UpdateUI()
    {
        
        Color healty = new Color(0.2235294f, 0.4823529f, 0.2666667f);
        Color middle = new Color(0.9568627f, 0.7058824f, 0.1058824f);
        Color dying = new Color(0.6627451f, 0.2313726f, 0.2313726f);
        hp_slider.value = PlayerManager.Instance.HP;
        if (hp_slider.value == 0f)
            Hp_Slider_zero();
        else
        {
            if (hp_slider.value >= 70)
            {
                hp_slider.fillRect.GetComponent<Image>().color = healty;
            }
            else
            {
                if (hp_slider.value > 40)
                   hp_slider.fillRect.GetComponent<Image>().color = middle;
                else
                {
                    hp_slider.fillRect.GetComponent<Image>().color = dying;
                }
            }
        }
    }
}
