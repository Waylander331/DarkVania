using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour {

    private PlayerFunction pf;
    private PlayerController pc;
    private Ray leftRay;
    private Ray rightRay;

    private void Start()
    {
        pf = GameManager.Instance.playerFunction;
        pc = GameManager.Instance.playerController;
    }


    public void Update()
    {

    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (pf == null || pc == null) return;

        if(coll.gameObject.tag == "Ground")
        {
            
            if (!pf.Grounded && !pc.jumped)
            {
                pf.OnCollisionEnterGround();
            }
        }
    }

    public void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
           // pf.OnCollisionStayGround();
        }
        
    }

    public void OnCollisionExit2D(Collision2D coll)
    {
        //if(coll.gameObject.tag == "Ground" && coll.contacts.Length > 1)
        //{
        //    Debug.Log(coll.contacts.Length);
        //    pf.OnCollisionExitGround();
        //}
    }

  

        //foreach (ContactPoint2D contact in collision.contacts)
        //{
        //    if(contact.collider.tag == "Ground")
        //    {
        //        Debug.Log("Ground");
        //        yield return new WaitForSeconds(0.03f);
        //        for (int i = 0; i < collision.contacts.Length; i++)
        //        {
        //            Debug.Log(i);
        //            if(i < 1)
        //            {
        //                pf.OnCollisionExitGround();
        //                canStart = false;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        canStart = false;
        //    }
        //}
        
    }
   

    //public void DetectGround(Ray ray1, Ray ray2)
    //{

    //    var myPos = transform.position;
    //    ray1.origin = new Vector2(myPos.x - 0.45f, myPos.y -0.2f);
    //    ray2.origin = new Vector2(myPos.x + 0.45f, myPos.y - 0.2f);
    //    ray1.direction = new Vector2(0, myPos.y - 0.2f);
    //    ray2.direction = new Vector2(0, myPos.y -0.2f);

    //    var leftHit = Physics2D.Raycast(ray1.origin, ray1.direction, 0.25f);
    //    var rightHit = Physics2D.Raycast(ray2.origin, ray2.direction, 0.25f);
    //    Physics2D.queriesStartInColliders = false;
        
    //    if(leftHit.collider.tag == "Ground" || rightHit.collider.tag == "Ground")
    //    {
    //        pf.OnCollisionEnterGround();
    //    }
    //    else if(!leftHit && !rightHit)
    //    {
    //        pf.OnCollisionExitGround();
    //    }
       
    //    Debug.DrawRay(ray1.origin, ray1.direction);
    //    Debug.DrawRay(ray2.origin, ray2.direction);
    //}


