using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController: MonoBehaviour
{   
    protected enum DriveMode { Manual, Automatic }
    protected DriveMode mode = DriveMode.Manual;

    [Header("===== Drive Mode =====")]
    [SerializeField] protected bool onAutomatic;
    [SerializeField] protected bool onManual;

    [Header("===== Car Speed =====")]
    [SerializeField] protected float speed = 15f;
    [SerializeField] protected float rotateSpeed = 50f;
    [SerializeField] protected float autoRotateSpeed = 2.5f;

    [Header("===== Checkpoints =====")]
    [SerializeField] protected int currentPoint = 0;
    [SerializeField] protected float minDistance = 0.1f;
    [SerializeField] protected List<Transform> checkpoints;
    [SerializeField] protected List<Vector3> checkpointsPos;

    protected void Reset()
    {
        this.ResetValue();
        this.LoadCheckpoints();
    }

    protected void Awake()
    {
        this.LoadCheckpoints();
    }

    protected void FixedUpdate()
    {
        this.CarMode();
    }

    protected virtual void ResetValue()
    {
        this.speed = 15f;
        this.rotateSpeed = 50f;
        this.autoRotateSpeed = 2.5f;
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
        transform.Translate(InputManager.Instance.Vertical * Vector3.forward * Time.deltaTime * speed);
        transform.Rotate(InputManager.Instance.Horizontal * Vector3.up * Time.deltaTime * rotateSpeed);
    }

    protected virtual void CarAutoMode()
    {
        transform.position = Vector3.MoveTowards(transform.position, checkpointsPos[currentPoint], Time.deltaTime * speed);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, checkpointsPos[currentPoint] - transform.position, Time.deltaTime * autoRotateSpeed, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

        if (Vector3.Distance(checkpointsPos[currentPoint], transform.position) < minDistance) currentPoint++;
        if (currentPoint == checkpoints.Count) currentPoint = 0;
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
}
