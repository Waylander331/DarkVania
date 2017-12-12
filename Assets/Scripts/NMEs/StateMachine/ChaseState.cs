using System;
using System.Collections;
using UnityEngine;

public class ChaseState : BaseState {
   

    public void ChasePlayer()
    {
        //StartCoroutine(DirectTowardsTarget());
        //if (transform.position.x > perimeter.bounds.max.x && facingRight)
        //{
        //    if(!playerDetected)
        //    {
        //        state = State.Patrol;
        //    }
        //    facingRight = false;
        //    facingLeft = true;
        //    Direction = Vector2.left / 10;
        //    transform.rotation = leftRot;
        //}
        //else if (transform.position.x < perimeter.bounds.min.x && facingLeft)
        //{
        //    if (!playerDetected)
        //    {
        //        state = State.Patrol;
        //    }
        //    facingRight = true;
        //    facingLeft = false;
        //    Direction = Vector2.right / 10;
        //    transform.rotation = rightRot;
        //}
    }

    //    if (Vector3.Distance(transform.position, target) > 2.1f)
    //    {
    //        Direction = Direction;
    //        animator.speed = 0.5f;
    //        animator.SetBool("Charge", true);
    //        Vector2 move = Vector2.MoveTowards(transform.position, waypoint, MovementSpeed);
    //        rb.MovePosition(move);
    //    }
    //    else
    //    {
    //        animator.SetBool("Charge", false);
    //        animator.SetTrigger("ChargeAttack");
    //        //yield return new WaitForSeconds(0);
    //        state = State.Attack;
    //    }
    //}

    //public IEnumerator DirectTowardsTarget()
    //{
    //    if (target.x > transform.position.x)
    //    {
    //        if (facingRight != true)
    //        {
    //            facingRight = true;
    //            yield return new WaitForSeconds(0.5f);
    //            Direction = Vector2.right / 10;
    //            facingLeft = false;
    //            transform.rotation = rightRot;
    //        }
    //    }
    //    else if (target.x < transform.position.x)
    //    {
    //        if (facingLeft != true)
    //        {
    //            facingLeft = true;
    //            yield return new WaitForSeconds(0.5f);
    //            Direction = Vector2.left / 10;
    //            facingRight = false;
    //            transform.rotation = leftRot;
    //        }
    //    }
    //}
    public override BaseAI CanTransition()
    {
        return null;
    }

    public override BaseState NextState()
    {
        throw new NotImplementedException();
    }

    public override void TransitionIn()
    {
        Debug.Log("Transited into ChaseState");
    }

    public override void TransitionOut()
    {
        Debug.Log("Transited out of ChaseState");
    }

    public override void Update()
    {
        Debug.Log("Chase State");
    }
}
