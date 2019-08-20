using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Characters/Character")]
public class CharacterScriptableObject : ScriptableObject
{
    [Header("---Info-------")]
    public string characterName;
    [Header("---Stats-------")]
    [Tooltip("Maximum HP of the character.")]
    public int healthMax;

    [Tooltip("Current HP of the chracter.")]
    public int healthCurrent;

    [Header("---Prefab-------")]
    public GameObject characterPrefab;

    void Init()
    {
        healthCurrent = healthMax;
        Debug.Log("SO Init");
    }
}