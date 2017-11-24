using UnityEngine.UI;
using UnityEngine;

public class SaveMenu : MonoBehaviour, IMenuTransition {

    public Button[] saveButtons;

    public CanvasGroup canvasGroupSaveMenu;


    public void AssignSaveButton(Button button)
    {
        MainMenuManager.Instance.saveFileHandler.saveButton = button;
    }

    public void IdentifyButton(int buttonIdentifier)
    {
        MainMenuManager.Instance.saveFileHandler.saveButtonId = buttonIdentifier;
    }

    public void SetMenuVisual()
    {
        canvasGroupSaveMenu.alpha = 1;
        MainMenuManager.Instance.saveFileHandler.inputSaveName.text = "Enter Name";
        MainMenuManager.Instance.saveFileHandler.inputSaveName.gameObject.SetActive(false);
        MainMenuManager.Instance.saveFileHandler.dropdownDifficulty.gameObject.SetActive(false);
        MainMenuManager.Instance.saveFileHandler.buttonLoadSave.gameObject.SetActive(false);
        MainMenuManager.Instance.saveFileHandler.buttonDeleteSave.gameObject.SetActive(false);
        MainMenuManager.Instance.saveFileHandler.layoutAcceptDecline.gameObject.SetActive(false);
        MainMenuManager.Instance.saveFileHandler.panelCreateNewSave.SetActive(false);
        MainMenuManager.Instance.SetSelectedObject(MainMenuManager.Instance.saveMenu.saveButtons[0].gameObject);
    }
}
