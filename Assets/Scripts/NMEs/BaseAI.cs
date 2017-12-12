using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BaseAI: MonoBehaviour{

    public int Health;
    public float MovementSpeed;
    public float rangeOfDetection;
    public float staggerTime;

    // PlayerController pc;
    public Rigidbody2D rb;
    public Animator animator;  
    public Vector3 target;
    public Quaternion leftRot = new Quaternion(0, 180, 0, 0);
    public Quaternion rightRot = new Quaternion(0, 0, 0, 0);
    public bool facingLeft;
    public bool facingRight = true;
    public enum State { Chase, Patrol, Attack, Staggered, Idle, Dead }
    public State state;
    public Vector2 waypoint;
    public Vector2 direction;
    public float staggerTimer;

    public int size;

    public Vector2 Direction
    {
        get
        {
            return direction;
        }
        set
        {
            direction = value;
            Vector2 position = new Vector2(transform.position.x, transform.position.y);
            waypoint = position + direction;
        }
    }

    public virtual bool DetectPlayer()
    { 
        return false;
    }

    public virtual void Awake() { }

    public virtual void Start()
    {
        Direction = Vector2.right / 10;
        state = State.Patrol;
    }

    public virtual void Update() { }

    public virtual void FixedUpdate()
    {
        target = new Vector3(GameManager.Instance.playerController.transform.position.x, GameManager.Instance.transform.position.y, 0);
    }

    public virtual void LateUpdate() { }

    public virtual void OnEnable() { }

    public virtual void OnDisable() { }

    public static void Test()
    {
        
    }

   
}
