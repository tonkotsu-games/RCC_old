using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BasicEnemy", menuName = "Characters/BasicEnemy")]
public class BasicEnemyScriptableObject : CharacterScriptableObject
{   
    [Range(.5f, 40f)]
	public float aggroRange = 3f;
}