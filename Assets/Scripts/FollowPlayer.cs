using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FollowPlayer : MonoBehaviour
{
    protected Transform player;
    protected Vector3 cameraDistance;

    private void Reset()
    {
        this.GetPlayer();
    }

    private void Start()
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
        transform.position = player.transform.position + player.transform.forward * cameraDistance.z;
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