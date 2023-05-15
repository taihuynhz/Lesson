using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance => instance;

    protected float horizontal ;
    public float Horizontal => horizontal;

    protected float vertical ;
    public float Vertical => vertical;

    protected bool brake;
    public bool Brake => brake;

    protected bool auto;
    public bool Auto => auto;

    protected bool manual;
    public bool Manual => manual;

    protected bool model_l;
    public bool Model_l => model_l;

    protected bool model_2;
    public bool Model_2 => model_2;

    protected bool model_3;
    public bool Model_3 => model_3;

    private void Awake()
    {
        if (InputManager.instance != null) return;
        InputManager.instance = this;
    }

    private void Update()
    {
        this.GetHorizontal();
        this.GetVertical();
        this.GetBrakeInput();
        this.GetCarMode();
        this.GetCarModel();
    }

    protected virtual void GetHorizontal()
    {
        this.horizontal = Input.GetAxis("Horizontal");
    }

    protected virtual void GetVertical()
    {
        this.vertical = Input.GetAxis("Vertical");
    }

    protected virtual void GetBrakeInput()
    {
        this.brake = Input.GetKey(KeyCode.Space);
    }

    protected virtual void GetCarMode()
    {
        this.auto = Input.GetKey(KeyCode.Alpha1);
        this.manual = Input.GetKey(KeyCode.Alpha2);
    }

    protected virtual void GetCarModel()
    {
        this.model_l = Input.GetKey(KeyCode.Alpha7);
        this.model_2 = Input.GetKey(KeyCode.Alpha8);
        this.model_3 = Input.GetKey(KeyCode.Alpha9);
    }

}
