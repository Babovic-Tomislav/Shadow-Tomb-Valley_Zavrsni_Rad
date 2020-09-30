using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAnim : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") < 0) anim.SetTrigger("Down");
        else if (Input.GetAxisRaw("Vertical") > 0) anim.SetTrigger("Up");
        else if (Input.GetAxisRaw("Horizontal") < 0) anim.SetTrigger("Left");
        else if (Input.GetAxisRaw("Horizontal") > 0) anim.SetTrigger("Right");
        else if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0) anim.SetTrigger("Stop");
    }
}
