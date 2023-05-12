using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckpointsCtrl : MonoBehaviour
{
    [SerializeField] protected List<Transform> checkpoints;
    [SerializeField] protected List<Vector3> checkpointsPos;

    protected void Reset()
    {
        this.LoadCheckpoints();
    }

    protected void Start()
    {
        this.LoadCheckpoints();
    }

    protected void LoadCheckpoints()
    {
        if (this.checkpoints.Count > 0) return; 

        foreach(Transform checkpoint in transform)
        {
            this.checkpoints.Add(checkpoint);
            this.checkpointsPos.Add(checkpoint.position);
        }
    }
}
