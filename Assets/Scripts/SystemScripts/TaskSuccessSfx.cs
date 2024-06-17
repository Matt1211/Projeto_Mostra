using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSuccessSfx : MonoBehaviour
{
    public static TaskSuccessSfx instance;
    public AudioClip taskSuccessClip;
    private AudioSource taskAudioSource;
    public GameObject taskSuccessPanel;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        InitialSetup();
    }

    private void InitialSetup()
    {
        taskAudioSource = GetComponent<AudioSource>();
        taskAudioSource.clip = taskSuccessClip;
    }

    // Update is called once per frame
    public void PlayTaskSuccessSfx()
    {
        taskAudioSource.Play();
    }
}
