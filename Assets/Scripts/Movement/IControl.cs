using Player;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Movement
{


    public interface IControl
    {
        void Move(PlayerManager player);
        void Enable();
        void Disable();
        InputAction get_action();
        InputAction get_click_action();
        public void enter_slider(Slider s);
        public void exit_slider(Slider s);
        public void load_sliders();
        public void select_sliders();
        public void Move_pregame();
        public void UpdateUI();
    }
}