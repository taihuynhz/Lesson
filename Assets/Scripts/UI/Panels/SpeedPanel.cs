using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPanel : Panel
{
    protected void Update()
    {
        this.SetText(PlayerController.Instance.PlayerSpeed);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText("SpeedCountText");
    }

    protected override void SetText(float value)
    {
        this.text.text = value.ToString("F2");
    }
}
