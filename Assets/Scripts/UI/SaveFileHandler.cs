using UnityEngine.UI;
using UnityEngine;
using System.IO;
using System;

public class SaveFileHandler : MonoBehaviour, IMenuTransition {

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
        var path = Application.persistentDataPath;
        for (int i = 1; i < 4; i++)
        {
            if (File.Exists(path + i))
            {
                var button = MainMenuManager.Instance.saveMenu.saveButtons[i - 1];
                var currentSaveFile = XMLFilHelper.LoadFile(path + i, typeof(SaveFileTemplate)) as SaveFileTemplate;
                string buttonText = "Name : " + currentSaveFile.Name + "\n" + GetDifficultyString();
                UIUtilities.ChangeButtonVisual(button, saveButtonColors);
                UIUtilities.RenameButton(button, buttonText, TextAnchor.MiddleCenter, 18);
            }
        }
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
        var path = Application.persistentDataPath;
        var newSave = new SaveFileTemplate(inputSaveName.text, (int)GameManager.Instance.difficulty, saveButtonId);
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
        GameManager.Instance.difficulty = dropdownDifficulty.value; 
        //est-ce que difficulty a vraiment besoin d'etre une enum ou ca pourrait etre un int?
        dropdownDifficulty.captionText.text = "Difficulty : " + dropdownDifficulty.options[(int)GameManager.Instance.difficulty].text;
    }

    public string GetDifficultyString()
    {
        string temp = "";
        switch (GameManager.Instance.difficulty) //une switch fonctionne aussi avec une variable int, si jamais
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

    public void SetMenuVisual()
    {
        var path = Application.persistentDataPath;

        if (File.Exists(path + saveButtonId))
        {
            MainMenuManager.Instance.SwitchMenuState(5); // LoadDeleteMenu
            MainMenuManager.Instance.saveMenu.canvasGroupSaveMenu.alpha = 0.15f;
            MainMenuManager.Instance.saveFileHandler.buttonLoadSave.gameObject.SetActive(true);
            MainMenuManager.Instance.saveFileHandler.buttonDeleteSave.gameObject.SetActive(true);
            MainMenuManager.Instance.SetSelectedObject(MainMenuManager.Instance.saveFileHandler.buttonLoadSave.gameObject);
        }
        else
        {
            MainMenuManager.Instance.SwitchMenuState(4); // Creating Save File
            MainMenuManager.Instance.saveMenu.canvasGroupSaveMenu.alpha = 0.15f;
            MainMenuManager.Instance.saveFileHandler.panelCreateNewSave.SetActive(true);
            MainMenuManager.Instance.saveFileHandler.layoutAcceptDecline.gameObject.SetActive(true);
            MainMenuManager.Instance.SetSelectedObject(MainMenuManager.Instance.saveFileHandler.buttonDecline.gameObject);
        }
    }
}
