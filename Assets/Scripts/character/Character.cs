using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("����")]
    [SerializeField] protected float maxHP;
    [SerializeField] protected float curHP;
    //protected float curHP;

    [Header("�޵�")]
    public bool invulnerable;// �Ƿ����޵�״̬
    public float invulnerableDuration;// �޵�ʱ��
    //public float invulnerableTimer;// �޵м�ʱ��


    protected virtual void OnEnable()
    {
        curHP = maxHP;
    }

    public virtual void TakeDamage(float damage)
    {
        if (invulnerable)return;// �����޵�״̬ 
        curHP = curHP - damage>0?curHP-damage:0;
        StartCoroutine(nameof(InvulnerableCoroutine));// �����޵�Э��
        if(curHP == 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(this.gameObject);
    }


    // Э��
    protected virtual IEnumerator InvulnerableCoroutine()
    {
        invulnerable = true;
        // �ȴ��޵�ʱ��
        yield return new WaitForSeconds(invulnerableDuration);
        invulnerable = false;
    }
}
