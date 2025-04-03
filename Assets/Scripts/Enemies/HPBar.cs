using Battle;
using UnityEngine;
using UnityEngine.UI;

namespace Enemies
{
    public class HPBar:MonoBehaviour
    {
        public static HPBar Instance;
        public UnityEngine.UI.Slider hp_slider;
        public BattleUnit enemy;
        
            void Awake()
            {
                if(Instance==null)
                    Instance = this;
                
            }
            void Start()
            {
               
                if (hp_slider != null)
                {
                    hp_slider.value = enemy.hp;
                    UpdateUI();
                }
                else
                {
                    Debug.Log("slider is null");
                }
            }

            public void Setup(BattleUnit e)
            {
                hp_slider = this.GetComponent<UnityEngine.UI.Slider>();
                enemy = e;
                if (hp_slider != null)
                {
                    hp_slider.value = e.hp;
                    Debug.Log("hp is " + hp_slider.value);
                    UpdateUI();
                }
                else
                {
                    Debug.Log("slider is null");
                }
            }
            void Hp_Slider_zero()
            {
                Debug.Log("HP is zero, you are DEAD!!");
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

            void Update()
            {
               
            }
    }
}