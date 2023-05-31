using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeButton : Buttons
{   
    [SerializeField] protected Button homeButton;

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
        this.homeButton = GetComponent<Button>();
    }

    protected override void TaskOnClick()
    {
        this.homeButton.onClick.AddListener(ButtonClicked);
    }

    protected override void ButtonClicked()
    {
        SceneManager.LoadScene("HomeScene");
    }
}
