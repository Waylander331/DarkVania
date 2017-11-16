using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {

    public static MainMenuManager MenuManager = null;

    public SaveMenu saveMenu;
    public OptionsMenu optionsMenu;
    public MainMenu mainMenu;
    public HandleSaveFile handleSaveFile;


    public enum MainMenuState { mainMenu = 1, optionsMenu, saveMenu, creatingSaveFile, loadDeleteMenu }
    private MainMenuState _state;
   
    public MainMenuState State // Activate game objects according to the current Main Menu State
    {
        get { return _state; }
       
        set
        {
            _state = value;
            mainMenu.gameObject.SetActive(State == MainMenuState.mainMenu);
            optionsMenu.gameObject.SetActive(State == MainMenuState.optionsMenu);
            saveMenu.gameObject.SetActive(State == MainMenuState.saveMenu || State == MainMenuState.creatingSaveFile || State == MainMenuState.loadDeleteMenu);
            saveMenu.canvasGroupSaveMenu.interactable = (State == MainMenuState.saveMenu);
            handleSaveFile.gameObject.SetActive(State == MainMenuState.creatingSaveFile || State == MainMenuState.loadDeleteMenu);
            switch (State)
            {
                case MainMenuState.mainMenu:
                    if (GameManager.Gm.usingController)
                        GameManager.Gm.eventSystem.SetSelectedGameObject(mainMenu.buttonLoadGame.gameObject);
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
                    if (GameManager.Gm.usingController)
                        GameManager.Gm.eventSystem.SetSelectedGameObject(saveMenu.saveButtons[0].gameObject);
                    break;
                case MainMenuState.optionsMenu:
                    if (GameManager.Gm.usingController)
                        GameManager.Gm.eventSystem.SetSelectedGameObject(optionsMenu.dropdownLanguages.gameObject);
                    break;
                case MainMenuState.creatingSaveFile:
                    saveMenu.canvasGroupSaveMenu.alpha = 0.15f;
                    handleSaveFile.panelCreateNewSave.SetActive(true);
                    handleSaveFile.layoutAcceptDecline.gameObject.SetActive(true);
                    if (GameManager.Gm.usingController)
                        GameManager.Gm.eventSystem.SetSelectedGameObject(handleSaveFile.buttonDecline.gameObject);
                    break;

                case MainMenuState.loadDeleteMenu:
                    saveMenu.canvasGroupSaveMenu.alpha = 0.15f;
                    handleSaveFile.buttonLoadSave.gameObject.SetActive(true);
                    handleSaveFile.buttonDeleteSave.gameObject.SetActive(true);
                    if (GameManager.Gm.usingController)
                        GameManager.Gm.eventSystem.SetSelectedGameObject(handleSaveFile.buttonLoadSave.gameObject);
                    break;
            }
        }
    }

    
    //private string currentLang = "english";

    private void Awake()
    {
        if (MenuManager == null)
        {
            MenuManager = this;
        }
        else if (MenuManager != this)
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
        SetSelectedObject();
    }


    //-----------------------------------------------------
    // MainMenu Functions
    //-----------------------------------------------------


    // Switch to the according menu using int
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
                    State = MainMenuState.mainMenu;
                    break;

                case MainMenuState.saveMenu:
                    State = MainMenuState.mainMenu;
                    break;

                case MainMenuState.creatingSaveFile:
                    State = MainMenuState.saveMenu;
                    break;

                case MainMenuState.loadDeleteMenu:
                    State = MainMenuState.saveMenu;
                    break;
            }
        }
    }




    public void SetSelectedObject()
    {
        if(GameManager.Gm.DetectController())
        {
            switch (State)
            {
                case MainMenuState.mainMenu:
                    GameManager.Gm.eventSystem.SetSelectedGameObject(mainMenu.buttonLoadGame.gameObject);
                    break;

                case MainMenuState.optionsMenu:
                    GameManager.Gm.eventSystem.SetSelectedGameObject(optionsMenu.dropdownLanguages.gameObject);
                    break;
            }
        }
    }
    

    public void ExitGame()
    {
        Application.Quit();
    }
}
