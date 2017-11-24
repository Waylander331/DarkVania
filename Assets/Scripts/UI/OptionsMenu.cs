using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OptionsMenu : MonoBehaviour, IMenuTransition {

    public Dropdown dropdownLanguages;

    public void SetMenuVisual()
    {
        MainMenuManager.Instance.SetSelectedObject(MainMenuManager.Instance.optionsMenu.dropdownLanguages.gameObject);
    }
}
