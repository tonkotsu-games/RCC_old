using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boss", menuName = "Characters/Boss")]
public class BossScriptableObject : CharacterScriptableObject
{   
    [Range(.5f, 40f)]
	public float aggroRange = 3f;

    [Space]
    [Header("Meteorite")]
    [Header("---Attacks-------")]
    [Space(10)]
    [Tooltip("Prefab of the Meteorite Attack")]
    public GameObject meteoritePrefab;
}