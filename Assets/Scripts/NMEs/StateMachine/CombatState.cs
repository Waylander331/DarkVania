using System;
using System.Collections;
using UnityEngine;

public class CombatState : BaseState{

    //public IEnumerator Attack()
    //{
    //    if (Vector3.Distance(transform.position, target) > 2.5f)
    //    {
    //        state = State.Chase;

    //    }
    //    else if (state == State.Attack)
    //    {
    //        int randomFloat = Random.Range(0, 2);
    //        yield return new WaitForSeconds(randomFloat);
    //        int randomInt = Random.Range(0, 100);
    //        if (randomInt > 49)
    //        {
    //            animator.SetTrigger("Attack1");
    //        }
    //        else
    //        {
    //            animator.SetTrigger("Attack2");
    //        }

    //    }
    //}

    public override void TransitionIn()
    {
        throw new NotImplementedException();
    }

    public override void TransitionOut()
    {
        throw new NotImplementedException();
    }

    public override BaseAI CanTransition()
    {
        throw new NotImplementedException();
    }

    public override BaseState NextState()
    {
        throw new NotImplementedException();
    }

    public override void Update()
    {
        throw new NotImplementedException();
    }
}
