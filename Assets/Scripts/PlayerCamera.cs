using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    private PlayerController target;
    private Camera camera;
    private float vAxis;
    public int maxFieldOfView;
    public int minFieldOfView;



	// Use this for initialization
	void Start ()
    {
        camera = GetComponent<Camera>();
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
        vAxis = Input.GetAxis("Zoom");
        if(vAxis > 0.5 && camera.orthographicSize < maxFieldOfView)
        {
            camera.orthographicSize += vAxis / 10;
        }
        else if (vAxis < -0.5 && camera.orthographicSize > minFieldOfView)
        {
            camera.orthographicSize += vAxis /10;
        }
    }
}
