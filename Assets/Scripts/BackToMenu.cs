using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void returnToMenu()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
