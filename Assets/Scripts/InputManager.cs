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

    private void Awake()
    {
        if (InputManager.instance != null) return;
        InputManager.instance = this;
    }

    private void Update()
    {
        this.GetHorizontal();
        this.GetVertical();
    }

    protected virtual void GetHorizontal()
    {
        this.horizontal = Input.GetAxis("Horizontal");
    }

    protected virtual void GetVertical()
    {
        this.vertical = Input.GetAxis("Vertical");
    }
}
