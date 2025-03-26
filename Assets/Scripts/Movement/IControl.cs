using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
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
        public void enter_slider(Slider s);
        public void exit_slider(Slider s);
    }
}