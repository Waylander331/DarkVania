﻿using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {

    public static MainMenuManager Instance = null; //jcrois pas que t'aies ebsoin de mettre = null. Pis tu pourais l'appeller simplement Instance

    public SaveMenu saveMenu;
    public OptionsMenu optionsMenu;
    public MainMenu mainMenu;
    public SaveFileHandler handleSaveFile;


    public enum MainMenuState { mainMenu = 1, optionsMenu, saveMenu, creatingSaveFile, loadDeleteMenu }
    private MainMenuState _state;
   
    public MainMenuState State // Activate game objects according to the current Main Menu State
    {
        get { return _state; }
       
        set  //C'est pas mauvais d'avoir du code dans un setter mais rendu la t'es aussi bin faire un fonction qui s'en occupe
        {
            //if (value != state)
            _state = value;
            mainMenu.gameObject.SetActive(State == MainMenuState.mainMenu);
            optionsMenu.gameObject.SetActive(State == MainMenuState.optionsMenu);
            saveMenu.gameObject.SetActive(State == MainMenuState.saveMenu || State == MainMenuState.creatingSaveFile || State == MainMenuState.loadDeleteMenu);
            saveMenu.canvasGroupSaveMenu.interactable = (State == MainMenuState.saveMenu);
            handleSaveFile.gameObject.SetActive(State == MainMenuState.creatingSaveFile || State == MainMenuState.loadDeleteMenu);

            switch (State)
            {
                case MainMenuState.mainMenu:
                    //if (GameManager.Gm.usingController)
                    //    GameManager.Gm.eventSystem.SetSelectedGameObject(mainMenu.buttonLoadGame.gameObject);
                    SetSelectedObject(mainMenu.buttonLoadGame.gameObject);
                    break;
                case MainMenuState.saveMenu:
                    saveMenu.canvasGroupSaveMenu.alpha = 1;
                    handleSaveFile.inputSaveName.text = "Enter Name";
                    handleSaveFile.inputSaveName.gameObject.SetActive(false);
                    handleSaveFile.dropdownDifficulty.gameObject.SetActive(false);
                    handleSaveFile.buttonLoadSave.gameObject.SetActive(false);
                    handleSaveFile.buttonDeleteSave.gameObject.SetActive(false);
                    handleSaveFile.layoutAcceptDecline.gameObject.SetActive(false);
                    handleSaveFile.panelCreateNewSave.SetActive(false);
                    SetSelectedObject(saveMenu.saveButtons[0].gameObject);
                    //if (GameManager.Gm.usingController)
                    //    GameManager.Gm.eventSystem.SetSelectedGameObject(saveMenu.saveButtons[0].gameObject);
                    break;
                case MainMenuState.optionsMenu:
                    SetSelectedObject(optionsMenu.dropdownLanguages.gameObject);
                    //if (GameManager.Gm.usingController)
                    //    GameManager.Gm.eventSystem.SetSelectedGameObject(optionsMenu.dropdownLanguages.gameObject);
                    break;
                case MainMenuState.creatingSaveFile:
                    saveMenu.canvasGroupSaveMenu.alpha = 0.15f;
                    handleSaveFile.panelCreateNewSave.SetActive(true);
                    handleSaveFile.layoutAcceptDecline.gameObject.SetActive(true);
                    SetSelectedObject(handleSaveFile.buttonDecline.gameObject);
                    //if (GameManager.Gm.usingController)
                    //    GameManager.Gm.eventSystem.SetSelectedGameObject(handleSaveFile.buttonDecline.gameObject);
                    break;

                case MainMenuState.loadDeleteMenu:
                    saveMenu.canvasGroupSaveMenu.alpha = 0.15f;
                    handleSaveFile.buttonLoadSave.gameObject.SetActive(true);
                    handleSaveFile.buttonDeleteSave.gameObject.SetActive(true);
                    SetSelectedObject(handleSaveFile.buttonLoadSave.gameObject);
                    //if (GameManager.Gm.usingController) //C'est des details, mais ces 2 lignes similaires sont repetees. 
                    //                                    //Tu pourrais en faire une mini fonction qui prent le bouton a selectionner en param
                    //    GameManager.Gm.eventSystem.SetSelectedGameObject(handleSaveFile.buttonLoadSave.gameObject);
                    break;
            }
        }
    }

    
    //private string currentLang = "english";

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

    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    // Check for save file and set initial state
    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        handleSaveFile.CheckForSaveFile(); // Check for save file upon loading the main menu
        State = MainMenuState.mainMenu;
    }

    private void Start()
    {
        GameManager.Gm.usingController = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.Gm.eventSystem.SetSelectedGameObject(null);
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




    public void SetSelectedObject(GameObject obj)
    {
        if(GameManager.Gm.usingController)
            GameManager.Gm.eventSystem.SetSelectedGameObject(obj);
    }
    

    public void ExitGame()
    {
        Application.Quit();
    }
}
