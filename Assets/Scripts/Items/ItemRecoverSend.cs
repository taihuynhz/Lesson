using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRecoverSend : MyMonoBehaviour
{
    [SerializeField] protected RecoverItemsSO recoverItemsSO;
    [SerializeField] protected PlayerDamageReceiver playerDamageReceiver;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRecoverItemsSO();
        this.LoadReceiver();
    }

    protected virtual void LoadRecoverItemsSO()
    {
        if (this.recoverItemsSO != null) return;
        string resPath = "RecoverItems/" + transform.name;
        this.recoverItemsSO = Resources.Load<RecoverItemsSO>(resPath);
    }

    protected virtual void LoadReceiver()
    {
        if (this.playerDamageReceiver != null) return;
        this.playerDamageReceiver = GameObject.Find("PlayerCollider").transform.GetComponentInChildren<PlayerDamageReceiver>();
    }

    public virtual void SendRecover(DamageReceiver damageReceiver)
    {
        damageReceiver.Add(recoverItemsSO.hp, recoverItemsSO.capacity, recoverItemsSO.fuel);
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        this.SendRecover(playerDamageReceiver);
        this.Destroy();
    }

    protected virtual void Destroy()
    {
        Destroy(this.gameObject);
    }


}
