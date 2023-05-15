using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("===== Drive Mode =====")]
    [SerializeField] protected bool onAutomatic;
    [SerializeField] protected bool onManual;

    [Header("===== Car =====")]
    [SerializeField] protected float maxAcceleration = 30.0f;
    [SerializeField] protected float brakeAcceleration = 50.0f;
    [SerializeField] protected float turnSensitivity = 1.0f;
    [SerializeField] protected float maxSteerAngle = 30.0f;
    [SerializeField] protected float autoSpeed = 15f;
    [SerializeField] protected float autoRotateSpeed = 2.5f;
    [SerializeField] protected List<Wheel> wheels;

    [Header("===== Checkpoints =====")]
    [SerializeField] protected int currentPoint = 0;
    [SerializeField] protected float minDistance = 0.1f;
    [SerializeField] protected List<Transform> checkpoints;
    [SerializeField] protected List<Vector3> checkpointsPos;

    [Serializable] protected struct Wheel { public UnityEngine.WheelCollider wheelCollider; public Axel axel; }
    protected enum Axel { Front, Rear }
    protected enum DriveMode { Manual, Automatic }
    protected DriveMode mode = DriveMode.Manual;

    protected new Rigidbody rigidbody;

    protected void Reset()
    {
        this.ResetValue();
        this.LoadComponents();
        this.LoadCheckpoints();
    }

    protected void Awake()
    {
        this.LoadComponents();
        this.LoadCheckpoints();
    }

    private void Start()
    {
        this.rigidbody.centerOfMass = Vector3.zero;
    }

    protected void FixedUpdate()
    {
        this.CarMode();
    }

    protected virtual void ResetValue()
    {
        this.maxAcceleration = 30.0f;
        this.brakeAcceleration = 50.0f;
        this.turnSensitivity = 1.0f;
        this.maxSteerAngle = 30.0f;
        this.autoSpeed = 15f;
        this.autoRotateSpeed = 2.5f;
    }

    protected virtual void LoadComponents()
    {
        this.LoadRigidbody();
    }

    protected virtual void LoadRigidbody()
    {
        if (this.rigidbody != null) return;
        this.rigidbody = GetComponent<Rigidbody>();
        this.rigidbody.mass = 500f;
        this.rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    protected virtual void LoadCheckpoints()
    {
        if (this.checkpoints.Count > 0) return;

        foreach (Transform checkpoint in transform.Find("Checkpoints"))
        {
            this.checkpoints.Add(checkpoint);
            this.checkpointsPos.Add(checkpoint.position);
        }
    }

    protected virtual void CarMode()
    {
        if (InputManager.Instance.Auto) mode = DriveMode.Automatic;
        else if (InputManager.Instance.Manual) mode = DriveMode.Manual;

        if (mode == DriveMode.Automatic)
        {
            this.onAutomatic = true;
            this.onManual = false;
            this.CarAutoMode();
        }
        else if (mode == DriveMode.Manual)
        {
            this.onAutomatic = false;
            this.onManual = true;
            this.CarManualMode();
        }
    }

    protected virtual void CarManualMode()
    {
        // Move
        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = InputManager.Instance.Vertical * 600 * maxAcceleration * Time.deltaTime;
        }

        // Steer
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var _steerAngle = InputManager.Instance.Horizontal * turnSensitivity * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
            }
        }

        // Brake
        if (InputManager.Instance.Brake || InputManager.Instance.Vertical == 0)
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 300 * brakeAcceleration * Time.deltaTime;
            }
        }
        else
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }
        }
    }

    protected virtual void CarAutoMode()
    {
        transform.position = Vector3.MoveTowards(transform.position, checkpointsPos[currentPoint], Time.deltaTime * autoSpeed);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, checkpointsPos[currentPoint] - transform.position, Time.deltaTime * autoRotateSpeed, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

        if (Vector3.Distance(checkpointsPos[currentPoint], transform.position) < minDistance) currentPoint++;
        if (currentPoint == checkpoints.Count) currentPoint = 0;
    }
}
