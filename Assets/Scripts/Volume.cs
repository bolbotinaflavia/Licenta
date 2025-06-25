
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeManager : MonoBehaviour
{
    public static VolumeManager Instance;
    public List<AudioSource> audioSource = new List<AudioSource>();
    public UnityEngine.Rendering.Volume  globalVolume;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        globalVolume = GetComponent<UnityEngine.Rendering.Volume>();
        if (globalVolume == null)
        {
            Debug.LogError("globalVolume component is missing.");
        }

        try
        {
            audioSource = GameObject.FindObjectsOfType<AudioSource>().ToList();
        }
        catch (Exception e)
        {
            Debug.LogError("Error finding AudioSources: " + e);
            throw;
        }
    }

    private void Update()
    {
        if (globalVolume == null)
        {
            Debug.LogError("globalVolume is null in Update.");
            return;
        }

        try
        {
            audioSource.RemoveAll(a => a == null);
            audioSource = GameObject.FindObjectsOfType<AudioSource>().ToList();
        }
        catch (Exception e)
        {
            Debug.LogError("Error updating AudioSources: " + e);
            throw;
        }

        try
        {
            if (audioSource.Count > 0)
            {
                foreach (var source in audioSource)
                {
                    if (source != null)
                    {
                        source.volume = globalVolume.weight;
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error setting volume: " + e);
            throw;
        }
    }

    public void Increase()
    {
        if (globalVolume == null)
        {
            Debug.LogError("globalVolume is null in Increase.");
            return;
        }

        if (globalVolume.weight * 100 < 100)
        {
            globalVolume.weight += 0.10f;
            globalVolume.weight = Mathf.Round(globalVolume.weight * 100) / 100f;
        }
    }

    public void Decrease()
    {
        if (globalVolume == null)
        {
            Debug.LogError("globalVolume is null in Decrease.");
            return;
        }

        if (globalVolume.weight * 100 > 0)
        {
            globalVolume.weight -= 0.10f;
            globalVolume.weight = Mathf.Round(globalVolume.weight * 100) / 100f;
        }
    }
}
