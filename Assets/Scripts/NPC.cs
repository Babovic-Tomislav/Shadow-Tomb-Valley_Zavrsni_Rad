using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public abstract class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;

    public List<string> dialogue;

    public bool triggerNpc = false;


    private void Awake()
    {
        dialogue = new List<string>();
    }

    private void Update()
    {

    }





    public virtual void SetDialogue(int i = 0)
    {
        PauseGame.canPause = false;
        if (dialoguePanel.activeInHierarchy == false)
            dialoguePanel.SetActive(true);




    }

    public void DeactivateDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        Time.timeScale = 1;
        PauseGame.canPause = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.triggerNpc = true;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            this.triggerNpc = true;

        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.triggerNpc = false;
        }

    }

    public virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (this.triggerNpc && Input.GetButtonDown("Submit") && 
            collision.collider.CompareTag("Player"))
        {
            Time.timeScale = 0;
            StartCoroutine(SetChat());
        }
    }

   IEnumerator SetChat()
    {
        SetDialogue();
        yield return new WaitForSecondsRealtime(0.5f);
        int i = 1;
        while(i<dialogue.Count)
        {
           
            while(!Input.GetKeyDown(KeyCode.Space))
            {
                yield return null;
            }
            SetDialogue(i);
            
            i++;
            yield return null;
        }
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }
        EndDialogue();
        PlayerUnit.bossFight = true;
        PlayerPrefs.SetFloat("x", gameObject.transform.position.x);
        PlayerPrefs.SetFloat("y", gameObject.transform.position.y);
        PlayerPrefs.SetString("_last_scene_", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Battle1", LoadSceneMode.Single);
    }

  

}