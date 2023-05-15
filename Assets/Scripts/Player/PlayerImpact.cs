using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpact : MonoBehaviour
{
    private static PlayerImpact instance;
    public static PlayerImpact Instance => instance;

    [Header("===== Components =====")]
    [SerializeField] protected MeshCollider playerCollider;

    protected void Reset()
    {
        this.LoadComponents();
    }

    protected void Awake()
    {
        this.LoadComponents();
    }

    protected virtual void LoadComponents()
    {
        this.LoadCollider();
    }

    protected virtual void LoadCollider()
    {
        if (this.playerCollider != null) return;
        this.playerCollider = GetComponent<MeshCollider>();
        this.playerCollider.convex = true;
    }
}
