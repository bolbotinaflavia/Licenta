using System.Globalization;
using Player;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Sliders_scripts
{
    public class VolumeMenu : MenuCountdown
    {
        private Volume _v;
        public static VolumeMenu Instance;
        [FormerlySerializedAs("inc_dec")] public int incDec;
        public Text counterText;
        protected override void OnTimerComplete()
        {
            if (incDec > 0)
            {
                Volume.Instance.Increase();
                menuOption.value = 1;
                if(!PlayerMovement.Instance.CurrentControl.get_click_action().triggered) 
                    StartTimer();
            }
            else
            {
                Volume.Instance.Decrease();
                menuOption.value = 1;
                if(!PlayerMovement.Instance.CurrentControl.get_click_action().triggered) 
                    StartTimer();
            }
            menuOption.value = 1;
        }

        private void Awake()
        {
            Instance = this;
            _v= Volume.FindObjectOfType<Volume>();
            counterText.text = (_v.globalVolume.weight * 100).ToString(CultureInfo.InvariantCulture);
        }
        // Start is called before the first frame update
        private void Start()
        {
            counterText.text=(_v.globalVolume.weight).ToString(CultureInfo.InvariantCulture);
        }

        // Update is called once per frame
        private void Update()
        {
            if (counterText != null)
            {
                //v = PlayerPrefs.GetFloat("Volume", 0);
                counterText.text = (_v.globalVolume.weight*100).ToString(CultureInfo.InvariantCulture);
                //counterText.text = (s.volume*100).ToString();
            }
        }
    }
}
