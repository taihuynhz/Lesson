using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarModelPanel : MonoBehaviour
{
    [SerializeField] protected float rotationSpeed = 0.5f;
    public Vector3 rotationAxis = Vector3.up;
    protected float leftMouseInput;
    protected float rightMouseInput;

    protected void Update()
    {
        this.Rotate();
    }

    protected void Rotate()
    {
        // Get the mouse position
        Vector2 mousePosition = Input.mousePosition;

        // Get mouse input
        leftMouseInput = Input.GetAxis("Fire1");
        rightMouseInput = Input.GetAxis("Fire2");

        // Calculate the amount of rotation to apply
        float rotationAmount = (mousePosition.x - Screen.width / 2) * rotationSpeed * Time.deltaTime;

        // Rotate the object
        if (leftMouseInput == 1)
            transform.Rotate(rotationAxis, rotationAmount);
        if (rightMouseInput == 1)
            transform.Rotate(rotationAxis, -rotationAmount);
    }
}