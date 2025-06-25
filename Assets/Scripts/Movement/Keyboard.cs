using System.Collections;
using System.Collections.Generic;
using Inventory;
using Player;
using StaticObjects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;


namespace Movement
{
    public class Keyboard : IControl
    {
        private readonly InputAction _moveAction;
        private readonly InputAction _clickAction;
        private List<Slider> sliders=new List<Slider>();
        private bool menu_battle=false;
        private int currentSliderIndex=0;
        private Color selectedColor=new Color(0.2234294f, 0.4823529f, 0.2666667f);

        public Keyboard(InputAction moveAction, InputAction clickAction)
        {
            this._moveAction = moveAction;
            this._clickAction = clickAction;
            
        }

        public void Enable()
        {
            _moveAction.Enable();
            _clickAction.Enable();
        }

        public void Disable()
        {
            _moveAction.Disable();
            _clickAction.Disable();
        }

        public InputAction get_action()
        {
            return _moveAction;
        }

        public InputAction get_click_action()
        {
           return _clickAction;
        }

        public void enter_slider(UnityEngine.UI.Slider s)
        {
            s.fillRect.GetComponent<Image>().color= Color.cyan;
        }

        public void exit_slider(UnityEngine.UI.Slider s)
        {
            s.fillRect.GetComponent<Image>().color = Color.black;
        }

        public void load_sliders()
        {
            sliders.Clear();
            Color c=new Color(0.9568627f, 0.7058824f, 0.1058824f);
            foreach(var s in GameObject.FindObjectsOfType<Slider>())
            {
                if (s.IsActive())
                {
                    if (!s.name.Equals("OpenMenu")&&!s.name.Equals("HP") && !s.tag.Equals("HP"))
                    {
                        if (s.tag.Equals("Fight"))
                        {
                            if (!MenuManager.Instance.current.activeSelf)
                            {
                                s.value = 1;
                                s.fillRect.GetComponent<Image>().color = c;
                                sliders.Add(s);
                            }
                        }
                        else
                        {
                            s.value = 1;
                            s.fillRect.GetComponent<Image>().color = c;
                            sliders.Add(s);
                        }
                    }
                }
            }
            sliders.TrimExcess();
            if (sliders.Count > 0)
            {
                currentSliderIndex = sliders.Count - 1;
                sliders[currentSliderIndex].fillRect.GetComponent<Image>().color = selectedColor;
            }
        }

        public void UpdateUI()
        {
            foreach (var s in GameObject.FindObjectsOfType<Slider>())
            {
                if (s.IsActive())
                {
                    if (!s.name.Equals("OpenMenu") && !s.name.Equals("HP") && !s.tag.Equals("HP"))
                    {
                        s.value = 1;
                    }
                }
            }
        }

        public void select_sliders()
        {
                Color c = new Color(0.9568627f, 0.7058824f, 0.1058824f);
                Vector2 inputPos = new Vector2(0,0);
                if (PlayerMovement.Instance.CurrentControl.get_click_action().IsPressed())
                {
                    sliders[currentSliderIndex].GetComponent<MenuCountdown>().OnClicked();
                }
                if (_moveAction.triggered)
                {
                    inputPos = _moveAction.ReadValue<Vector2>();
                    if (Mathf.Approximately(inputPos.y, 1) || Mathf.Approximately(inputPos.x, 1))
                    {
                        if (currentSliderIndex + 1 < sliders.Count)
                        {
                            sliders[currentSliderIndex].value = 1;
                            sliders[currentSliderIndex].fillRect.GetComponent<Image>().color = c;
                            currentSliderIndex++;
                            sliders[currentSliderIndex].value = 1;
                            sliders[currentSliderIndex].fillRect.GetComponent<Image>().color = selectedColor;
                        }
                        else
                        {
                            if (currentSliderIndex + 1 >= sliders.Count)
                            {
                                sliders[currentSliderIndex].value = 1;
                                sliders[currentSliderIndex].fillRect.GetComponent<Image>().color = c;
                                currentSliderIndex = 0;
                                sliders[currentSliderIndex].value = 1;
                                sliders[currentSliderIndex].fillRect.GetComponent<Image>().color = selectedColor;
                            }
                        }
                    }
                    if (Mathf.Approximately(inputPos.y, -1) || Mathf.Approximately(inputPos.x, -1))
                    {
                        if (currentSliderIndex - 1 >= 0)
                        {
                            sliders[currentSliderIndex].value = 1;
                            sliders[currentSliderIndex].fillRect.GetComponent<Image>().color = c;
                            currentSliderIndex--;
                            sliders[currentSliderIndex].value = 1;
                            sliders[currentSliderIndex].fillRect.GetComponent<Image>().color = selectedColor;

                        }
                        else
                        {
                            if (currentSliderIndex - 1 < 0)
                            {
                                sliders[currentSliderIndex].value = 1;
                                sliders[currentSliderIndex].fillRect.GetComponent<Image>().color = c;
                                currentSliderIndex = sliders.Count - 1;
                                sliders[currentSliderIndex].value = 1;
                                sliders[currentSliderIndex].fillRect.GetComponent<Image>().color = selectedColor;
                            }
                        }
                    }
                }
        }

        public void Move_pregame()
        {
            select_sliders();
        }
        public void Move(PlayerManager player)
        {
            if (GameController.Instance.state == GameState.Menu || SceneManager.GetActiveScene().name == "StartGame")
            {
                select_sliders();
            }
            else
            {
                Vector2 inputPos = _moveAction.ReadValue<Vector2>();
                if (Mathf.Approximately(inputPos.y, 1))
                {
                    player.IsMoving = false;
                    player.menu_slider_open();
                    player.isHover = false;
                    MenuManager.Instance.current.SetActive(true);
                    GameController.Instance.state = GameState.Menu;
                    load_sliders();
                }
                else
                {
                    player.menu_slider_close();
                }
                if (!PlayerManager.Instance.IsMoving) return;
                Vector3 worldMouse =
                         Camera.main.ScreenToWorldPoint(new Vector3(inputPos.x, inputPos.y, Camera.main.nearClipPlane));
                Vector3 mouseNext = new Vector3(player.player.transform.position.x + (inputPos.x * 10f),
                    player.player.transform.position.y, worldMouse.z);
                if (player.IsMoving)
                {
                    if (MenuManager.Instance.current.activeSelf == false && inputPos.x != 0)
                    {

                        player.SetFacingDirection(inputPos.x);
                        RaycastHit2D hit = Physics2D.Raycast(inputPos, Vector2.zero);
                        if ((hit.collider != null && hit.collider.gameObject == player.player) || player.newItem)
                        {
                            player.IsMoving = false;
                        }
                        else
                        {
                            if (player.player.transform.position.x < 398)
                            {
                                //IsMoving = false;
                                player.player.transform.position = Vector3.MoveTowards(
                                         player.player.transform.position,
                                         new Vector2(400f, player.player.transform.position.y), Time.deltaTime * 50f);
                            }
                            if (Door.Instance.Opened == true && player.player.transform.position.x < Door.Instance.transform.position.x + 100f)
                            {
                                player.player.transform.position = Vector3.MoveTowards(
                                         player.player.transform.position,
                                         new Vector2(Door.Instance.transform.position.x + 150f, player.player.transform.position.y), Time.deltaTime * 75f);
                            }
                            else
                            {

                                player.IsMoving = true;
                                player.player.transform.position = Vector3.MoveTowards(
                                         player.player.transform.position,
                                         mouseNext,
                                         Time.deltaTime * 75f);
                            }
                        }
                    }
                    else
                    {
                        player.IsMoving = false;
                    }
                }
           }

        }
    }
}