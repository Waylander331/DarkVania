using System;
using UnityEngine;

public class PatrolState : BaseState
{
    public delegate BaseAI PatrolUpdate();
    public PatrolUpdate patrolUpdate;

    public override void TransitionIn()
    {
        Debug.Log("Transited into Patrol");
    }

    public override void TransitionOut()
    {
        Debug.Log("Transited out of Patrol");
    }

    public override BaseAI CanTransition()
    {
        foreach (IBaseFunction enemy in GameManager.Instance.enemies)
        {
            if(enemy.IsPlayerDetected)
            {
                return (BaseAI)enemy;
            }
        }
        return null;
    }

    public override BaseState NextState()
    {
        return new ChaseState();
    }

    public override void Update()
    {
        for (int i = 0; i < GameManager.Instance.enemies.Count; i++)
        {
            var enemy = (IBaseFunction)GameManager.Instance.enemies[i];
            enemy.Patrol();
        }

    }
    //public void Patrol()
    //{
        
    //}
}
