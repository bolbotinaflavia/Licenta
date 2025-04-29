using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public abstract class MenuCountdown : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public MenuCountdown instance;
    [FormerlySerializedAs("menu_option")] public UnityEngine.UI.Slider menuOption;
    public float timer = 5f;
    private Coroutine _unfillCoroutine;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

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
    private IEnumerator UnFillSlider()
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

    protected abstract void OnTimerComplete();
}
