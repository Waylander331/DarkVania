using UnityEngine.UI;
using UnityEngine;

public class SaveMenu : MonoBehaviour {

    MainMenuManager menuManager; //t'as pas vraiment besoin de garder une variable pour une clsse qui a un accesseur

    public Button[] saveButtons;

    public CanvasGroup canvasGroupSaveMenu;
    

    void Start () {
        menuManager = MainMenuManager.Instance;
	}

    //ces fonctions-ci ne sont pas utilisees *******************************************
    public void AssignSaveButton(Button button)
    {
        menuManager.handleSaveFile.saveButton = button;
    }

    public void IdentifyButton(int buttonIdentifier)
    {
        menuManager.handleSaveFile.saveButtonId = buttonIdentifier;
    }

}
