using System;
using System.Collections;
using UnityEngine;


public class Skeleton : BaseAI, IBaseFunction {

    private BaseState _currentState;
    private bool _isPlayerDetected;
 
    public bool IsPlayerDetected
    {
        get
        {
            return _isPlayerDetected;
        }

        set
        {
            _isPlayerDetected = value;
        }
    }

    // Use this for initialization
    public override void Start ()
    {
        GameManager.Instance.enemies.Add(this);
        _currentState = new PatrolState();
    }

    public override void Update()
    {
 
        if (_currentState.CanTransition() == this)
        {
            _currentState.TransitionOut();
            _currentState = _currentState.NextState();
            _currentState.TransitionIn();
        }
        else
            _currentState.Update();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Weapon")
        {
            if (GameManager.Instance.playerController.subState == PlayerController.SubState.Light)
            {
                if(state == State.Idle)
                {
                    staggerTimer += staggerTime;
                }
                else
                {
                    Health -= 1;
                    state = State.Idle;
                }
            }
            else
            {
                if(state == State.Idle)
                {
                    staggerTimer += staggerTime;
                }
                else
                {
                    Health -= 1;
                    state = State.Idle;
                }                
            }
        }
    }

    public IEnumerator Stagger()
    {
        if (Health <= 0)
        {

        }

        animator.Play("Skeleton_Idle");
        animator.SetBool("Charge", false);
        animator.SetTrigger("Idle");

        yield return new WaitForSeconds(staggerTimer);
        staggerTimer = 0;

        if (Health > 0)
        {
            if (Vector2.Distance(transform.position, target) > 2.5f)
            {
                state = State.Chase;
            }
            state = State.Attack;
        }
        else state = State.Dead;

    }

    public void Patrol()
    {
        if (Vector2.Distance(transform.position, GameManager.Instance.playerController.transform.position) < rangeOfDetection)
        {
            if (GameManager.Instance.playerController.transform.position.x < transform.position.x)
            {
                facingRight = false;
                facingLeft = true;
                Direction = Vector2.left / 10;
                transform.rotation = leftRot;
            }
            else if (GameManager.Instance.playerController.transform.position.x > transform.position.x)
            {
                facingRight = true;
                facingLeft = false;
                Direction = Vector2.right / 10;
                transform.rotation = rightRot;
            }
            IsPlayerDetected = true;
        }
        else
        {
            Direction = Direction;
        }
        animator.SetTrigger("Walk");
        animator.speed = 0.5f;
        Vector2 move = Vector2.MoveTowards(transform.position, waypoint, MovementSpeed / 2 * Time.deltaTime);
        rb.MovePosition(move);
    }


    public void Attack()
    {
        throw new NotImplementedException();
    }

    public void Dead()
    {
        throw new NotImplementedException();
    }

    public void Chase()
    {
        throw new NotImplementedException();
    }

    public void Idle()
    {
        throw new NotImplementedException();
    }

    //public bool PlayerDetected()
    //{
    //    return playerDetected;
    //}
}
