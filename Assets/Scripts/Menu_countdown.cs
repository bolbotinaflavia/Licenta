using System;
using System.Collections;
using Battle;
#if !UNITY_WEBGL
#if !UNITY_WEBGL
using Eyeware.BeamEyeTracker.Unity;
#endif
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
    private Coroutine _unfill;
    private Action slider_clicked;
#if !UNITY_WEBGL
#if !UNITY_WEBGL
    public ImmersiveHUDPanelBehaviour immersiveHUDPanelBehaviour;
    #endif
    #endif

    private void Awake()
    {
        if(instance == null)
            instance = this;
#if !UNITY_WEBGL
#if !UNITY_WEBGL
        immersiveHUDPanelBehaviour = GetComponent<ImmersiveHUDPanelBehaviour>();
        #endif
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

     //          {
     //              OnTimerComplete();
     //              PlayerMovement.Instance.CurrentControl.get_click_action().Reset();
     //          }
     //          PlayerMovement.Instance.CurrentControl.get_click_action().Reset();
    public void OnPointerEnter(PointerEventData eventData)
    {
        menuOption.value = 1;
        start_timer();
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Pointer Exited");
        end_timer();
    }
    // ReSharper disable Unity.PerformanceAnalysis
    public void start_timer()
    {
        menuOption.value = 1;
        _unfill ??= StartCoroutine(unfill());
    }
    public void end_timer()
    {
        if (_unfill != null)
        {
            StopCoroutine(_unfill);
            _unfill = null;
        }

        menuOption.value = 1;
    }
    public IEnumerator unfill()
    {
        float dec = 0.3f / timer;
        
        while (menuOption.value > 0)
        {
            menuOption.value -= dec * Time.deltaTime;

            yield return null;
        }

        menuOption.value = 0;
        _unfill = null;
        OnTimerComplete();
    }

    private void Update()
    {
#if !UNITY_WEBGL
#if !UNITY_WEBGL
        if (PlayerMovement.Instance.CurrentControl.get_action().name.Equals("EyeMove"))
        {
            if (BattleSystem.Instance != null && GameController.Instance != null)
            {
                if (GameController.Instance.state.Equals(GameState.Battle))
                {
                    if (BattleSystem.Instance.State == BattleState.PlayerAction)
                    {
                        if (MenuManager.Instance.current.activeSelf.Equals(true))
                        {
                            if (this.gameObject.CompareTag("Fight").Equals(false))
                            {
                                if (immersiveHUDPanelBehaviour.IsSelected)
                                {
                                    // Debug.Log("Timer started");

                                    _unfill ??= StartCoroutine(unfill());
                                }
                                else
                                {
                                    if (_unfill!= null)
                                    {
                                        StopCoroutine(_unfill);
                                        _unfill= null;
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

                                _unfill??= StartCoroutine(unfill());
                            }
                            else
                            {
                                if (_unfill != null)
                                {
                                    StopCoroutine(_unfill);
                                    _unfill = null;
                                    menuOption.value = 1;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (MenuManager.Instance.current.activeSelf.Equals(true))
                    {
                        if (this.gameObject.CompareTag("HP").Equals(false))
                        {
                            if (immersiveHUDPanelBehaviour.IsSelected)
                            {
                                // Debug.Log("Timer started");
                                _unfill??= StartCoroutine(unfill());
                            }
                            else
                            {
                                if (_unfill!= null)
                                {
                                    StopCoroutine(_unfill);
                                    _unfill= null;
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
                            _unfill ??= StartCoroutine(unfill());
                        }
                        else
                        {
                            if (_unfill!= null)
                            {
                                PlayerManager.Instance.IsMoving = true;
                                StopCoroutine(_unfill);
                                _unfill= null;
                                menuOption.value = 1;
                            }
                        } 
                    }
                }
            }
            else
            {
                if (MenuManager.Instance.current.activeSelf.Equals(true))
                {
                    if (this.gameObject.CompareTag("HP").Equals(false))
                    {
                        if (immersiveHUDPanelBehaviour.IsSelected)
                        {
                            // Debug.Log("Timer started");
                            _unfill??= StartCoroutine(unfill());
                        }
                        else
                        {
                            if (_unfill != null)
                            {
                                StopCoroutine(_unfill);
                                _unfill= null;
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
                        _unfill??= StartCoroutine(unfill());
                    }
                    else
                    {
                        if (_unfill != null)
                        {
                            StopCoroutine(_unfill);
                            _unfill = null;
                            menuOption.value = 1;
                        }
                    }
                }
            }
        }
        #endif
        #endif
    }

    protected abstract void OnTimerComplete();
}
