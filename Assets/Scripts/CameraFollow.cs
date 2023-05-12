using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow: MonoBehaviour
{
    [SerializeField] protected Transform target;
    protected Vector3 cameraDistance;

    private void Reset()
    {
        this.GetPlayer();
    }

    private void Awake()
    {
        this.GetPlayer();
        this.GetDistance();
    }

    protected void LateUpdate()
    {
        this.Following();
    }

    protected virtual void Following()
    {
        transform.position = target.transform.position + target.transform.forward * cameraDistance.z;
        transform.LookAt(target.transform.position);
        transform.position = new Vector3(transform.position.x, transform.position.y + cameraDistance.y, transform.position.z);
    }

    protected virtual void GetPlayer()
    {
        target = GameObject.Find("Player").transform;
    }

    protected virtual void GetDistance()
    {
        cameraDistance = transform.position - target.transform.position;
    }
}