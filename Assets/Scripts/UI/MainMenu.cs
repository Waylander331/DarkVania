using UnityEngine.UI;
using UnityEngine;
using System;

public class MainMenu : MonoBehaviour, IMenuTransition {


    public Button buttonLoadGame;

    public void SetMenuVisual()
    {
        MainMenuManager.Instance.SetSelectedObject(MainMenuManager.Instance.mainMenu.buttonLoadGame.gameObject);
    }
}
