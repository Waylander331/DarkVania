using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

    public GameObject gameManager;
    public GameObject HudManager;

    // Use this for initialization
    void Awake ()
    { 
		if(GameManager.Instance == null)
        {
            Instantiate(gameManager);
        }

        if(GUIManager.GuiM == null)
        {
            Instantiate(HudManager);
        }
	}
}
