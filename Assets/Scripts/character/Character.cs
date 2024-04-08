using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("属性")]
    [SerializeField] protected float maxHP;
    [SerializeField] protected float curHP;
    //protected float curHP;

    [Header("无敌")]
    public bool invulnerable;// 是否处于无敌状态
    public float invulnerableDuration;// 无敌时间
    //public float invulnerableTimer;// 无敌计时器

    public UnityEvent OnHurt; // 受伤事件
    public UnityEvent OnDeath; // 死亡事件
    protected virtual void OnEnable()
    {
        curHP = maxHP;
    }

    public virtual void TakeDamage(float damage)
    {
        if (invulnerable)return;// 处于无敌状态 
        curHP = curHP - damage>0?curHP-damage:0;
        if (curHP > 0)
        {
            StartCoroutine(nameof(InvulnerableCoroutine));// 启动无敌协程
            // 执行 受伤函数
            OnHurt?.Invoke();
        }else 
        { 
            Die();
        }
    }

    public virtual void Die()
    {
        //Destroy(this.gameObject);
        OnDeath?.Invoke();
    }


    // 协程
    protected virtual IEnumerator InvulnerableCoroutine()
    {
        invulnerable = true;
        // 等待无敌时间
        yield return new WaitForSeconds(invulnerableDuration);
        invulnerable = false;
    }
}
