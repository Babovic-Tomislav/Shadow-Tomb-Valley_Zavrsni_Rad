using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    public GameObject pausePanel;
    public Button back;
    public Button equipPanel;
    public Button usePanel;
    public static bool canPause=true;
    private int GamePaused=0;
    // Start is called before the first frame update


  

    void Start()
    {
        pausePanel.SetActive(false);
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape") && canPause)
        {
            

            if (GamePaused==0)
            {
                PauseGameplay();
                GamePaused = 1;
            }
            else
            {
                ContinueGame();
                GamePaused = 0;
            }
        }
    }

    private void PauseGameplay()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    private void ContinueGame()
    {
        equipPanel.onClick.Invoke();
        usePanel.onClick.Invoke();
        Time.timeScale = 1;
        back.onClick.Invoke();
        pausePanel.SetActive(false);
    }

}
