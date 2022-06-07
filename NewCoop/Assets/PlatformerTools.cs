using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerTools : MonoBehaviour
{
    [Header("-----Dash-----")]
    [Range(1,100)][SerializeField] float DashSpeed;
    [Range(1, 500)] [SerializeField] float DashMultipler;
    [Range(0.005f, 1f)] [SerializeField] float DashTime;
    [Range(0.005f, 1f)] [SerializeField] float StartDashTime;
    

    [Header("-----Managers-----")]
    [SerializeField] MovementBehaviour movementBehaviour;
    [SerializeField] MovementManager movementManager;

    [Header("-----Others-----")]
    [SerializeField] Rigidbody2D rb;

    private int direction;
    private float _gravity;

    private void Start()
    {
        DashTime = StartDashTime;
        _gravity = movementManager.gravityScale;
    }

    private void Update()
    {
        #region DASH
        #region Direction
        if (direction == 0)
        {
            if (movementBehaviour.CancelDown == 1 && movementManager.dashCount < 1)
            {
                rb.velocity = Vector2.zero;
                if (movementBehaviour.x == -1)
                {
                    direction = 1;
                }
                else if (movementBehaviour.x == 1)
                {
                    direction = 2;
                }
                else if (movementBehaviour.Jump == 1)
                {
                    direction = 3;
                }
                else if (movementBehaviour.y == -1)
                {
                    direction = 4;
                }
            }
        }
        #endregion

        #region After Press Cancel
        else
        {
            #region Dashed
            if (DashTime <= 0)
            {
                direction = 0;
                DashTime = StartDashTime;
                movementManager.isDashing = false;
                _gravity = rb.gravityScale;
                rb.gravityScale = movementManager.gravityScale;

            }
            #endregion

            #region Dashing
            else
            {
                DashTime -= Time.deltaTime;
                movementManager.dashCount++;
                movementManager.isDashing = true;

                rb.gravityScale = 0;

                switch (direction)
                {
                    case 1:
                        Debug.Log("left dashed");
                        rb.velocity = Vector2.left * DashMultipler * DashSpeed * Time.fixedDeltaTime;
                        break;
                    case 2:
                        Debug.Log("Right dashed");
                        rb.velocity = Vector2.right * DashMultipler * DashSpeed * Time.fixedDeltaTime;
                        break;
                    case 3:
                        Debug.Log("Up Dashed");
                        rb.velocity = Vector2.up * DashMultipler / 2 * DashSpeed * Time.fixedDeltaTime;
                        break;
                    case 4:
                        Debug.Log("Down Dashed");
                        rb.velocity = Vector2.down * DashMultipler * DashSpeed * Time.fixedDeltaTime;
                        break;
                    default:
                        break;
                }
            }
            #endregion
        }
        #endregion
        #endregion

        movementManager.jumpCounter = (movementManager.isGrounded) ? movementManager.totalJump : movementManager.jumpCounter;
        movementManager.jumpTimeCounter = (movementManager.isGrounded) ? movementManager.jumpTime : movementManager.jumpTimeCounter;

        if (movementBehaviour.JumpDown == 1)
        {
            ///<summary>
            ///
            /// eðer bir kez zýplarsa aþþaðýdaki koþullarý sýrasýyla yerine getirecek. Basýlý tutma iþe yaramaz, bütün fonksiyonlar 
            /// bir kereliðine çaðýrýlýr.
            /// 
            /// 
            ///<summart>///
            if (movementManager.jumpCounter > 0)
            {
                if (!movementManager.isGrounded && !movementManager.buffering)
                {
                    movementManager.coyoteJump = true;
                    movementManager.jumpCounter--;
                    movementManager.AddDoubleJump();
                    this.Wait(movementManager.CoyotoTime, () => movementManager.coyoteJump = false);
                }
                else if (movementManager.isGrounded && !movementManager.buffering && !movementManager.coyoteJump)
                {
                    movementManager.jumpCounter--;
                    movementManager.AddJumpForce();
                }
                else if (movementManager.isGrounded && movementManager.coyoteJump)
                {
                    movementManager.jumpCounter--;
                    movementManager.AddCoyotoJumpForce();
                }
            }
            else if (movementManager.isGrounded && !movementManager.coyoteJump && movementManager.buffering)
            {
                movementManager.jumpCounter--;
                movementManager.AddBufferJumpForce();
            }
            else if (movementManager.jumpCounter < 0)
            {
                movementManager.jumpCounter--;
                movementManager.buffering = true;
                this.Wait(movementManager.BufferTime, () => movementManager.buffering = false);
            }
            
        }

        //if (movementBehaviour.Jump == 1)
        //{
        //    movementManager.jumpTimeCounter -= Time.deltaTime;
        //    if (movementManager.jumpCounter > 0)
        //    {
        //        if (movementManager.jumpTimeCounter > 0)
        //        {
        //            JumpCut();
        //        }
        //    }
        //}

    }

    private void FixedUpdate()
    {
        movementManager.isGrounded = Physics2D.OverlapBox(movementManager.checkPos.position, movementManager.checkPosSize, 0, movementManager.layerMask);
        movementManager.OnJump();
    }

    #region Jump Cut
    private void JumpCut()
    {
        rb.velocity += Vector2.up * movementManager.jumpCutMultiplier * movementManager.jumpAmount * Time.deltaTime;
        if (movementManager.isGrounded)
        {
            movementManager.jumpTimeCounter = movementManager.jumpCounter;
        }
    }
    #endregion
}

