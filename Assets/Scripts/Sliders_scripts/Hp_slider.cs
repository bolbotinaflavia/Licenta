using Player;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Sliders_scripts
{
    public class HpSlider : MonoBehaviour
    {
        public static HpSlider Instance;
        [FormerlySerializedAs("hp_slider")] public Slider hpSlider;

        private void Awake()
        {
            if(Instance==null)
                Instance = this;
            // animator = GetComponent<Animator>();
        }

        private void Hp_Slider_zero()
        {
            Debug.Log("HP is zero, you are DEAD!!");
        }

        // Start is called before the first frame update
        private void Start()
        {
            hpSlider = this.GetComponent<Slider>();
            if (hpSlider != null)
            {
                hpSlider.value = PlayerManager.Instance.hp;
                UpdateUI();
            }
            else
            {
                Debug.Log("slider is null");
            }
            
        }

        // Update is called once per frame


        public void UpdateUI()
        {
        
            Color healthy = new Color(0.2235294f, 0.4823529f, 0.2666667f);
            Color middle = new Color(0.9568627f, 0.7058824f, 0.1058824f);
            Color dying = new Color(0.6627451f, 0.2313726f, 0.2313726f);
            hpSlider.value = PlayerManager.Instance.hp;
            if (hpSlider.value == 0f)
                Hp_Slider_zero();
            else
            {
                if (hpSlider.value >= 70)
                {
                    hpSlider.fillRect.GetComponent<Image>().color = healthy;
                }
                else
                {
                    hpSlider.fillRect.GetComponent<Image>().color = hpSlider.value > 40 ? middle : dying;
                }
            }
            
        }
    }
}
