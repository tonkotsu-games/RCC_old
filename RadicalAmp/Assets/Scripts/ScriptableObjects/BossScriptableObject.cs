using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boss", menuName = "Characters/Boss")]
public class BossScriptableObject : CharacterScriptableObject
{
    [Space]
    [Header("Meteorite")]
    [Header("---Attacks-------")]
    [Space(10)]
    [Tooltip("Prefab of the Meteorite Attack")]
    public GameObject meteoritePrefab;
}