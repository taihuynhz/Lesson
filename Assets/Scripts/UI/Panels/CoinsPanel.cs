using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsPanel : Panel
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText("CoinsCountText");
    }
}
