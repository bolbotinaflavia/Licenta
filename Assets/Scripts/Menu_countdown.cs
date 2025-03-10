using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public abstract class Menu_countdown : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
   
    public UnityEngine.UI.Slider menu_option;
    public float timer = 5f;
    private Coroutine unfill_coroutine;
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Pointer Entered");
        StartTimer();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Pointer Exited");
        EndTimer();
    }
    public void StartTimer()
    {
        // Debug.Log("Timer started");
        if (unfill_coroutine == null)
            unfill_coroutine = StartCoroutine(UnFillSlider());

    }
    public void EndTimer()
    {
        // Debug.Log("Timer ended");
        if (unfill_coroutine != null)
        {
            StopCoroutine(unfill_coroutine);
            unfill_coroutine = null;
        }

        menu_option.value = 1;
    }
    private IEnumerator UnFillSlider()
    {
        float decRate = 0.3f / timer;

        while (menu_option.value > 0)
        {

            menu_option.value -= decRate * Time.deltaTime;

            yield return null;
        }

        menu_option.value = 0;
        unfill_coroutine = null;
        OnTimerComplete();
    }

    protected abstract void OnTimerComplete();
}
