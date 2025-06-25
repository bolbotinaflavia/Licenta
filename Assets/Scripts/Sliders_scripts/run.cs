using Battle;
using Enemies;

namespace Sliders_scripts
{
    public class Run:MenuCountdown
    {
        protected override void OnTimerComplete()
        {
            if (GameController.Instance.EnemyObj != null)
            {
                var name = GameController.Instance.EnemyObj.GetComponent<Enemy>().EnemieBase.name;
                if (name.Equals("Magician"))
                {
                   StartCoroutine(BattleSystem.Instance.Notification.notification_show("You can't run from the magician!", 2f));
                }
                else
                {
                    GameController.Instance.StopBattle(false);
                    menuOption.value = 1;
                }
            }
            else
            {
                GameController.Instance.StopBattle(false);
                this.menuOption.value = 1;
                menuOption.value = 1;
            }
                 StartCoroutine(Deselect());
        }
    }
}