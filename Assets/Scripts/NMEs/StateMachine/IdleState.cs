using System;
using UnityEngine;

public class IdleState : BaseState {



    public override void TransitionIn()
    {
        Debug.Log("Transited in Idle State");
    }

    public override void TransitionOut()
    {
        Debug.Log("transuited out of idle State");
    }

    public override BaseAI CanTransition()
    {
        return null;
    }

    public override BaseState NextState()
    {
        return new PatrolState();
    }

    public override void Update()
    {
        
    }
}
