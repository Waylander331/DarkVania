using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollider : MonoBehaviour
{
    private PlayerFunction pc;

    void Start()
    {
        pc = GameManager.Instance.playerFunction;
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.collider.tag == "Player")
        {
            foreach(ContactPoint2D contacts in coll.contacts)
            {
                Vector2 contactPoints = new Vector2(pc.transform.position.x - contacts.point.x, 0);
                if(contactPoints.x < 0)
                {
                    pc.JumpedOnEnemy(0);
                }
                else if(contactPoints.x > 0)
                {
                    pc.JumpedOnEnemy(1);
                }
            }
        }
    }
	
}
