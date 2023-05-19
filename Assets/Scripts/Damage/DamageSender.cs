using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : MyMonoBehaviour
{   
    [SerializeField] protected int damage = 5;
    [SerializeField] protected PlayerDamageReceiver playerDamageReceiver;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadReceiver();
    }

    protected virtual void LoadReceiver()
    {
        if (this.playerDamageReceiver != null) return;
        this.playerDamageReceiver = GameObject.Find("PlayerCollider").transform.GetComponentInChildren<PlayerDamageReceiver>();
    }

    public virtual void Send(Transform obj)
    {
        DamageReceiver damageReceiver = obj.GetComponentInChildren<DamageReceiver>();
        if (damageReceiver == null) return;
        this.Send(damageReceiver);
    }

    public virtual void Send(DamageReceiver damageReceiver)
    {
        damageReceiver.Deduct(this.damage);
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        this.Send(playerDamageReceiver);
    }
}
