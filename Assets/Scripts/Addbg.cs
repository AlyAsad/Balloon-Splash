using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//
//
// GETTING COMPILE ERRORS SO COMMENTING OUT RN
//
//


/*
public class AddBackgroundToAllScenes : EditorWindow

{   
    private GameObject backgroundPrefab;

    [MenuItem("Tools/Add Background to All Scenes")]
    public static void ShowWindow()
    {
        GetWindow<AddBackgroundToAllScenes>("Add Background to All Scenes");
    }

    private void OnGUI()
    {
        GUILayout.Label("Background Prefab", EditorStyles.boldLabel);
        backgroundPrefab = (GameObject)EditorGUILayout.ObjectField("Prefab", backgroundPrefab, typeof(GameObject), false);

        if (GUILayout.Button("Add Background to All Scenes"))
        {
            AddBackgroundToScenes();
        }
    }

    private void AddBackgroundToScenes()
    {
        if (backgroundPrefab == null)
        {
            Debug.LogError("Background prefab not assigned.");
            return;
        }

        string[] scenePaths = AssetDatabase.FindAssets("t:Scene");
        foreach (string scenePath in scenePaths)
        {
            string path = AssetDatabase.GUIDToAssetPath(scenePath);
            Scene scene = EditorSceneManager.OpenScene(path);

            if (GameObject.FindObjectOfType<BackgroundManager>() == null)
            {
                GameObject backgroundManager = new GameObject("BackgroundManager");
                BackgroundManager manager = backgroundManager.AddComponent<BackgroundManager>();
                manager.backgroundPrefab = backgroundPrefab;

                Undo.RegisterCreatedObjectUndo(backgroundManager, "Create BackgroundManager");
            }

            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene);
        }

        Debug.Log("Background added to all scenes.");
    }
}*/




//REMOVE WHEN ABOVE CODE IS FIXED
public class AddBackgroundToAllScenes : MonoBehaviour {}