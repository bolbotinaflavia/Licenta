using Cinemachine;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;

namespace Sliders_scripts
{
    public class run:Menu_countdown
    {
        protected override void OnTimerComplete()
        { 
           GameController.Instance.StopBattle();
        }
    }
}