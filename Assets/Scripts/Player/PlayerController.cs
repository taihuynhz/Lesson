using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MyMonoBehaviour
{
    private static PlayerController instance;
    public static PlayerController Instance => instance;

    [Header("===== Drive Mode =====")]
    [SerializeField] protected bool onAutomatic;
    [SerializeField] protected bool onManual;

    [Header("===== Car =====")]
    [SerializeField] protected float maxAcceleration = 30.0f;
    public float MaxAcceleration => maxAcceleration;
    [SerializeField] protected float brakeAcceleration = 50.0f;
    [SerializeField] protected float turnSensitivity = 1.0f;
    [SerializeField] protected float maxSteerAngle = 30.0f;
    [SerializeField] protected float autoSpeed = 15f;
    [SerializeField] protected float autoRotateSpeed = 2.5f;
    [SerializeField] protected List<Wheel> wheels;

    [Header("===== Checkpoints =====")]
    [SerializeField] protected int laps = 0;
    public int Laps => laps;

    [SerializeField] protected int currentPoint = 0;
    public int CurrentPoint => currentPoint;
    [SerializeField] protected float minDistanceAuto = 0.1f;
    [SerializeField] protected float minDistanceManual = 10f;
    [SerializeField] protected List<Transform> checkpoints;
    [SerializeField] protected List<Vector3> checkpointsPos;

    protected enum Axel { Front, Rear }
    [Serializable] protected struct Wheel
    {
        public WheelCollider wheelCollider;
        public Axel axel;
    }

    protected enum DriveMode { Manual, Automatic }
    protected DriveMode mode = DriveMode.Manual;
 
    protected new Rigidbody rigidbody;
    protected Vector3 lastPosition;
    protected float playerSpeed;
    public float PlayerSpeed => playerSpeed;

    protected override void Reset()
    {
        base.Reset();   
        this.ResetValue();
        this.LoadComponents();
        this.LoadCheckpoints();
    }

    protected override void Awake()
    {
        if (PlayerController.instance != null) return;
        PlayerController.instance = this;

        base.Awake();
        this.LoadComponents();
        this.LoadCheckpoints();
    }

    protected override void Start()
    {
        base.Start();
        this.rigidbody.centerOfMass = Vector3.zero;
    }

    protected void FixedUpdate()
    {
        this.CarMode();
        this.GetPlayerSpeed();
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.maxAcceleration = 30.0f;
        this.brakeAcceleration = 50.0f;
        this.turnSensitivity = 1.0f;
        this.maxSteerAngle = 30.0f;
        this.autoSpeed = 15f;
        this.autoRotateSpeed = 2.5f;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
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
        this.ApplyMotorTorque();
        this.Steering();
        this.Braking();

        this.CheckPointsCounter();
    }

    public virtual void ApplyMotorTorque()
    {
        // Forward/backward movement
        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = InputManager.Instance.Vertical * 600 * maxAcceleration * Time.deltaTime;
        }
    }

    public virtual void Steering()
    {
        // Apply sterring
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var _steerAngle = InputManager.Instance.Horizontal * turnSensitivity * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
            }
        }
    }

    public virtual void Braking()
    {
        // Apply braking
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

        this.CheckPointsCounter();
    }

    protected virtual void CheckPointsCounter()
    {
        if (mode == DriveMode.Automatic)
        {
            if (Vector3.Distance(checkpointsPos[currentPoint], transform.position) < minDistanceAuto) currentPoint++;
        }
        else if (mode == DriveMode.Manual)
        {
            if (Vector3.Distance(checkpointsPos[currentPoint], transform.position) < minDistanceManual) currentPoint++;
        }

        if (currentPoint == checkpoints.Count)
        {
            currentPoint = 0;
            laps++;
        }
    }

    protected virtual void GetPlayerSpeed()
    {
        this.playerSpeed = Vector3.Distance(lastPosition, transform.position) / Time.deltaTime;
        lastPosition = transform.position;
    }
}
