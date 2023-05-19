using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamageReceiver : DamageReceiver
{
    protected override void OnDead()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
