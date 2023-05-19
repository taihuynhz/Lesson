using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecoverItem", menuName = "SO/RecoverItem")]
public class RecoverItemsSO : ScriptableObject
{
    public string itemName = "RecoverItem";
    public float fuel = 25f;
    public float capacity = 10f;
    public float hp = 30f;
}
