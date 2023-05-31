using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeButton : Buttons
{
    [SerializeField] protected Button resumeButton;
    [SerializeField] protected GameObject pauseMenu;

    protected void Update()
    {
        this.TaskOnClick();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadButton();
        this.pauseMenu = GameObject.Find("PauseMenuPanel");
    }

    protected override void LoadButton()
    {
        this.resumeButton = GetComponent<Button>();
    }

    protected override void TaskOnClick()
    {
        this.resumeButton.onClick.AddListener(ButtonClicked);
    }

    protected override void ButtonClicked()
    {
        this.pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
