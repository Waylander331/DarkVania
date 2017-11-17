using UnityEngine.UI;
using UnityEngine;
using System.IO;


public class SaveFileHandler : MonoBehaviour {

    public int saveButtonId;

    public GameObject panelCreateNewSave;

    public LayoutGroup layoutAcceptDecline;

    public InputField inputSaveName;

    public Dropdown dropdownDifficulty;

    public Button buttonDecline;

    public Button buttonLoadSave;
    public Button buttonDeleteSave;
    [HideInInspector]
    public Button saveButton;

    public ColorBlock saveButtonColors;
    public ColorBlock emptySaveButtonColors;

    private string path;

    private void Awake()
    {
        path = Application.persistentDataPath;
    }

    // Look for existing save file in the Save Files document and change save buttons accordingly
    public void CheckForSaveFile()
    {
        for (int i = 1; i < 4; i++)
        {
            if (File.Exists(path + i))
            {
                var button = MainMenuManager.Instance.saveMenu.saveButtons[i - 1];
                var currentSaveFile = XMLFilHelper.LoadFile(path + i, typeof(SaveFileTemplate));
                string buttonText = "Name : " + currentSaveFile + "\n" + GetDifficultyString();
                UIUtilities.ChangeButtonVisual(button, saveButtonColors);
                UIUtilities.RenameButton(button, buttonText, TextAnchor.MiddleCenter, 18);
            }
        }
    }

    // Attached to the SaveFile buttons
    // If the current button holds a save file, switch to menu 5, else switch to menu 4
    public void GetSaveFileMenu()
    {
        if (File.Exists(path + saveButtonId))
        {
            MainMenuManager.Instance.SwitchMenuState(5); // LoadDeleteMenu
        }
        else
            MainMenuManager.Instance.SwitchMenuState(4); // Creating Save File
    }

    // Attached to the Accept button 
    // Starts the button sequence to create or delete a save file
    public void ButtonCreateDeleteTransition()
    {
        switch(MainMenuManager.Instance.State)
        {
            case MainMenuManager.MainMenuState.creatingSaveFile:
                if (!inputSaveName.IsActive())
                {
                    inputSaveName.gameObject.SetActive(true);
                    dropdownDifficulty.gameObject.SetActive(true);
                    panelCreateNewSave.SetActive(false);
                }
                else
                {
                    CreateUserSaveFile();
                    MainMenuManager.Instance.HandleBackInput(true);
                }
                break;

            case MainMenuManager.MainMenuState.loadDeleteMenu:
                if(layoutAcceptDecline.IsActive())
                {
                    DeleteUserSaveFile();
                    CheckForSaveFile();
                    UIUtilities.ChangeButtonVisual(saveButton, emptySaveButtonColors);
                    UIUtilities.RenameButton(saveButton, "Empty Save", TextAnchor.MiddleCenter, 18);
                    MainMenuManager.Instance.HandleBackInput(true);
                }
                break;
        }
        
    }

    // Save a SaveFile at path and reshape/rename linked button
    private void CreateUserSaveFile()
    {
        var newSave = new SaveFileTemplate(inputSaveName.text, (int)GameManager.Gm.difficulty, saveButtonId);
        XMLFilHelper.CreateXmlFile(path + saveButtonId, newSave);
        UIUtilities.ChangeButtonVisual(saveButton, saveButtonColors);
        string buttonText = "Name : " + newSave.Name + "\n" + GetDifficultyString();
        UIUtilities.RenameButton(saveButton, buttonText, TextAnchor.MiddleCenter, 18);
    }

    // Delete a Save File
    public void DeleteUserSaveFile()
    {
        var path = Application.persistentDataPath + saveButtonId;
        XMLFilHelper.DeleteFile(path);
    }

    // Change the difficulty and rename dropdown text
    public void ChangeDifficulty()
    {
        GameManager.Gm.difficulty = dropdownDifficulty.value; 
        //est-ce que difficulty a vraiment besoin d'etre une enum ou ca pourrait etre un int?
        dropdownDifficulty.captionText.text = "Difficulty : " + dropdownDifficulty.options[(int)GameManager.Gm.difficulty].text;
    }

    public string GetDifficultyString()
    {
        string temp = "";
        switch (GameManager.Gm.difficulty) //une switch fonctionne aussi avec une variable int, si jamais
        {
            case 0:
                temp = "Difficulty : Normal";
                break;

            case 1 :
                temp = "Difficulty : Hard";
                break;
        }
        return temp;
    }
}
