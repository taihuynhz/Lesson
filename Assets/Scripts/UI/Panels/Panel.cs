using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Panel : MyMonoBehaviour
{
    [SerializeField] protected TMP_Text text;

    protected void LoadText(string name)
    {
        this.text = GameObject.Find(name).transform.GetComponentInChildren<TMP_Text>();
    }

    protected virtual void SetText(float value)
    {
        this.text.text = value.ToString();
    }
}
