using UnityEngine;
using System.Collections;
using System;

public class DeadState : BaseState
{
    public override BaseAI CanTransition()
    {
        throw new NotImplementedException();
    }

    //public IEnumerator Deadd()
    //{
    //    if (baseAi.Health <= 0)
    //    {
    //        //state = State.Dead;
    //        if (baseAi.facingRight)
    //        {
    //            //baseAi.transform.rotation = baseAi.leftRot;
    //        }
    //        else
    //        {
    //            transform.rotation = rightRot;
    //        }

    //        animator.SetTrigger("Dead");
    //        yield return new WaitForSeconds(2f);
    //        gameObject.SetActive(false);
    //    }
    //    else state = State.Idle;
    //}

    public override BaseState NextState()
    {
        throw new NotImplementedException();
    }

    public override void TransitionIn()
    {
        throw new NotImplementedException();
    }

    public override void TransitionOut()
    {
        throw new NotImplementedException();
    }

    public override void Update()
    {
        throw new NotImplementedException();
    }
}
