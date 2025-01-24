using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RenameChildrenEditor : EditorWindow
{
    private List<GameObject> gameObjects = new List<GameObject>();

    [MenuItem("Tools/Rename Children")]
    public static void ShowWindow()
    {
        GetWindow<RenameChildrenEditor>("Rename Children");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Rename Child Objects", EditorStyles.boldLabel);

        if (GUILayout.Button("Add Selected GameObjects"))
        {
            AddSelectedGameObjects();
        }

        if (GUILayout.Button("Rename Children"))
        {
            RenameChildObjects();
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("GameObjects to process:", EditorStyles.boldLabel);
        foreach (var go in gameObjects)
        {
            EditorGUILayout.ObjectField(go, typeof(GameObject), true);
        }
    }

    private void AddSelectedGameObjects()
    {
        gameObjects.Clear();
        var selectedObjects = Selection.gameObjects;
        foreach (var obj in selectedObjects)
        {
            if (obj != null && !gameObjects.Contains(obj))
            {
                gameObjects.Add(obj);
            }
        }
    }

    private void RenameChildObjects()
    {
        foreach (var go in gameObjects)
        {
            if (go != null)
            {
                foreach (Transform child in go.transform)
                {
                    if (child.name.Contains("(Clone)"))
                    {
                        string newName = child.name.Replace("(Clone)", "").Trim();
                        child.name = newName;
                    }
                }
            }
        }

        // Refresh the editor to reflect changes
        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("Rename Children", "Renaming complete.", "OK");
    }
}
