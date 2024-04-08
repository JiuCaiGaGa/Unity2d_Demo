using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public InputActions actions;
    public Vector2 direction;
    public float movespeed = 1f; // 步行/奔跑移动速度
    public float attackspeed = 0.3f;// 攻击时移速
    public float speed;// 实时速度

    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Animator animator;
    [Header("近战攻击")]
    public bool isMeleeAttack;
    [Header("闪避")]
    public bool isDodging = false;
    public float dodgeForce;
    public float dodgeDuration = 0f;// 闪避的持续时间
    public float dodgeTimer = 0f;// 计时器
    public float dodgeCooldown = 2f;// 设置两秒的冷却
    private bool isDodgeCooldown = false;// 不在冷却

    private void Awake()
    {// 实例化 actions
        actions = new InputActions();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();// 获取 Player 上的 SpriteRenderer 组件
        animator = GetComponent<Animator>();

        // 近战攻击
        actions.Gameplay.MeleeAttack.started += MeleeAttack;

        // 闪避
        actions.Gameplay.Dodge.started += Dodge;
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
        /*Debug.Log(direction);*/
        setAnimation();
    }

    private void FixedUpdate()
    {
        // 玩家移动
        Move();
        // 玩家闪避
        Dodge();
    }

    void Move()
    {

        speed = isMeleeAttack ? attackspeed : movespeed; 

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
    /*
        攻击
     */
    private void MeleeAttack(InputAction.CallbackContext context)
    {
        if (!isDodging)
        {// 闪避状态下不能攻击
            animator.SetTrigger("meleeAttack");
            isMeleeAttack = true;
        }

    }
    /*
        闪避
     */

    private void Dodge(InputAction.CallbackContext context)
    {
        //animator.SetBool("isDodge", true);
        //Debug.Log("已触发闪避");
        if(!isDodging && !isDodgeCooldown)
        {
            isDodging = true;
            isMeleeAttack=false;
        }
    }

    void Dodge()
    {
        if (isDodgeCooldown)// 不可以触发闪避
        {
            dodgeTimer += Time.fixedDeltaTime;
            if (dodgeTimer >= dodgeCooldown)// 冷却结束
            {
                isDodgeCooldown = false;
                dodgeTimer = 0f;
            }
        }
        if (!isDodgeCooldown && isDodging)
            //if (isDodge)
        {
            if (dodgeTimer <= dodgeDuration)
            {//
                rb.AddForce(direction * dodgeForce,ForceMode2D.Impulse);
                dodgeTimer += Time.fixedDeltaTime;// 加上一个固定时间间隔
            }else
            {// 闪避已完成
                isDodging = false;
                isDodgeCooldown=true;
                dodgeTimer = 0f;
            }
        }
    }
    void setAnimation()
    {
        animator.SetFloat("speed", rb.velocity.magnitude);// 向 rb 组件的速度向量大小赋值给 speed
        animator.SetBool("isAttack", isMeleeAttack);
        animator.SetBool("isDodge", isDodging);
        //Debug.Log("isAttack :" +isMeleeAttack);
    }
}
