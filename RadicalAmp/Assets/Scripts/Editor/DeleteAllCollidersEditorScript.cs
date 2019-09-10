using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DeleteAllCollidersEditorScript : EditorWindow
{
    [MenuItem("EditorScript/Delete/AreYouSure?")]

    static void Init()
    {
        DeleteAllCollidersEditorScript window = ScriptableObject.CreateInstance<DeleteAllCollidersEditorScript>();
        window.position = new Rect(Screen.width / 2 - 175, Screen.height / 2, 350, 150);
        window.ShowPopup();
    }

    void OnGUI()
    {
        GUIStyle sectionNameStyle = new GUIStyle();
        sectionNameStyle = EditorStyles.wordWrappedLabel;
        sectionNameStyle.fontSize = 20;
        sectionNameStyle.normal.textColor = Color.red;
        EditorGUILayout.LabelField("Are you sure? This will delete all Colliders in Scene and cannot be undone!", sectionNameStyle);
        GUILayout.Space(10);
        if (GUILayout.Button("Agree!")) this.Delete();
         GUILayout.Space(10);
        if (GUILayout.Button("Abort!")) this.Close();
    }

    public void Delete()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>() ;
        foreach(GameObject go in allObjects)
        {
            if (go.activeInHierarchy)
            {
                Collider collider = go.GetComponent<Collider>();
                if(collider != null)
                {
                    DestroyImmediate(collider);
                }
                collider = null;
            }
        }

        this.Close();
    }
}
