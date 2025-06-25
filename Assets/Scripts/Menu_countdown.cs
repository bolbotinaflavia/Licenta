using System;
using System.Collections;
using Battle;
#if !UNITY_WEBGL
using Eyeware.BeamEyeTracker.Unity;
#endif
using Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public abstract class MenuCountdown : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public MenuCountdown instance;
    [FormerlySerializedAs("menu_option")] public UnityEngine.UI.Slider menuOption;
    public Color baseColor;
    public float timer = 5f;
    private Coroutine _unfillCoroutine;
    private Action slider_clicked;
#if !UNITY_WEBGL
    public ImmersiveHUDPanelBehaviour immersiveHUDPanelBehaviour;
    #endif

    private void Awake()
    {
        if(instance == null)
            instance = this;
#if !UNITY_WEBGL
        immersiveHUDPanelBehaviour = GetComponent<ImmersiveHUDPanelBehaviour>();
        #endif
    }

    public void OnClicked()
    {
        if (PlayerMovement.Instance.CurrentControl.get_action().name.Equals("MouseMove")&&Player.PlayerMovement.Instance.CurrentControl.get_click_action().triggered)
        {
            OnTimerComplete();
            PlayerMovement.Instance.CurrentControl.get_click_action().Reset();
        }
        if (PlayerMovement.Instance.CurrentControl.get_action().name.Equals("KeyboardMove") && PlayerMovement.Instance.CurrentControl.get_click_action().triggered)
        {
            OnTimerComplete();
            PlayerMovement.Instance.CurrentControl.load_sliders();
            PlayerMovement.Instance.CurrentControl.get_click_action().Reset();
        }
        StartCoroutine(Deselect());
    }
    public IEnumerator Deselect()
    {
        yield return null;
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            Debug.Log(EventSystem.current.currentSelectedGameObject.name);
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
     // if (PlayerMovement.Instance.CurrentControl.get_action().name.Equals("EyeMove"))
     //          {
     //              OnTimerComplete();
     //              PlayerMovement.Instance.CurrentControl.get_click_action().Reset();
     //          }
     //          PlayerMovement.Instance.CurrentControl.get_click_action().Reset();
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
#if !UNITY_WEBGL
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
                                PlayerManager.Instance.IsMoving = true;
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
        #endif
    }

    protected abstract void OnTimerComplete();
}
