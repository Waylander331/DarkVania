using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateEditorCollider : MonoBehaviour {

    private BoxCollider2D boxColl;


	// Use this for initialization
	void Start ()
    {
        boxColl = GetComponent<BoxCollider2D>();
        boxColl.enabled = false;
	}
}
