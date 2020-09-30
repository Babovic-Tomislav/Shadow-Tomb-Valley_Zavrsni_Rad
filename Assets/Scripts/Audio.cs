using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Audio : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    public static Audio audioPlayer;
    private void Awake()
    {
        if (audioPlayer == null)
        {
            audioPlayer = this;
            DontDestroyOnLoad(this);
            audioSource.Play();
        }
        else
        {
            Destroy(this);
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Screen.SetResolution(960, 540, true);
        
    }

    public void ChangeVolume(float volume)
    {
        audioSource.volume = volume;
        
    }

    private void Update()
    {
        
    }


}
