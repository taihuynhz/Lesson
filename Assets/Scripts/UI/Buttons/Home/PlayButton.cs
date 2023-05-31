using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : Buttons
{
    [SerializeField] protected Button playButton;

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
        this.playButton = GetComponent<Button>();
    }

    protected override void TaskOnClick()
    {
        this.playButton.onClick.AddListener(ButtonClicked);
    }

    protected override void ButtonClicked()
    {
        SceneManager.LoadScene("Lesson_8");
    }
}
