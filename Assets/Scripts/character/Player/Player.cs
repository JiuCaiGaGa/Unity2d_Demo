using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    [Header("近战攻击伤害")]
    public float meleeAttackDamage;

    [Header("近战攻击")]
    public Vector2 attackSize = new Vector2(1f, 1f);
    private Vector2 AttackAreaPos;
    
    public float offsetX = 0f;
    public float offsetY = 0f;

    public LayerMask enemyLayer;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void MeleeAttackAnimEvent(float isAttack)
    {
        //Debug.Log("这是第" + isAttack + "段攻击.");

        AttackAreaPos = transform.position;
        offsetX = sr.flipX ? -Mathf.Abs(offsetX) : Mathf.Abs(offsetX);// 偏转

        AttackAreaPos.x += offsetX;
        AttackAreaPos.y += offsetY;

        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(AttackAreaPos, attackSize,0f, enemyLayer);
        //Collider2D[] hitColliders = Physics2D.OverlapBoxAll(AttackAreaPos, attackSize, 0f);

        Debug.Log("你好");
        foreach(Collider2D hitCollider in hitColliders)
        {
            Debug.Log("造成了高达"+ meleeAttackDamage * isAttack + "的伤害！");
            hitCollider.GetComponent<Character>().TakeDamage(meleeAttackDamage * isAttack);
        }
    }

    // 用于测试的绘图
    private void OnDrawGizmosSelected()
    {
        //Debug.Log("nimade");

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(AttackAreaPos,attackSize);
    }
}
