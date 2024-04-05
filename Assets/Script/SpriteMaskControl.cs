using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMaskControl : MonoBehaviour
{
    SpriteRenderer sr;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        Debug.Log("Awake 函数已执行。");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("来了小老弟");
        if (collision.tag == "Player")
        {
            Debug.Log("来了老弟");
            sr.color = new(sr.color.r, sr.color.g, sr.color.b, 0.3f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sr.color = new(sr.color.r, sr.color.g, sr.color.b, 1f);
    }
}
