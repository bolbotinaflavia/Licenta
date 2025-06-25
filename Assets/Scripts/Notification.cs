using System.Collections;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class Notification:MonoBehaviour
    {
        [SerializeField]private TextMeshProUGUI message;
        public TextMeshProUGUI Message
        {
            get => message;
            set => message = value;
        }
        public IEnumerator notification_show(string final_text, float seconds)
        {
            message.text = "";
            foreach (var letter in final_text.ToCharArray())
            {
                if (letter.Equals('\n'))
                {
                    yield return new WaitForSeconds(1/30f);
                }
                message.text += letter;
                yield return new WaitForSeconds(1/50f);
            }
            yield return new WaitForSeconds(seconds);
            StartCoroutine(notification_delete());
        }
        private IEnumerator notification_delete()
        {
            message.text = "";
            yield return new WaitForSeconds(3f);
        }
    }
}