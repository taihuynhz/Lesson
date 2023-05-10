using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] protected Transform player;
    [SerializeField] protected Vector3 cameraDistance;

    private void Reset()
    {
        this.GetPlayer();
    }

    protected void Start()
    {
        this.GetPlayer();
        this.GetDistance();
    }

    protected void LateUpdate()
    {
        Following();
    }

    protected virtual void Following()
    {
        transform.position = player.transform.position - player.transform.forward * -cameraDistance.z;
        transform.LookAt(player.transform.position);
        transform.position = new Vector3(transform.position.x, transform.position.y + cameraDistance.y, transform.position.z);
    }

    protected virtual void GetPlayer()
    {
        player = GameObject.Find("Player").transform;
    }

    protected virtual void GetDistance()
    {
        cameraDistance = transform.position - player.transform.position;
    }
}