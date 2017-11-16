using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script is in test and everything will be changed. Nothing here is final.
public class Skeleton : MonoBehaviour {

    [SerializeField] private float skeletonSpeed;
    public float rangeOfDetection;
    public float lightStaggerTimer;
    public float heavyStaggerTimer;
    private float staggerTimer;
    //private bool staggered;

    private PlayerController pc;
    private Rigidbody2D rb;
    private Animator animator;

    public bool playerDetected = false;

    public float skeletonHp;

    private Vector3 target;

    private Quaternion leftRot = new Quaternion(0, 180, 0, 0);
    private Quaternion rightRot = new Quaternion(0, 0, 0, 0);
    private bool facingLeft;
    private bool facingRight = true;

    public enum State { Chase, Patrol, Attack, Wait, Dead}
    public State state;

    private Vector2 waypoint;
    private Vector2 direction;

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

    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start ()
    {
        pc = GameManager.Gm.playerController;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Direction = Vector2.right /10;
        state = State.Patrol;
    }
	


	// Update is called once per frame
	void FixedUpdate ()
    {
        target = new Vector3(pc.transform.position.x, pc.transform.position.y, 0);

        switch (state)
        {
            case State.Patrol:
                Patrol();
                break;

            case State.Chase:
                ChasePlayer();
                break;

            case State.Attack:
                StartCoroutine(Attack());
                break;

            case State.Wait:
                Wait();
                break;

            case State.Dead:
                StartCoroutine(Dead());
                break;
        }
	}


    public void Wait()
    {
        StartCoroutine(Stagger());
    }

    public void Patrol()
    {
        animator.SetBool("Charge", false);
        if (!playerDetected)
        {       
            if(Vector2.Distance(transform.position, pc.transform.position) < rangeOfDetection)
            {
                if (pc.transform.position.x < transform.position.x)
                {
                    playerDetected = true;
                    facingRight = false;
                    facingLeft = true;
                    Direction = Vector2.left / 10;
                    transform.rotation = leftRot;
                }
                else if (pc.transform.position.x > transform.position.x)
                {
                    playerDetected = true;
                    facingRight = true;
                    facingLeft = false;
                    Direction = Vector2.right / 10;
                    transform.rotation = rightRot;
                }
            }
            else
            {
                Direction = Direction;
            }

            //animator.SetTrigger("Walk");
            //animator.speed = 0.5f;
            //Vector2 move = Vector2.MoveTowards(transform.position, waypoint, skeletonSpeed / 2 * Time.deltaTime);
            //rb.MovePosition(move);
        }
        else
        {
            animator.speed = 1;
            state = State.Chase;
            
        }
       
    }

    public void ChasePlayer()
    {
       StartCoroutine(DirectTowardsTarget());
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

        if (Vector3.Distance(transform.position, target) > 2.1f)
        {
            Direction = Direction;
            animator.speed = 0.5f;
            animator.SetBool("Charge", true);
            Vector2 move = Vector2.MoveTowards(transform.position, waypoint, skeletonSpeed);
            rb.MovePosition(move);
        }
        else
        {
            animator.SetBool("Charge", false);
            animator.SetTrigger("ChargeAttack");
            //yield return new WaitForSeconds(0);
            state = State.Attack;
            StartCoroutine(Attack());
        }
    }

    public IEnumerator Attack()
    {
        if (Vector3.Distance(transform.position, target) > 2.5f)
        {
            state = State.Chase;
            
        }
        else if (state == State.Attack)
        {
            int randomFloat = Random.Range(0, 2);
            yield return new WaitForSeconds(randomFloat);
            int randomInt = Random.Range(0, 100);
            if(randomInt > 49)
            {
                animator.SetTrigger("Attack1");
            }
            else
            {
                animator.SetTrigger("Attack2");
            }
            
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Weapon")
        {
            if (pc.subState == PlayerController.SubState.Light)
            {
                if(state == State.Wait)
                {
                    staggerTimer += lightStaggerTimer;
                }
                else
                {
                    skeletonHp -= 1;
                    state = State.Wait;
                }
            }
            else
            {
                if(state == State.Wait)
                {
                    staggerTimer += heavyStaggerTimer;
                }
                else
                {
                    skeletonHp -= 1;
                    state = State.Wait;
                }                
            }
        }
    }

    public IEnumerator Stagger()
    {
        if (skeletonHp <= 0)
        {
            StartCoroutine(Dead());
        }
          
       Debug.Log("Staggered");

        animator.Play("Skeleton_Idle");
        animator.SetBool("Charge", false);
        animator.SetTrigger("Idle");

        yield return new WaitForSeconds(staggerTimer);
        staggerTimer = 0;

        if(skeletonHp > 0)
        {
            if (Vector2.Distance(transform.position, target) > 2.5f)
            {
                state = State.Chase;
            }
            state = State.Attack;
        }
        else state = State.Dead;

    }

    public IEnumerator DirectTowardsTarget()
    {
        if (target.x > transform.position.x )
        {
            if(facingRight != true)
            {
                facingRight = true;
                yield return new WaitForSeconds(0.5f);
                Direction = Vector2.right / 10;
                facingLeft = false;
                transform.rotation = rightRot;
            }   
        }
        else if (target.x < transform.position.x)
        {
            if (facingLeft != true)
            {
                facingLeft = true;
                yield return new WaitForSeconds(0.5f);
                Direction = Vector2.left / 10;
                facingRight = false;
                transform.rotation = leftRot;
            }
        }
    }

 

    public IEnumerator Dead()
    {
        if (skeletonHp <= 0)
        {
            state = State.Dead;
            if (facingRight)
            {
                transform.rotation = leftRot;
            }
            else
            {
                transform.rotation = rightRot;
            }

            animator.SetTrigger("Dead");
            yield return new WaitForSeconds(2f);
            gameObject.SetActive(false);
        }
        else state = State.Wait;
        
    }

}
