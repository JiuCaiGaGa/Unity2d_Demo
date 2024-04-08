using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    [Header("��ս�����˺�")]
    public float meleeAttackDamage;

    [Header("��ս����")]
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
        //Debug.Log("���ǵ�" + isAttack + "�ι���.");

        AttackAreaPos = transform.position;
        offsetX = sr.flipX ? -Mathf.Abs(offsetX) : Mathf.Abs(offsetX);// ƫת

        AttackAreaPos.x += offsetX;
        AttackAreaPos.y += offsetY;

        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(AttackAreaPos, attackSize,0f, enemyLayer);
        //Collider2D[] hitColliders = Physics2D.OverlapBoxAll(AttackAreaPos, attackSize, 0f);

        Debug.Log("���");
        foreach(Collider2D hitCollider in hitColliders)
        {
            Debug.Log("����˸ߴ�"+ meleeAttackDamage * isAttack + "���˺���");
            hitCollider.GetComponent<Character>().TakeDamage(meleeAttackDamage * isAttack);
        }
    }

    // ���ڲ��ԵĻ�ͼ
    private void OnDrawGizmosSelected()
    {
        //Debug.Log("nimade");

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(AttackAreaPos,attackSize);
    }
}
