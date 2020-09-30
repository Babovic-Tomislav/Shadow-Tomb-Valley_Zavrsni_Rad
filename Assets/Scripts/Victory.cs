using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class Victory : MonoBehaviour
{
    public TMP_Text time;
    public TMP_Text lvl;
    public TMP_Text enemiesKilled;
    public float clipTime;
    // Start is called before the first frame update
    void Start()
    {
        clipTime = Audio.audioPlayer.audioSource.time;
        Audio.audioPlayer.audioSource.clip = Resources.Load<AudioClip>("Audio/victory");
        Audio.audioPlayer.audioSource.Play();
        lvl.text = PlayerUnit.unitLvl.ToString();
        enemiesKilled.text = PlayerUnit.enemiesKilled.ToString();
        float timeToScale = PlayerUnit.time + Time.realtimeSinceStartup;
       
        int h = (int)(timeToScale / 3600);
        timeToScale -= h * 3600;
        
        int m = (int)(timeToScale  / 60);
        timeToScale -= m * 60;
      
        int s = (int)(timeToScale);
      
        time.text = h.ToString()+":" + m.ToString() +":"+ s.ToString();

    }

    public void PlayMainMusic()
    {
        Audio.audioPlayer.audioSource.clip = Resources.Load<AudioClip>("Audio/main");
        Audio.audioPlayer.audioSource.time = clipTime;
        Audio.audioPlayer.audioSource.Play();
    }
}
