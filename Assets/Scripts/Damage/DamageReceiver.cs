using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageReceiver : MyMonoBehaviour
{
    [Header("===== Player Stats =====")]
    [SerializeField] protected CapsuleCollider playerCollider;
    [SerializeField] protected float hp = 100;
    [SerializeField] protected float hpMax = 100;
    [SerializeField] protected float fuel = 100;
    [SerializeField] protected float capacity = 100;
    [SerializeField] protected bool isDead = false;

    protected override void OnEnable()
    {
        this.Reborn();
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.Reborn();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
    }

    protected virtual void LoadCollider()
    {
        if (this.playerCollider != null) return;
        this.playerCollider = GetComponent<CapsuleCollider>();
    }

    public virtual void Reborn()
    {
        this.hp = this.hpMax;
        this.isDead = false;
    }

    public virtual void Add(float hp, float capacity, float fuel)
    {
        if (this.isDead) return;

        this.hp += hp;
        if (this.hp > this.hpMax) this.hp = this.hpMax;

        this.capacity += capacity;

        this.fuel += fuel;
        if (this.fuel > this.capacity) this.fuel = this.capacity;
    }

    public virtual void Deduct(int deduct)
    {
        if (this.isDead) return;

        this.hp -= deduct;
        if (this.hp < 0) this.hp = 0;
        this.CheckIsDead();
    }

    protected virtual bool IsDead()
    {
        return this.hp <= 0;
    }

    protected virtual void CheckIsDead()
    {
        if (!this.IsDead()) return;
        this.isDead = true;
        this.OnDead();
    }
    protected abstract void OnDead();
}
