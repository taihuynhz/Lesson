using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    [SerializeField] protected float speed = 15f;
    [SerializeField] protected float rotateSpeed = 50f;

    protected void Update()
    {
        this.CarMovement();
        this.CarRotate();
    }

    protected virtual void CarMovement()
    {
        transform.Translate(InputManager.Instance.Vertical * Vector3.forward * Time.deltaTime * speed);
    }

    protected virtual void CarRotate()
    {
        transform.Rotate(InputManager.Instance.Horizontal * Vector3.up * Time.deltaTime * rotateSpeed);
    }
}
