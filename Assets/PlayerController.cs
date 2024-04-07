using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public InputActions actions;
    public Vector2 direction;
    public float movespeed = 1f; // ����/�����ƶ��ٶ�
    public float attackspeed = 0.3f;// ����ʱ����
    public float speed;// ʵʱ�ٶ�

    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Animator animator;
    [Header("��ս����")]
    public bool isMeleeAttack;
    [Header("����")]
    public bool isDodging = false;
    public float dodgeForce;
    public float dodgeDuration = 0f;// ���ܵĳ���ʱ��
    public float dodgeTimer = 0f;// ��ʱ��
    public float dodgeCooldown = 2f;// �����������ȴ
    private bool isDodgeCooldown = false;// ������ȴ

    private void Awake()
    {// ʵ���� actions
        actions = new InputActions();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();// ��ȡ Player �ϵ� SpriteRenderer ���
        animator = GetComponent<Animator>();

        // ��ս����
        actions.Gameplay.MeleeAttack.started += MeleeAttack;

        // ����
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
        // ����ƶ�
        Move();
        // �������
        Dodge();
    }

    void Move()
    {

        speed = isMeleeAttack ? attackspeed : movespeed; 

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
    /*
        ����
     */
    private void MeleeAttack(InputAction.CallbackContext context)
    {
        if (!isDodging)
        {// ����״̬�²��ܹ���
            animator.SetTrigger("meleeAttack");
            isMeleeAttack = true;
        }

    }
    /*
        ����
     */

    private void Dodge(InputAction.CallbackContext context)
    {
        //animator.SetBool("isDodge", true);
        //Debug.Log("�Ѵ�������");
        if(!isDodging && !isDodgeCooldown)
        {
            isDodging = true;
            isMeleeAttack=false;
        }
    }

    void Dodge()
    {
        if (isDodgeCooldown)// �����Դ�������
        {
            dodgeTimer += Time.fixedDeltaTime;
            if (dodgeTimer >= dodgeCooldown)// ��ȴ����
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
                dodgeTimer += Time.fixedDeltaTime;// ����һ���̶�ʱ����
            }else
            {// ���������
                isDodging = false;
                isDodgeCooldown=true;
                dodgeTimer = 0f;
            }
        }
    }
    void setAnimation()
    {
        animator.SetFloat("speed", rb.velocity.magnitude);// �� rb ������ٶ�������С��ֵ�� speed
        animator.SetBool("isAttack", isMeleeAttack);
        animator.SetBool("isDodge", isDodging);
        //Debug.Log("isAttack :" +isMeleeAttack);
    }
}
