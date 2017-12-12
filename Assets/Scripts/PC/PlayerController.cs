using UnityEngine;


[RequireComponent(typeof(PlayerFunction))]
public class PlayerController : MonoBehaviour {

    public PlayerFunction pf;
    public float playerHp;
    public float playerSpeed;
    public Vector2 maxVelocity;
    public float dashSpeed;
    public float chargeSpeed;
    public float heavySpeed;
    public float jumpSpeed;
    public float jumpHeight;
    public float dashTime;
    public bool jumped;
    public float lastDirection;

    public Rigidbody2D rb;
    public Animator animator;


    public enum State { Idle, Moving, Dashing, Attacking, Air, Dead }
    public enum SubState { Light, Heavy, PostHeavy, HeavyAir, LightAir, Charge, PostCharge, HeavyThrow, PostHeavyThrow, Default }
    public State state;
    public SubState subState;
    [HideInInspector]
    public float direction;
    [HideInInspector]
    public float left;
    [HideInInspector]
    public float right;

    


    // Use this for initialization
    void Start ()
    {
        direction = transform.position.x;
        state = State.Air;
        jumped = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        ReadInputAttack();

        if (pf.HAxis > 0)
        {
            direction = right;
        }
        if (pf.HAxis < 0)
        {
            direction = left;
        }

        right = transform.position.x + 0.01f * playerSpeed;
        left = transform.position.x + -0.01f * playerSpeed;

        switch (state)
         {
            case State.Idle:
               // ReadInputMove();
                break;

             case State.Moving:
                ReadInputMove();
                pf.MoveCharacter();
                break;
         } 
    }

    private void LateUpdate()
    {
        if (pf.HAxis == 0 && rb.velocity.x != 0 && state != State.Air)
        {
            rb.drag = 50;
        }
        else rb.drag = 0;
    }


    void FixedUpdate()
    {
        //StartCoroutine(pf.PostCharge());
        switch (state)
        {
            case State.Idle:

                break;

            case State.Moving:
                pf.AnimateMovement();
                break;

            case State.Dashing:
                StartCoroutine(pf.Dash());
                break;

            case State.Air:
                pf.AnimateMovement();
                switch (subState)
                {
                    case SubState.HeavyAir:
                        pf.PostHeavyAir();
                        break;

                    case SubState.LightAir:

                        break;
                }
                break;

            case State.Dead:

                break;

            case State.Attacking:

                switch (subState)
                {
                    case SubState.Light:
                        StartCoroutine(pf.WindowTimer());
                        break;

                    case SubState.Heavy:
                        pf.HeavyAttack();
                        break;

                    case SubState.PostHeavy:
                        StartCoroutine(pf.PostHeavy());
                        break;

                    case SubState.Charge:
                        pf.MoveRunAttack();
                        break;

                    case SubState.PostCharge:
                        StartCoroutine(pf.PostCharge());
                        break;

                    case SubState.HeavyThrow:
                        pf.HeavyThrowAir();
                        break;

                    case SubState.PostHeavyThrow:
                        StartCoroutine(pf.PostThrow());
                        break;

                    case SubState.Default:

                        break;
                }
                break;
        }
    }

    public void ReadInputMove()
    {

        if (Input.GetButtonDown("Dash") && state == State.Moving)
        {
            state = State.Dashing;
        }

        if (Input.GetButtonDown("Jump") && pf.Grounded && state == State.Moving)
        {
            pf.GroundJump();
            jumped = true;
        }
    }


    public void ReadInputAttack()
    {
        if (pf.Grounded)
        {
            if (subState == SubState.Default || subState == SubState.Light)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    state = State.Attacking;
                    subState = SubState.Light;
                    StartCoroutine(pf.LightAttackChain());
                }
            }
            if (state == State.Moving)
            {
                if (Input.GetButtonDown("Fire2") && Input.GetKey(KeyCode.W))
                {
                    state = State.Attacking;
                    subState = SubState.HeavyThrow;
                }
                else if (Input.GetButtonDown("Fire2") && subState != SubState.Heavy && Input.GetAxis("Horizontal") == 0)
                {
                    state = State.Attacking;
                    subState = SubState.Heavy;
                }
                else if (Input.GetButtonDown("Fire2") && subState != SubState.Charge)
                {
                    state = State.Attacking;
                    subState = SubState.Charge;
                }
            }
        }
        else
        {
            state = State.Air;
            if (Input.GetButtonDown("Jump"))
            {
                pf.AirJump();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                pf.HeavyAir();
            }
        }

    }

}
