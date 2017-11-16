using UnityEngine.UI;
using UnityEngine;

public class SaveMenu : MonoBehaviour {

    MainMenuManager menuManager;

    public Button[] saveButtons;

    public CanvasGroup canvasGroupSaveMenu;

    // Use this for initialization
    void Start () {
        menuManager = MainMenuManager.MenuManager;
	}

    public void AssignSaveButton(Button button)
    {
        menuManager.handleSaveFile.saveButton = button;
    }

    public void IdentifyButton(int buttonIdentifier)
    {
        menuManager.handleSaveFile.saveButtonId = buttonIdentifier;
    }

}
