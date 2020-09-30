using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPosition : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {

        if (PlayerPrefs.GetString("_last_scene_") == "Menu")
            transform.position = new Vector2(3.5f, 3.5f);
        else
            transform.position = new Vector2(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"));

        



    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "PlayerHouse" && transform.position.x < 1f && transform.position.y > -4.5f && transform.position.x > 0.1f && transform.position.y < -3.7f)
        {
            PlayerPrefs.SetString("_last_scene_", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("Town", LoadSceneMode.Single);
            PlayerPrefs.SetFloat("x", -7.5f);
            PlayerPrefs.SetFloat("y", -12.5f);
        }
        else if (SceneManager.GetActiveScene().name == "Town" && transform.position.x > -7.7f && transform.position.x < -6.8f && transform.position.y > -12f && transform.position.y < -11.2f)
        {
            PlayerPrefs.SetString("_last_scene_", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("PlayerHouse", LoadSceneMode.Single);
            PlayerPrefs.SetFloat("x", 0.5f);
            PlayerPrefs.SetFloat("y", -3.5f);
        }
        else if (SceneManager.GetActiveScene().name == "Town" && transform.position.x < 12.7f && transform.position.x > 12.2f && transform.position.y < -4.8f && transform.position.y > -5f)
        {
            PlayerPrefs.SetString("_last_scene_", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("Inn", LoadSceneMode.Single);
            PlayerPrefs.SetFloat("x", 0.5f);
            PlayerPrefs.SetFloat("y", -5.5f);
        }
        else if (SceneManager.GetActiveScene().name == "Town" && transform.position.x > -0.6f && transform.position.x < 0.2f && transform.position.y > -17.5f && transform.position.y < -16.7f)
        {
            PlayerPrefs.SetString("_last_scene_", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("World", LoadSceneMode.Single);
            PlayerPrefs.SetFloat("x", -24.5f);
            PlayerPrefs.SetFloat("y", 10.5f);
        }
        else if (SceneManager.GetActiveScene().name == "World" && transform.position.x < -23.6f && transform.position.x > -25f && transform.position.y > 11.6f && transform.position.y < 12.3f)
        {
            PlayerPrefs.SetString("_last_scene_", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("Town", LoadSceneMode.Single);
            PlayerPrefs.SetFloat("x", -0.5f);
            PlayerPrefs.SetFloat("y", -16.5f);
        }
        else if (SceneManager.GetActiveScene().name == "World" && transform.position.x < 16.2f && transform.position.x > 15f && transform.position.y < -13.7f && transform.position.y > -14.4f)
        {
            PlayerPrefs.SetString("_last_scene_", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("Forest", LoadSceneMode.Single);
            PlayerPrefs.SetFloat("x", -24f);
            PlayerPrefs.SetFloat("y", -0.5f);
        }
        else if (SceneManager.GetActiveScene().name == "Forest" && transform.position.x > -25.5f && transform.position.x < -24.9f && transform.position.y > -2f && transform.position.y < 1f)
        {
            PlayerPrefs.SetString("_last_scene_", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("World", LoadSceneMode.Single);
            PlayerPrefs.SetFloat("x", 15.5f);
            PlayerPrefs.SetFloat("y", -13f);
        }
        else if (SceneManager.GetActiveScene().name == "Forest" && transform.position.x < 25.2f && transform.position.x > 24.4f && transform.position.y > 0.2f && transform.position.y < 1.2f)
        {
            SceneManager.LoadScene("Victory", LoadSceneMode.Single);
        }
        else if (SceneManager.GetActiveScene().name == "Inn" && transform.position.x < 1.2f && transform.position.x > 0.3f && transform.position.y < -5.7f && transform.position.y > -6f)
        {
            PlayerPrefs.SetString("_last_scene_", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("Town", LoadSceneMode.Single);
            PlayerPrefs.SetFloat("x", 12.5f);
            PlayerPrefs.SetFloat("y", -5.5f);
        }

    }


}
