using Battle;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Enemies
{
    public class HpBar:MonoBehaviour
    {
        public static HpBar Instance;
        [FormerlySerializedAs("hp_slider")] public Slider hpSlider;
        [SerializeField] private BattleUnit enemy;


        private void Awake()
            {
                if(Instance==null)
                    Instance = this;
                
            }
            
            

            public void Setup(BattleUnit e)
            {
                hpSlider = this.GetComponent<Slider>();
              
                if (hpSlider != null)
                {
                    
                    if (e != null)
                    {
                        this.enemy = e;       
                        hpSlider.maxValue = enemy.Hp;
                        hpSlider.value = enemy.Hp;
                      //  Debug.Log(" max value of slider " + hp_slider.maxValue+ " with hp: " + hp_slider.value);
                        UpdateUI_Enemy();
                    }
                }
                else
                {
                    Debug.Log("slider is null");
                }
            }

            private void Hp_Slider_zero()
            {
                Debug.Log("HP is zero, the enemy is dead!!");
                
            }
            
            public void UpdateUI_Enemy()
            {
                
                Color healty = new Color(0.2235294f, 0.4823529f, 0.2666667f);
                Color middle = new Color(0.9568627f, 0.7058824f, 0.1058824f);
                Color dying = new Color(0.6627451f, 0.2313726f, 0.2313726f);
                if(enemy != null){
                    hpSlider.value = enemy.Hp;
                    if(hpSlider.value == 0)
                        GameController.Instance.StopBattle();
                }
                if (hpSlider.value == 0f)
                    Hp_Slider_zero();
                else
                {
                    if (hpSlider.value >= 2*hpSlider.maxValue/3)
                    {
                        hpSlider.fillRect.GetComponent<Image>().color = healty;
                    }
                    else
                    {
                        hpSlider.fillRect.GetComponent<Image>().color = hpSlider.value > hpSlider.maxValue/3 ? middle : dying;
                    }
                }
            }
    }
}