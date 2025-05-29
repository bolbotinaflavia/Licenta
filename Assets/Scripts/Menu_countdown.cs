using System;
using System.Collections;
using System.Linq;
using Battle;
using DefaultNamespace.HUD;
using Eyeware.BeamEyeTracker.Unity;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public abstract class MenuCountdown : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public MenuCountdown instance;
    [FormerlySerializedAs("menu_option")] public UnityEngine.UI.Slider menuOption;
    public Color baseColor;
    public float timer = 5f;
    private Coroutine _unfillCoroutine;
    private Action slider_clicked;
    public ImmersiveHUDPanelBehaviour immersiveHUDPanelBehaviour;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        immersiveHUDPanelBehaviour = GetComponent<ImmersiveHUDPanelBehaviour>();
    }

    public void OnClicked()
    {
        menuOption.value = 1;
        if (PlayerMovement.Instance.CurrentControl.get_action().name.Equals("MouseMove")&&Player.PlayerMovement.Instance.CurrentControl.get_click_action().triggered)
        {
           // PlayerMovement.Instance.CurrentControl.get_click_action().performed += ctx => OnTimerComplete();
           OnTimerComplete();
           PlayerMovement.Instance.CurrentControl.get_click_action().Reset();
            Debug.Log("Clicked by mouse and all");
        }

        if (PlayerMovement.Instance.CurrentControl.get_action().name.Equals("KeyboardMove") &&
            this.name.Equals("OpenMenu")&&GameController.Instance.state==GameState.FreeRoam)
        {
            OnTimerComplete();
            PlayerMovement.Instance.CurrentControl.load_sliders();
        }
        if (PlayerMovement.Instance.CurrentControl.get_action().name.Equals("KeyboardMove") &&
            PlayerMovement.Instance.CurrentControl.get_click_action().triggered)
        {
            Debug.Log("Clicked by enter and all");
            OnTimerComplete();
            PlayerMovement.Instance.CurrentControl.load_sliders();
            PlayerMovement.Instance.CurrentControl.get_click_action().Reset();
        }

        if (PlayerMovement.Instance.CurrentControl.get_action().name.Equals("EyeMove"))
        {
            Debug.Log("Looked at by eye track and all");
            OnTimerComplete();
            PlayerMovement.Instance.CurrentControl.get_click_action().Reset();
            
        }
    
        PlayerMovement.Instance.CurrentControl.get_click_action().Reset();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        menuOption.value = 1;
        StartTimer();
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Pointer Exited");
        EndTimer();
    }
    // ReSharper disable Unity.PerformanceAnalysis
    public void StartTimer()
    {
        menuOption.value = 1;
        // Debug.Log("Timer started");
        _unfillCoroutine ??= StartCoroutine(UnFillSlider());
    }
    public void EndTimer()
    {
        // Debug.Log("Timer ended");
        if (_unfillCoroutine != null)
        {
            StopCoroutine(_unfillCoroutine);
            _unfillCoroutine = null;
        }

        menuOption.value = 1;
    }
    public IEnumerator UnFillSlider()
    {
        float decRate = 0.3f / timer;
        
        while (menuOption.value > 0)
        {
            menuOption.value -= decRate * Time.deltaTime;

            yield return null;
        }

        menuOption.value = 0;
        _unfillCoroutine = null;
        OnTimerComplete();
    }

    private void Update()
    {
        if (PlayerMovement.Instance.CurrentControl.get_action().name.Equals("EyeMove"))
        {
            if (BattleSystem.Instance != null && GameController.Instance != null)
            {
                if (GameController.Instance.state.Equals(GameState.Battle))
                {
                    if (BattleSystem.Instance.State == BattleState.PlayerAction)
                    {
                        if (MenuManager.Instance.currentMenu.activeSelf.Equals(true))
                        {
                            if (this.gameObject.CompareTag("Fight").Equals(false))
                            {
                                if (immersiveHUDPanelBehaviour.IsSelected)
                                {
                                    // Debug.Log("Timer started");

                                    _unfillCoroutine ??= StartCoroutine(UnFillSlider());
                                }
                                else
                                {
                                    if (_unfillCoroutine != null)
                                    {
                                        StopCoroutine(_unfillCoroutine);
                                        _unfillCoroutine = null;
                                        menuOption.value = 1;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (immersiveHUDPanelBehaviour.IsSelected)
                            {
                                // Debug.Log("Timer started");

                                _unfillCoroutine ??= StartCoroutine(UnFillSlider());
                            }
                            else
                            {
                                if (_unfillCoroutine != null)
                                {
                                    StopCoroutine(_unfillCoroutine);
                                    _unfillCoroutine = null;
                                    menuOption.value = 1;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (MenuManager.Instance.currentMenu.activeSelf.Equals(true))
                    {
                        if (this.gameObject.CompareTag("HP").Equals(false))
                        {
                            if (immersiveHUDPanelBehaviour.IsSelected)
                            {
                                // Debug.Log("Timer started");
                                _unfillCoroutine ??= StartCoroutine(UnFillSlider());
                            }
                            else
                            {
                                if (_unfillCoroutine != null)
                                {
                                    StopCoroutine(_unfillCoroutine);
                                    _unfillCoroutine = null;
                                    menuOption.value = 1;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (immersiveHUDPanelBehaviour.IsSelected)
                        {
                            // Debug.Log("Timer started");
                            _unfillCoroutine ??= StartCoroutine(UnFillSlider());
                        }
                        else
                        {
                            if (_unfillCoroutine != null)
                            {
                                PlayerManager.Instance.isMoving = true;
                                StopCoroutine(_unfillCoroutine);
                                _unfillCoroutine = null;
                                menuOption.value = 1;
                            }
                        } 
                    }
                }
            }
            else
            {
                if (MenuManager.Instance.currentMenu.activeSelf.Equals(true))
                {
                    if (this.gameObject.CompareTag("HP").Equals(false))
                    {
                        if (immersiveHUDPanelBehaviour.IsSelected)
                        {
                            // Debug.Log("Timer started");
                            _unfillCoroutine ??= StartCoroutine(UnFillSlider());
                        }
                        else
                        {
                            if (_unfillCoroutine != null)
                            {
                                StopCoroutine(_unfillCoroutine);
                                _unfillCoroutine = null;
                                menuOption.value = 1;
                            }
                        }
                    }
                }
                else
                {
                    if (immersiveHUDPanelBehaviour.IsSelected)
                    {
                        Debug.Log("Timer started");
                        _unfillCoroutine ??= StartCoroutine(UnFillSlider());
                    }
                    else
                    {
                        if (_unfillCoroutine != null)
                        {
                            StopCoroutine(_unfillCoroutine);
                            _unfillCoroutine = null;
                            menuOption.value = 1;
                        }
                    }
                }
            }
        }
    }

    protected abstract void OnTimerComplete();
}
