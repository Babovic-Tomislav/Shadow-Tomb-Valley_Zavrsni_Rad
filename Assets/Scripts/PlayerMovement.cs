using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2.0f;
    
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
   
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

        

        gameObject.transform.position = new Vector2(transform.position.x + (h * speed * Time.deltaTime),
           transform.position.y + (v * speed * Time.deltaTime));
        int rint = UnityEngine.Random.Range(0, 400);
        if (rint == 2)
            if ((SceneManager.GetActiveScene().name == "World" || SceneManager.GetActiveScene().name == "Forest") && (h != 0 || v != 0))
            {
                PlayerPrefs.SetFloat("x", transform.position.x);
                PlayerPrefs.SetFloat("y", transform.position.y);
                PlayerPrefs.SetString("_last_scene_", SceneManager.GetActiveScene().name);
                SceneManager.LoadScene("Battle1", LoadSceneMode.Single);
            }
    }

  

}
