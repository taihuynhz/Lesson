using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PosPanel : Panel
{
    protected void Update()
    {
        this.SetText(PlayerController.Instance.CurrentPoint);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText("PosCountText");
    }
}
