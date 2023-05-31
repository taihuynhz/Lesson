using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapsPanel : Panel
{
    protected void Update()
    {
        this.SetText(PlayerController.Instance.Laps);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText("LapsCountText");
    }

    protected override void SetText(float value)
    {
        this.text.text = value.ToString() + "/12";
    }
}
