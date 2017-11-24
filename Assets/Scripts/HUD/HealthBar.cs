using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    Slider slider;
    GameManager gm;
	// Use this for initialization
	void Start ()
    {
        gm = GameManager.Instance;
        slider = GetComponent<Slider>();
       // slider.maxValue = GameManager.Instance.playerController.playerHp;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(gm.gameState == GameManager.GameState.Game)
            Health(GameManager.Instance.playerController.playerHp);
	}



    public void Health(float objectHp)
    {
        slider.value = objectHp;
    }
    

}
