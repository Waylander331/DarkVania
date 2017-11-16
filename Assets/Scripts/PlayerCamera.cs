using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    private PlayerController target;
    private Camera playerCamera;
    private float vAxis;
    public int maxFieldOfView;
    public int minFieldOfView;



	// Use this for initialization
	void Start ()
    {
        playerCamera = GetComponent<Camera>();
        target = GameManager.Gm.playerController;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -15);
        AdjustFieldOfView();
	}

    private void AdjustFieldOfView()
    {
        vAxis = Input.GetAxis("Vertical");
        if(vAxis > 0.5 && playerCamera.orthographicSize < maxFieldOfView)
        {
            playerCamera.orthographicSize += vAxis / 10;
        }
        else if (vAxis < -0.5 && playerCamera.orthographicSize > minFieldOfView)
        {
            playerCamera.orthographicSize += vAxis /10;
        }
    }
}
