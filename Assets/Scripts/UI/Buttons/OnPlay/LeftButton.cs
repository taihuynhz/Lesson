using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeftButton : Buttons
{
    [SerializeField] protected Button leftButton;

    protected void Update()
    {
        this.TaskOnClick();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadButton();
    }

    protected override void LoadButton()
    {
        this.leftButton = GetComponent<Button>();
    }

    protected override void TaskOnClick()
    {
        this.leftButton.onClick.AddListener(ButtonClicked);
    }

    protected override void ButtonClicked()
    {
        PlayerController.Instance.Steering();
    }
}
