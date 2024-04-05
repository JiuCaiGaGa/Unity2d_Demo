using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMaskControl : MonoBehaviour
{
    SpriteRenderer sr;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        Debug.Log("Awake ������ִ�С�");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("����С�ϵ�");
        if (collision.tag == "Player")
        {
            Debug.Log("�����ϵ�");
            sr.color = new(sr.color.r, sr.color.g, sr.color.b, 0.3f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sr.color = new(sr.color.r, sr.color.g, sr.color.b, 1f);
    }
}
