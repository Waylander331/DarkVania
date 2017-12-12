using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

    public static GameManager Instance = null;

    public EventSystem eventSystem;

    public PlayerController playerController;
    public PlayerFunction playerFunction;

    public enum GameState { MainMenu = 0, PauseMenu = 1, Game = 2}
    public  GameState gameState;

    public int difficulty;

    public float gameSpeed;

    public List<BaseAI> enemies;


    [HideInInspector]
    public bool usingController;


    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }

        playerController = FindObjectOfType<PlayerController>();
        playerFunction = FindObjectOfType<PlayerFunction>();
    }

    private void OnEnable()
    {
        enemies = new List<BaseAI>();
    }
    void Update ()
    {
        DetectController();
        DetectMouse();
	}

    public void PlayerDead()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].state = Skeleton.State.Idle;
        }
    }


    public void LevelToLoad(int level)
    {
        switch (level)
        {
            case 1:
                StartCoroutine(DelayToLoadScene("ProgTest"));
                break;
        }
    }


    public IEnumerator DelayToLoadScene(string sceneName)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }

    public bool DetectMouse()
    {
        bool mouseDetected = false;
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            Cursor.lockState = CursorLockMode.None;
            usingController = false;
            Cursor.visible = true;
            eventSystem.SetSelectedGameObject(null);
            mouseDetected = true;
        }
        return mouseDetected;
    }


    // Detect if the user is currently using a controller or mouse
    public bool DetectController()
    {
        bool controllerDetected = false;
        if (!usingController)
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0
            || Input.GetButtonDown("Submit") || Input.GetButtonDown("Cancel"))
            {
                usingController = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                controllerDetected = true;
            }
        }
        return controllerDetected;
    }

}
