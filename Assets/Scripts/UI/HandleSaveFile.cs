using UnityEngine.UI;
using UnityEngine;
using System.IO;
  

public class HandleSaveFile : MonoBehaviour{ 

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

    // Look for existing save file in the Save Files document and change save buttons accordingly
    public void CheckForSaveFile()
    {
        for (int i = 1; i < 4; i++)
        {
            var path = "C:/Users/Waylander/Documents/GitHub/DarkVania/Assets/Save Files/" + i;
            if(File.Exists(path))
            {
                var button = MainMenuManager.MenuManager.saveMenu.saveButtons[i - 1];
                var currentSaveFile = HandleXmlFile.LoadFile(path, typeof(SaveFileTemplate));
                string buttonText = "Name : " + currentSaveFile + "\n" + GetDifficultyString();
                ModUI.ChangeButtonVisual(button, saveButtonColors);
                ModUI.RenameButton(button, buttonText, TextAnchor.MiddleCenter, 18);
            }
        }
    }

    // Attached to the SaveFile buttons
    // If the current button holds a save file, switch to menu 5, else switch to menu 4
    public void GetSaveFileMenu()
    {
        var path = "C:/Users/Waylander/Documents/GitHub/DarkVania/Assets/Save Files/" + saveButtonId; 
        if (File.Exists(path))
        {
            MainMenuManager.MenuManager.SwitchMenuState(5);
        }
        else
            MainMenuManager.MenuManager.SwitchMenuState(4);
    }

    // Attached to the Accept button 
    // Starts the button chain to create or delete a save file
    public void ButtonCreateDeleteTransition()
    {
        switch(MainMenuManager.MenuManager.State)
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
                    MainMenuManager.MenuManager.HandleBackInput(true);
                }
                break;

            case MainMenuManager.MainMenuState.loadDeleteMenu:
                if(layoutAcceptDecline.IsActive())
                {
                    DeleteUserSaveFile();
                    CheckForSaveFile();
                    ModUI.ChangeButtonVisual(saveButton, emptySaveButtonColors);
                    ModUI.RenameButton(saveButton, "Empty Save", TextAnchor.MiddleCenter, 18);
                    MainMenuManager.MenuManager.HandleBackInput(true);
                }
                break;
        }
        
    }

    // Save a SaveFile at path and reshape/rename linked button
    private void CreateUserSaveFile()
    {
        var path = "C:/Users/Waylander/Documents/GitHub/DarkVania/Assets/Save Files/" + saveButtonId;
        var newSave = new SaveFileTemplate(inputSaveName.text, (int)GameManager.Gm.difficulty, saveButtonId);
        HandleXmlFile.CreateXmlFile(path, newSave);
        ModUI.ChangeButtonVisual(saveButton, saveButtonColors);
        string buttonText = "Name : " + newSave.Name + "\n" + GetDifficultyString();
        ModUI.RenameButton(saveButton, buttonText, TextAnchor.MiddleCenter, 18);
    }

    // Delete a Save File
    public void DeleteUserSaveFile()
    {
        var path = "C:/Users/Waylander/Documents/GitHub/DarkVania/Assets/Save Files/" + saveButtonId;
        HandleXmlFile.DeleteFile(path);
    }

    // Change the difficulty and rename dropdown text
    public void ChangeDifficulty()
    {
        GameManager.Gm.difficulty = (GameManager.Difficulty)dropdownDifficulty.value;
        dropdownDifficulty.captionText.text = "Difficulty : " + dropdownDifficulty.options[(int)GameManager.Gm.difficulty].text;
    }

    public string GetDifficultyString()
    {
        string temp = "";
        switch (GameManager.Gm.difficulty)
        {
            case GameManager.Difficulty.normal:
                temp = "Difficulty : Normal";
                break;

            case GameManager.Difficulty.hard:
                temp = "Difficulty : Hard";
                break;
        }
        return temp;
    }
}
