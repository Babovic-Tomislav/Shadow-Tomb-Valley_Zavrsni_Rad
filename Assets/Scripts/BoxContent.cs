using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxContent : MonoBehaviour
{
    public ItemObject item;
    public int amount = 1;

    private void Start()
    {
        if (DestroyOnLoad.box)
            Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && Input.GetButtonDown("Submit"))
        {
            Inventory.AddItem(item);
            DestroyOnLoad.box = true;
            Destroy(gameObject);
        }
    }

}
