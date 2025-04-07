using Battle;
using UnityEngine;
using UnityEngine.UI;

namespace Enemies
{
    public class HPBar:MonoBehaviour
    {
        public static HPBar Instance;
        public UnityEngine.UI.Slider hp_slider;
        [SerializeField] BattleUnit enemy;

        
        
        
            void Awake()
            {
                if(Instance==null)
                    Instance = this;
                
            }
            
            

            public void Setup(BattleUnit e)
            {
                hp_slider = this.GetComponent<UnityEngine.UI.Slider>();
              
                if (hp_slider != null)
                {
                    
                    if (e != null)
                    {
                        this.enemy = e;       
                        hp_slider.maxValue = enemy.Hp;
                        hp_slider.value = enemy.Hp;
                      //  Debug.Log(" max value of slider " + hp_slider.maxValue+ " with hp: " + hp_slider.value);
                        UpdateUI_Enemy();
                    }
                }
                else
                {
                    Debug.Log("slider is null");
                }
            }
            void Hp_Slider_zero()
            {
                Debug.Log("HP is zero, the enemy is dead!!");
                
            }
            
            public void UpdateUI_Enemy()
            {
                
                Color healty = new Color(0.2235294f, 0.4823529f, 0.2666667f);
                Color middle = new Color(0.9568627f, 0.7058824f, 0.1058824f);
                Color dying = new Color(0.6627451f, 0.2313726f, 0.2313726f);
                if(enemy != null)
                    hp_slider.value = enemy.Hp;
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
}