using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public InputActions actions;
    public Vector2 direction;
    public float speed;
    public Rigidbody2D rb;
    public SpriteRenderer sr; 

    private void Awake()
    {// ʵ���� actions
        actions = new InputActions();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();// ��ȡ Player �ϵ� SpriteRenderer ���
    }
    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }

    private void Update()
    {
        direction = actions.Gameplay.Move.ReadValue<Vector2>();
        Debug.Log(direction);
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.velocity = direction * speed;
        // ��ҵ����ҷ�ת
        if (direction.x<0) // ����
        {
            sr.flipX = true;
        }else if (direction.x>0) // ����
        {
            sr.flipX=false;
        }
    }
}
