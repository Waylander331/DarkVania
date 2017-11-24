using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class MainMenuManager : MonoBehaviour, IMenuTransition {

    public static MainMenuManager Instance;

    public SaveMenu saveMenu;
    public OptionsMenu optionsMenu;
    public MainMenu mainMenu;
    public SaveFileHandler saveFileHandler;


    public enum MainMenuState { mainMenu = 1, optionsMenu, saveMenu, creatingSaveFile, loadDeleteMenu }
    private MainMenuState _state;
   
    public MainMenuState State // Activate game objects according to the current Main Menu State
    {
        get { return _state; }
        set
        {
            _state = value;
            SetMenuVisual();
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            DestroyObject(gameObject);
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    
    // Check for save file and set initial state
    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
       
        State = MainMenuState.mainMenu;
    }

    public void Start()
    {
        saveFileHandler.CheckForSaveFile(); // Check for save file upon loading the main menu
        GameManager.Instance.usingController = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.Instance.eventSystem.SetSelectedGameObject(null);
    }

    private void Update()
    {
        HandleBackInput();
    }

    //-----------------------------------------------------
    // MainMenu Functions
    //-----------------------------------------------------



    // Switch to the appropriate menu using int
    public void SwitchMenuState(int state)
    {
        State = (MainMenuState)state;
    }

    // Send the user back to the previous menu
    public void HandleBackInput(bool buttonWasPressed = false)
    {
        if (buttonWasPressed || Input.GetButtonDown("Cancel") || Input.GetButtonDown("Cancel1"))
        {
            switch (State)
            {
                case MainMenuState.optionsMenu:
                case MainMenuState.saveMenu:
                    State = MainMenuState.mainMenu;
                    break;

                case MainMenuState.creatingSaveFile:
                case MainMenuState.loadDeleteMenu:
                    State = MainMenuState.saveMenu;
                    break;
            }
        }
    }

    public void SetMenuVisual()
    {
        mainMenu.gameObject.SetActive(State == MainMenuState.mainMenu);
        optionsMenu.gameObject.SetActive(State == MainMenuState.optionsMenu);
        saveMenu.gameObject.SetActive(State == MainMenuState.saveMenu || State == MainMenuState.creatingSaveFile || State == MainMenuState.loadDeleteMenu);
        saveMenu.canvasGroupSaveMenu.interactable = (State == MainMenuState.saveMenu);
        saveFileHandler.gameObject.SetActive(State == MainMenuState.creatingSaveFile || State == MainMenuState.loadDeleteMenu);

        switch (State)
        {
            case MainMenuState.mainMenu:
                mainMenu.SetMenuVisual();
                break;
            case MainMenuState.saveMenu:
                saveMenu.SetMenuVisual();
                break;
            case MainMenuState.optionsMenu:
                optionsMenu.SetMenuVisual();
                break;
        }
    }

    public void SetSelectedObject(GameObject obj)
    {
        if(GameManager.Instance.usingController)
            GameManager.Instance.eventSystem.SetSelectedGameObject(obj);
    }
    

    public void ExitGame()
    {
        Application.Quit();
    }
}
