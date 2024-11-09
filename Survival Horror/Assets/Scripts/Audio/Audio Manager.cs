using FMODUnity;
using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInstances;

    public static AudioManager instance { get; private set; }

    private EventInstance menuEventInstance;
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one AudioManager instance in this scene.");
        }
        instance = this;

        eventInstances = new List<EventInstance>();
    }

    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }
  
    public void PlayOneShot(EventReference sound)
    {
        RuntimeManager.PlayOneShot(sound);
    }

    private void InitializeMenuSong(EventReference menuEventInstance)
    {
        //menuEventInstance = CreateInstance
    }

    private void CleanUp()
    {
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
    }

    private void OnDestroy()
    {
        CleanUp();
    }
}
