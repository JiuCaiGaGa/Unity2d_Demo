using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D rb;

    Animator animator;

    Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Debug.Log("Update"+Time.deltaTime);
    }
    private void FixedUpdate()
    {
        //Debug.Log("FixedUpdate" + Time.deltaTime);
        //��ȡ�������
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        //Debug.Log("moveX:"+moveX+",moveY:"+moveY);

        animator.SetFloat("Horizontal",moveX);
        animator.SetFloat("Vertical", moveY);
        animator.SetFloat("Speed", moveDirection.sqrMagnitude);


        //�����ƶ�����
        moveDirection = new Vector2(moveX, moveY).normalized;

        //Ӧ���ƶ�
        rb.velocity = moveDirection * moveSpeed;
    }
}
