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
    public Animator animator;

    private void Awake()
    {// 实例化 actions
        actions = new InputActions();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();// 获取 Player 上的 SpriteRenderer 组件
        animator = GetComponent<Animator>();
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
        /**/Debug.Log(direction);
        setAnimation();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.velocity = direction * speed;
        // 玩家的左右反转
        if (direction.x<0) // 向左
        {
            sr.flipX = true;
        }else if (direction.x>0) // 向右
        {
            sr.flipX=false;
        }
    }

    void setAnimation()
    {
        animator.SetFloat("Speed", rb.velocity.magnitude);// 向 rb 组件的速度向量大小赋值给 speed
    }
}
