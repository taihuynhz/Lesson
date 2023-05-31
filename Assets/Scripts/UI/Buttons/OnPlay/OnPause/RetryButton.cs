using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RetryButton : Buttons
{
    [SerializeField] protected Button retryButton;

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
        this.retryButton = GetComponent<Button>();
    }

    protected override void TaskOnClick()
    {
        this.retryButton.onClick.AddListener(ButtonClicked);
    }

    protected override void ButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
