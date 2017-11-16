using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Localisation : MonoBehaviour {

    public Text textStartGame;
    public Text textSaveFiles;
    public Text textLoadSave;
    public Text textSave1;
    public Text textSave2;
    public Text textSaves3;
    public Text textCreateNewSave;
    public Text textAccept;
    public Text textDecline;
    public Text textDifficulty;
    public Text textExit;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}




    //public void ChangeLanguage(string buttonLanguage)
    //{
    //    switch (buttonLanguage)
    //    {
    //        case "french":
    //            textStartGame.text = "Démarrer La Partie";
    //            textExit.text = "Quitter";

    //            switch (difficulty)
    //            {
    //                case Difficulty.normal:
    //                    textDifficulty.text = "Difficulté : Normal";
    //                    break;

    //                case Difficulty.hard:
    //                    textDifficulty.text = "Difficulté : Difficile";
    //                    break;
    //            }
    //            break;

    //        case "english":
    //            textStartGame.text = "Start Game";
    //            textExit.text = "Quit";

    //            switch (difficulty)
    //            {
    //                case Difficulty.normal:
    //                    textDifficulty.text = "Difficulty : Normal";
    //                    break;

    //                case Difficulty.hard:
    //                    textDifficulty.text = "Difficulty : Hard";
    //                    break;
    //            }
    //            break;
    //    }
    //    currentLang = buttonLanguage;
    //}
}
