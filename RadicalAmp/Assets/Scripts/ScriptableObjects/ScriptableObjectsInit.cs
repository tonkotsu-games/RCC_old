using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectsInit
{
    [SerializeField]
    private CharacterScriptableObject[] toReset = null;

    void Awake()
    {
        for (int i = 0; i < toReset.Length; i++)
        {
            toReset[i].healthCurrent = toReset[i].healthMax;
        }
    }
}