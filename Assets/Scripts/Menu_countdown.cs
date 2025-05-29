using System;
using System.Collections;
using Battle;
using Eyeware.BeamEyeTracker;
using Eyeware.BeamEyeTracker.Unity;
using Movement;
using Player;
using UnityEngine;
using UnityEngine.Assertions.Must;
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

        if (PlayerMovement.Instance.CurrentControl.get_action().name.Equals("MouseMove") &&
            PlayerMovement.Instance.CurrentControl.get_click_action().IsPressed())
        {
            Debug.Log("Clicked by mouse and all");
            OnTimerComplete();
            PlayerMovement.Instance.CurrentControl.get_click_action().Reset();
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
            if (BattleSystem.Instance != null)
            {
                if (BattleSystem.Instance.State == BattleState.PlayerMove)
                {
                    if (immersiveHUDPanelBehaviour.IsSelected == true)
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
                if (immersiveHUDPanelBehaviour.IsSelected == true)
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

    protected abstract void OnTimerComplete();
}
