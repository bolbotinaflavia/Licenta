using System.Globalization;
using Player;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Sliders_scripts
{
    public class VolumeMenu : MenuCountdown
    {
        private VolumeManager _v;
        public static VolumeMenu Instance;
        [FormerlySerializedAs("inc_dec")] public int incDec;
        public Text counterText;
        protected override void OnTimerComplete()
        {
            if (incDec > 0)
            {
                VolumeManager.Instance.Increase();
                UpdateUI();
                menuOption.value = 1;
                if (PlayerMovement.Instance.CurrentControl.get_action().name.Equals("EyeMove") && !PlayerMovement.Instance.CurrentControl.get_click_action().triggered)
                    StartTimer();
            }
            else
            {
                VolumeManager.Instance.Decrease();
                UpdateUI();
                menuOption.value = 1;
                if (PlayerMovement.Instance.CurrentControl.get_action().name.Equals("EyeMove") && !PlayerMovement.Instance.CurrentControl.get_click_action().triggered)
                    StartTimer();
            }
            menuOption.value = 1;
              StartCoroutine(Deselect());
        }

        private void Awake()
        {
            Instance = this;
            _v= VolumeManager.FindObjectOfType<VolumeManager>();
            counterText.text = (_v.globalVolume.weight * 100).ToString(CultureInfo.InvariantCulture);
            UpdateUI();
        }
        // Start is called before the first frame update
        private void Start()
        {
            counterText.text=(_v.globalVolume.weight).ToString(CultureInfo.InvariantCulture);
            UpdateUI();
        }

        // Update is called once per frame
        private void UpdateUI()
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
