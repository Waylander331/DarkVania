using UnityEngine;
using System.Collections;

public class PlayerFunction : MonoBehaviour {

    public GameManager gm;
    private PlayerController pc;

    private float baseSpeed;
    private bool grounded;
    private int attackCount;
    public float timer;
    private float resetTimer;
   
  

    private Quaternion leftRot = new Quaternion(0, 180, 0, 0);
    private Quaternion rightRot = new Quaternion(0, 0, 0, 0);
    private bool facingLeft;
    private bool facingRight;

    private bool attacking;
    private bool dashing;
    private bool runAttack = false;
    private bool windowOfOportunity = false;
    private bool stopCharge = false;
    private bool heavyAttack = false;
    private bool stopHeavy = false;
    private bool heavyThrow = false;
    private bool stopHeavyThrow = false;
    private bool heavyAir = false;
    private bool stopHeavyAir = false;
    private int lightAirCount = 0;

    private bool deadAnimPlaying = false;

	// Use this for initialization
	void Start ()
    {
        pc = GetComponent<PlayerController>();
        grounded = false;
        baseSpeed = pc.playerSpeed;
        resetTimer = timer;
        gm = GameManager.Gm;
	}
	
	// Update is called once per frame
	void Update ()
    {
        PlayerCustomPhysics();
    }


    public void GroundJump()
    {
        grounded = false;
        StartCoroutine(SetJumpedToFalse());
        pc.animator.SetBool("Grounded", false);
        float jumpDirection = 0f;
        if (facingRight) { jumpDirection = Mathf.Ceil(Input.GetAxis("Horizontal")); }
        else if (facingLeft) { jumpDirection = Mathf.Floor(Input.GetAxis("Horizontal")); }
        
        Vector2 jump = new Vector2(jumpDirection * pc.jumpSpeed, (pc.jumpHeight) * 100);
        pc.rb.AddForce(jump);
    }


    private IEnumerator SetJumpedToFalse()
    {
        yield return new WaitForSeconds(0.1f);
        pc.jumped = false;
        //StopCoroutine(SetJumpedToFalse());
    }


    
    public void JumpedOnEnemy(int side)
    {
        float jumpDirection = 0;
        if(side == 0)
        {
            jumpDirection = -10;
        }
        else if(side ==  1)
        {
            jumpDirection = 10;
        }
        Vector2 jump = new Vector2(jumpDirection * 100, 0);
        pc.rb.AddForce(jump);
    }

    // Temporary velocity control
    private void PhysicsControl()
    {
        if (pc.rb.velocity.y > pc.maxVelocity.y || pc.rb.velocity.y < -pc.maxVelocity.y)
        {
            pc.state = PlayerController.State.Air;
        }

        if (pc.rb.velocity.x > pc.maxVelocity.x + 1)
        {
            pc.rb.velocity = new Vector2(pc.maxVelocity.x, pc.rb.velocity.y);
        }
        if (pc.rb.velocity.x < -pc.maxVelocity.x - 1)
        {
            pc.rb.velocity = new Vector2(-pc.maxVelocity.x, pc.rb.velocity.y);
        }

       
    }


    public void MoveCharacter()
    {
        if (grounded)
        {
            //Vector2 move = new Vector2(pc.direction, transform.position.y);
            //pc.rb.MovePosition(move);

            if (pc.rb.velocity.x <= pc.maxVelocity.x && facingRight)
            {
                pc.rb.AddForce(new Vector2(HAxis * 10, 0), ForceMode2D.Impulse);
            }
            if (pc.rb.velocity.x >= -pc.maxVelocity.x && facingLeft)
            {
                pc.rb.AddForce(new Vector2(HAxis * 10, 0), ForceMode2D.Impulse);
            }
        }
    }

    #region Dash section

    public IEnumerator Dash()
    {
        if (grounded)
        {
            dashing = true;
            pc.playerSpeed = pc.dashSpeed;
            pc.animator.SetTrigger("Dash");

            if (facingLeft)
            {
                pc.direction = pc.left;
            }
            else
            {
                pc.direction = pc.right;
            }

            Vector2 movement = new Vector2(pc.direction, transform.position.y);
            pc.rb.MovePosition(movement);

            yield return new WaitForSeconds(pc.dashTime);
            dashing = false;
            pc.playerSpeed = baseSpeed;
            pc.state = PlayerController.State.Moving;
        }
    }
#endregion

    #region Light Attack Chain section

    public IEnumerator LightAttackChain()
    {
        if(attackCount == 0)
        {
            pc.animator.SetFloat("Speed", 0);
            pc.animator.SetTrigger("LightAttack1");
            attackCount = 1;
        }
        else if(attackCount == 1 && windowOfOportunity)
        {
            windowOfOportunity = false;
            pc.animator.SetTrigger("LightAttack2");
            timer = resetTimer;
            attackCount = 2;
        }
        else if(attackCount == 2 && windowOfOportunity)
        {
            windowOfOportunity = false;
            pc.animator.SetTrigger("LightAttack3");
            yield return new WaitForSeconds(0.6f);
            ResetAttackState();
        }
    }

    public IEnumerator WindowTimer()
    {
        if(windowOfOportunity)
        {
            timer -= Time.deltaTime;
            if(timer <= 0 || Input.GetAxis("Horizontal") != 0 || Input.GetButtonDown("Jump"))
            {
                windowOfOportunity = false;
                if(Input.GetAxis("Horizontal") != 0)
                {
                    yield return new WaitForSeconds(0.01f);
                    ResetAttackState();
                }
                else
                ResetAttackState();
            }
        }
    }

    public void WindowOfOportunity()
    {
        windowOfOportunity = true;
    }

    public void ResetAttackState()
    {
        pc.state = PlayerController.State.Moving;
        pc.subState = PlayerController.SubState.Default;
        attackCount = 0;
        timer = resetTimer;
    }
#endregion

    #region Run Attack section

    public void MoveRunAttack()
    {
        if (runAttack)
        {
            pc.playerSpeed = pc.chargeSpeed;
            if (facingLeft)
            {
                //pc.direction = pc.left;
                
            }
            else
            {
                // pc.direction = pc.right;
                
            }

            //Vector2 movement = new Vector2(pc.direction, transform.position.y);
            //pc.rb.MovePosition(movement);
        }
        else
        {
            runAttack = true;
            pc.animator.SetTrigger("RunAttack");
        }
    }

    public void StopRunAttack()
    {
        //pc.playerSpeed = 0;
        pc.animator.SetFloat("Speed", 0);
        runAttack = false;
        stopCharge = true;
        pc.subState = PlayerController.SubState.PostCharge;
        pc.animator.speed = 0;
    }

    public IEnumerator PostCharge()
    {
        if(stopCharge)
        {
            stopCharge = false;
            yield return new WaitForSeconds(0.4f);
            pc.animator.speed = 1;
            yield return new WaitForSeconds(0.2f);
            pc.state = PlayerController.State.Moving;
            pc.subState = PlayerController.SubState.Default;
            //pc.playerSpeed = baseSpeed;
        }
    }
#endregion

    #region Standing Heavy Attack section

    public void HeavyAttack()
    {
        if (heavyAttack)
        {
            pc.playerSpeed = pc.heavySpeed;
            if (facingLeft)
            {
                pc.direction = pc.left;
            }
            else
            {
                pc.direction = pc.right;
            }

            Vector2 movement = new Vector2(pc.direction, transform.position.y);
            pc.rb.MovePosition(movement);
        }
        else
        {
            heavyAttack = true;
            pc.animator.speed = 1.5f;
            pc.animator.SetTrigger("HeavyAttack");
        }
    }

    public void StopHeavy()
    {
        pc.animator.speed = 0;
        heavyAttack = false;
        stopHeavy = true;
        pc.subState = PlayerController.SubState.PostHeavy;
    }

    public IEnumerator PostHeavy()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            pc.animator.speed = 1;
            pc.subState = PlayerController.SubState.HeavyThrow;
        }


        if (stopHeavy)
        {
            stopHeavy = false;
            yield return new WaitForSeconds(0.3f);
            if(pc.subState != PlayerController.SubState.HeavyThrow)
            {
                pc.animator.speed = 1;
                pc.playerSpeed = baseSpeed;
                pc.state = PlayerController.State.Moving;
                pc.subState = PlayerController.SubState.Default;
            }
           
        }
    }
#endregion

    #region Heavy Throw Air section

    public void HeavyThrowAir()
    {
        
        if (heavyThrow)
        {
            pc.playerSpeed = pc.heavySpeed;
            if (facingLeft)
            {
                pc.direction = pc.left;
            }
            else
            {
                pc.direction = pc.right;
            }

            Vector2 movement = new Vector2(pc.direction, transform.position.y);
            pc.rb.MovePosition(movement);
        }
        else
        {
            heavyThrow = true;
            pc.animator.SetTrigger("HeavyThrow");
        }
    }

    public void StopThrow()
    {
        heavyThrow = false;
        stopHeavyThrow = true;
        pc.subState = PlayerController.SubState.PostHeavyThrow;
    }

    public IEnumerator PostThrow()
    {
        if(stopHeavyThrow)
        {
            stopHeavyThrow = false;
            pc.playerSpeed = baseSpeed;
            yield return new WaitForSeconds(0.15f);
            pc.state = PlayerController.State.Moving;
            pc.subState = PlayerController.SubState.Default;
        }
    }
    #endregion
   
    #region Heavy Air attack section

    public void HeavyAir()
    {
        if(!heavyAir)
        {
            pc.subState = PlayerController.SubState.HeavyAir;
            heavyAir = true;
            pc.animator.SetTrigger("HeavyAir");
        }
    }

    public void StopHeavyAir()
    {
        stopHeavyAir = true;
    }

    public void PostHeavyAir()
    {
        if (grounded)
        {
            stopHeavyAir = true;
        }

        if (stopHeavyAir)
        {
            stopHeavyAir = false;
            pc.subState = PlayerController.SubState.Default;
            heavyAir = false;
        }
       
    }
    #endregion

    #region Air Jump
    public void AirJump()
    {
        pc.animator.SetTrigger("LightAir");
        float jumpHeight = 0;

        if (lightAirCount < 3)
        {
            lightAirCount += 1;
            float jumpDirection = 0;

            if (HAxis != 0)
            {
                if (HAxis > 0.1)
                {
                    if (pc.rb.velocity.x <= 0)
                    {
                        pc.rb.velocity = new Vector2(0, pc.rb.velocity.y);
                        jumpDirection = 10 * 100;
                    }
                    else
                    {
                        jumpDirection = 30;
                    }
                }

                if (HAxis < 0.1)
                {
                    if (pc.rb.velocity.x >= 0)
                    {
                        pc.rb.velocity = new Vector2(0, pc.rb.velocity.y);
                        jumpDirection = -10 * 100;
                    }
                    else
                    {
                        jumpDirection = -30;
                    }
                }
            }
            else jumpDirection = 0;

            if (pc.rb.velocity.y < 0)
            {
                pc.rb.velocity = new Vector2(pc.rb.velocity.x, 0);
                jumpHeight = pc.jumpHeight / 1.2f;
            }
            else
            {
                jumpHeight = pc.jumpHeight / 2.7f;
            }

            Vector2 jump = new Vector2(jumpDirection, jumpHeight * 100);
            pc.rb.AddForce(jump);
        }
    }
    #endregion
    
    public void OnCollisionEnterGround()
    {
        lightAirCount = 0;
        grounded = true;
        pc.jumped = false;
        PostHeavyAir();
        pc.animator.SetBool("Grounded", true);
        pc.state = PlayerController.State.Moving;
    }

    public void OnCollisionExitGround()
    {
        pc.animator.SetBool("Grounded", false);
        grounded = false;

        //float pushDirection = 0;
        //if (!pc.jumped)
        //{
        //    if (facingLeft)
        //    {
        //        pushDirection = -450;
        //    }
        //    else
        //    {
        //        pushDirection = 450;
        //    }
        //    Vector2 ledgePush = new Vector2(pushDirection, 0);
        //    pc.rb.AddForce(ledgePush);
        //}

    }

   
    public void OnCollisionStayGround()
    {
        pc.animator.SetBool("Grounded", true);
        grounded = true;
        pc.rb.gravityScale = 0;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "LesserNme")
        {
            RemoveHealth(0.5f);
        }
        if(collision.tag == "GreaterNme")
        {
            RemoveHealth(1);
        }
    }

    public void RemoveHealth(float removeHp)
    {
        pc.playerHp -= removeHp;
        CheckPlayerHealth();
    }

    public void CheckPlayerHealth()
    {
        if (pc.playerHp <= 0)
        {
            gm.PlayerDead();
            Dead();
        }
    }


    public void Dead()
    {
        if (!deadAnimPlaying)
        {
            deadAnimPlaying = true;
            pc.animator.SetTrigger("Dead");
           
        }
    }


    private void PlayerCustomPhysics()
    {
        if (pc.rb.velocity.y > pc.maxVelocity.y || pc.rb.velocity.y < -pc.maxVelocity.y)
        {
            
            pc.state = PlayerController.State.Air;
            grounded = false;
            pc.animator.SetBool("Grounded", false); 
        }
    }

    public void AnimateMovement()
    {

        pc.animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        pc.animator.SetFloat("vSpeed", pc.rb.velocity.y);

        if(Input.GetAxis("Horizontal") > 0 && transform.rotation != rightRot)
        {
            facingLeft = false;
            facingRight = true;
            transform.rotation = rightRot;
        }
        else if (Input.GetAxis("Horizontal") < 0 && transform.rotation != leftRot)
        {
            facingRight = false;
            facingLeft = true;
            transform.rotation = leftRot;
        }
    }

    public bool Grounded
    {
        get { return grounded; }
    }

    public float HAxis
    {
        get { return Input.GetAxis("Horizontal"); }
    } 
}
