using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;


public class bgmScript : MonoBehaviour
{
    // temporary solution to the audio not playing issue. Now it's a singleton that will persist between all scenes. 
    public static bgmScript instance;

    FMOD.Studio.EventInstance Ambience;
    FMOD.Studio.EventInstance Music;

    public EventReference AmbienceEvent;

    public EventReference MusicEvent;

    FMOD.Studio.PARAMETER_ID musicParameterId;
    FMOD.Studio.PARAMETER_ID resetParameterId;

    private float timer;
    // Start is called before the first frame update
    void Awake()
    {
        // make this a singleton + dontdestroyonload
        if (instance == null)
        {
            instance = this;
        }
        else if (instance!= null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        // starts playing music
        Debug.Log("beginning to play music");
        Ambience = RuntimeManager.CreateInstance(AmbienceEvent);
        Ambience.start();

        Music = RuntimeManager.CreateInstance(MusicEvent);
        Music.start();

        FMOD.Studio.EventDescription musicEventDescription;
        Music.getDescription(out musicEventDescription);

        FMOD.Studio.PARAMETER_DESCRIPTION musicParameterDescription;
        musicEventDescription.getParameterDescriptionByName("Music progression", out musicParameterDescription);
        musicParameterId = musicParameterDescription.id;

        FMOD.Studio.PARAMETER_DESCRIPTION resetParameterDescription;
        musicEventDescription.getParameterDescriptionByName("Reset", out resetParameterDescription);
        resetParameterId = resetParameterDescription.id;
    }

    void Update()
    {
        // timer += Time.deltaTime;
        // if (timer >= 2f)
        // {
        //     Debug.Log(GetParameter());
        //     timer = 0;
        // }
    }

    public void SetParameter(float index)
    {
    //  Debug.Log("i have been called!!");
        Music.setParameterByID(musicParameterId, index);
    }

    public float GetParameter()
    {
        float id;
        Music.getParameterByID(musicParameterId, out id);
        return id;
    }

    public void Reset()
    {
        Music.setParameterByID(resetParameterId, 1);
    }

}
