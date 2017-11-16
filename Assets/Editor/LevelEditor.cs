using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelEditor : EditorWindow
{
    List<Object> loadedPrefabs = new List<Object>();

    private GameObject selectedPrefab;
    private Vector2 mousePos;
    private static bool editorMode;
    private Texture2D spriteTexture;
    private SpriteRenderer sr;
    private Transform groundTilesParent;
    private Transform wallTilesParent;

    [MenuItem("Window/Level Editor")]
   // [CustomEditor(typeof(LevelEditor))]

    static void Init()
    {
        LevelEditor window = (LevelEditor)EditorWindow.GetWindow(typeof(LevelEditor));
        window.Show();
        editorMode = false;
    }



    public void OnEnable()
    {
        SceneView.onSceneGUIDelegate += SceneGUI;
        loadedPrefabs = LoadPrefabsToList("LevelEditor");
    }


    private void OnDisable()
    {
        SceneView.onSceneGUIDelegate -= SceneGUI;
    }


    void SceneGUI(SceneView sceneView)
    {
        InstantiatePrefabOnCursor();
    }


    public void OnProjectChange()
    {
        loadedPrefabs = LoadPrefabsToList("LevelEditor");
    }


    public void OnHierarchyChange()
    {
        InstantiateParentTiles();
    }

    private void OnGUI()
    {
        GUILayout.Label("Game Objects", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("Turn On the Level Editor to activate. Click on a prefab button to select a prefab to instantiate. " +
            "Use the left click to instantiate a single object, " +
            "hold shift and move cursor to instantiate multiple object. " +
            "Hold ctrl and move cursor to destroy a game object under the cursor", MessageType.Info, true);
        InstantiateParentTiles(); // Make sure there is a parent for the tiles to be created.
        CreatePrefabsButton(loadedPrefabs); // Create prefabs button for every object in the loadedPrefabs list;
    }



    public List<Object> LoadPrefabsToList(string path)
    {
        var prefabs = Resources.LoadAll(path);
        var list = new List<Object>();

        for (int i = 0; i < prefabs.Length; i++)
        {
            list.Add(prefabs[i]);
        }

        return list;
    }

    public void CreatePrefabsButton(List<Object> objectList)
    {
       
        if (GUILayout.Button("Editor is: " + editorMode))
        {
            editorMode = !editorMode;
            
        }

        if (selectedPrefab != null)
        {
            EditorGUILayout.TextField("Selected Prefab: " + selectedPrefab.name);
            sr = selectedPrefab.GetComponent<SpriteRenderer>();
            spriteTexture = sr.sprite.texture;
            GUILayout.Box(spriteTexture, GUILayout.Width(sr.sprite.rect.width), GUILayout.Height(sr.sprite.rect.height));
        }
        else
        {
            EditorGUILayout.TextField("Selected Prefab: No Selected Prefab");
        }

        sr = new SpriteRenderer();
        for (int i = 0; i < objectList.Count; i++)
        {
            if (GUILayout.Button(objectList[i].name))
            {
                editorMode = true;

                selectedPrefab = (GameObject)objectList[i];
                
            }
            
        }
    }

    public void InstantiatePrefabOnCursor()
    {
        Event currentEvent = Event.current;
        Camera cam = SceneView.lastActiveSceneView.camera;

        mousePos = Event.current.mousePosition;
        mousePos.y = Screen.height - mousePos.y - 36.0f;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        mousePos = new Vector2(Mathf.Ceil(mousePos.x) - 0.5f, Mathf.Ceil(mousePos.y) - 0.5f);

        var ray = HandleUtility.GUIPointToWorldRay(mousePos);
        ray.origin = new Vector3(mousePos.x, mousePos.y, -0.5f);
        ray.direction = new Vector3(0, 0, 10);
        var hit = Physics2D.Raycast(ray.origin, ray.direction);
        // Debug.Log("Origin: " + ray.origin + "Direction: " + ray.direction);

        if (editorMode)
        {
            switch (currentEvent.type)
            {
                case EventType.MouseDown:
                    if (currentEvent.button == 0)
                    {
                        if (!hit)
                        {
                            if (selectedPrefab != null)
                            {
                                var tile = Instantiate(selectedPrefab, mousePos, Quaternion.identity) as GameObject;
                                SetParent(tile);
                            }
                            else
                            {
                                EditorGUILayout.TextField("Please choose a prefab to instantiate");
                            }
                        }
                    }
                    break;

                case EventType.MouseMove:
                    if (currentEvent.shift)
                    {
                        if (!hit)
                        {
                            var tile = Instantiate(selectedPrefab, mousePos, Quaternion.identity) as GameObject;
                            SetParent(tile);
                        }
                    }
                    if (currentEvent.control && currentEvent.button == 0)
                    {
                        if (hit)
                        {
                            var target = hit.collider.GetComponentInParent<SpriteRenderer>();
                            DestroyImmediate(target.gameObject);
                        }
                    }
                    break;
            }
        }
    }


    private void InstantiateParentTiles()
    {
        var count = 0;
        var prefabs = Resources.LoadAll("Parents");
        var groundTile = GameObject.FindGameObjectsWithTag("GroundTiles");

        for (int i = 0; i < prefabs.Length; i++)
        {
            switch (prefabs[i].name)
            {
            case "GroundTiles":
                if(groundTile.Length == 0)
                {
                    var prefab = (GameObject)Instantiate(prefabs[i]);
                    groundTilesParent = prefab.transform;
                }
                else
                {  
                    for (int j = 0; j < groundTile.Length; j++)
                    {
                        if (groundTile[j].name == "GroundTiles(Clone)")
                        {
                            count++;
                            groundTilesParent = groundTile[j].transform;
                        }
                    }
                    if(count == 0)
                    {
                        var prefab = (GameObject)Instantiate(prefabs[i]);
                        groundTilesParent = prefab.transform;
                    }
                }
                break;
            }
        }
    
    }


    private void SetParent(GameObject tile)
    {
        switch (tile.tag)
        {
            case "Ground":
                tile.transform.parent = groundTilesParent;
                break;
        }
    }
}
    
