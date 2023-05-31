using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : Buttons
{
    [SerializeField] protected Button pauseButton;
    [SerializeField] protected GameObject pauseMenu;

    protected override void Start()
    {
        base.Start();
        this.pauseMenu.SetActive(false);
    }

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
        this.pauseButton = gameObject.GetComponent<Button>();
    }

    protected override void TaskOnClick()
    {
        this.pauseButton.onClick.AddListener(ButtonClicked);
    }

    protected override void ButtonClicked()
    {
        this.pauseMenu.SetActive(true);
        Time.timeScale = 0f;  
    }
}
