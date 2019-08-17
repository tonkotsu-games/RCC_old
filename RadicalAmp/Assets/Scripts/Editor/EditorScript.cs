﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorScript : EditorWindow
{
    [MenuItem("EditorScript/New Position %m")]

    static void NewPosition()
    {        
        GameObject obj = Selection.activeGameObject;
        obj.transform.position = new Vector3(SceneView.lastActiveSceneView.camera.transform.localPosition.x, 
                                             SceneView.lastActiveSceneView.camera.transform.localPosition.y - 1, 
                                             SceneView.lastActiveSceneView.camera.transform.localPosition.z + 1);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}