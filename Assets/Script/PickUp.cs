using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public AudioClip SFX;// “Ù–ß
    public GameObject VFX;// Ãÿ–ß

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PropManager manager = collision.GetComponent<PropManager>();
        if (manager)
        {
            bool pickedUp = manager.PickupItem(gameObject);
            if (pickedUp) RemoveItem();
        }
    }

    private void RemoveItem()
    {
        AudioSource.PlayClipAtPoint(SFX,transform.position);
        Instantiate(VFX,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
}