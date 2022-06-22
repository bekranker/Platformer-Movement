using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerTools : MonoBehaviour
{

    [Space(25)]
    [Header("-----Jump Support to Corner-----")]
    [Space(15)]
    [BackgroundColor(0f, 1f, 1f, 1f)] [SerializeField] Transform rayPointTopLeft;
    [BackgroundColor(0f, 1f, 1f, 1f)] [SerializeField] Transform rayPointTopRight;
    [BackgroundColor(0f, 1f, 1f, 1f)] [SerializeField] Transform rayPointLeft;
    [BackgroundColor(0f, 1f, 1f, 1f)] [SerializeField] Transform rayPointRight;
    [BackgroundColor(0f, 1f, 1f, 1f)] [SerializeField] Transform rayPointTopLeftFeed;
    [BackgroundColor(0f, 1f, 1f, 1f)] [SerializeField] Transform rayPointTopRightFeed;
    [BackgroundColor(0f, 1f, 1f, 1f)] [SerializeField] Transform rayPointLeftFeed;
    [BackgroundColor(0f, 1f, 1f, 1f)] [SerializeField] Transform rayPointRightFeed;
    [Space(15)]
    [BackgroundColor(0f, 1f, 1f, 1f)] [SerializeField] Vector3 TopRayDistance;
    [BackgroundColor(0f, 1f, 1f, 1f)] [SerializeField] Vector3 RayDistance;
    [BackgroundColor(0f, 1f, 1f, 1f)] [SerializeField] Vector3 FeedRayDistance;
    [Space(15)]
    [BackgroundColor(0f, 1f, 1f, 1f)] [Range(0.005f, 10f)][SerializeField] float DistanceOfRays;
    [BackgroundColor(0f, 1f, 1f, 1f)] [Range(0.005f, 10f)][SerializeField] float FeedDistanceOfRays;
    [BackgroundColor(0f, 1f, 1f, 1f)] [Range(0.005f, 100f)][SerializeField] float JumpSupportAmount;
    [BackgroundColor(0f, 1f, 1f, 1f)] [Range(0.005f, 1f)][SerializeField] float JumpSupportMultiplyAmount;

    [Space(25)]
    [Header("-----Dash-----")]
    [Space(15)]
    [BackgroundColor(1f, 1f, 0f, 1f)] [Range(1f,100f)][SerializeField] float DashSpeed;
    [BackgroundColor(1f, 1f, 0f, 1f)] [Range(1f, 500f)] [SerializeField] float DashMultipler;
    [BackgroundColor(1f, 1f, 0f, 1f)] [Range(0.005f, 1f)] [SerializeField] float DashTime;
    [BackgroundColor(1f, 1f, 0f, 1f)] [Range(0.005f, 1f)] [SerializeField] float StartDashTime;

    [Space(25)]
    [Header("-----Managers-----")]
    [Space(15)]
    [BackgroundColor(1f, 0f, 0f, 1f)] [SerializeField] MovementBehaviour Inputs;
    [BackgroundColor(1f, 0f, 0f, 1f)] [SerializeField] MovementManager movementManager;

    [Space(25)]
    [Header("-----Others-----")]
    [Space(15)]
    [BackgroundColor(0f, 1f, 0f, 1f)] [SerializeField] Rigidbody2D rb;

    private int direction;
    private float _gravity;

    private Vector3 TopRightRay;
    private Vector3 TopLeftRay;
    private Vector3 RightRay;
    private Vector3 LeftRay;
    private Vector3 TopLeftFeedRay;
    private Vector3 TopRightFeedRay;
    private Vector3 LeftFeedRay;
    private Vector3 RightFeedRay;

    private bool IsCanDoubleJump;
    private void Start()
    {
        DashTime = StartDashTime;
        _gravity = movementManager.gravityScale;
    }

    private void Update()
    {

        #region Corner Jump Support
        TopRightRay = rayPointTopRight.position + TopRayDistance;
        TopLeftRay = rayPointTopLeft.position - TopRayDistance;
        RightRay = rayPointRight.position + RayDistance;
        LeftRay = rayPointLeft.position - RayDistance;
        LeftFeedRay = rayPointLeftFeed.position - FeedRayDistance;
        RightFeedRay = rayPointRightFeed.position + FeedRayDistance;
        TopLeftFeedRay = rayPointTopLeftFeed.position - FeedRayDistance;
        TopRightFeedRay = rayPointTopRightFeed.position + FeedRayDistance;

        JumpSupport();

        #endregion

        #region DASH
        #region Direction
        if (direction == 0)
        {
            if (Inputs.CancelDown == 1 && movementManager.dashCount < 1)
            {
                rb.velocity = Vector2.zero;
                if (Inputs.x == -1)
                {
                    direction = 1;
                }
                else if (Inputs.x == 1)
                {
                    direction = 2;
                }
                else if (Inputs.Jump == 1 || Inputs.JumpDown == 1)
                {
                    direction = 3;
                }
                else if (Inputs.y == -1)
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
                        rb.velocity = Vector2.up * DashMultipler / 1.5f  * DashSpeed * Time.fixedDeltaTime;
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

        #region calculatings
        movementManager.coyoteTimeCounter = (!movementManager.coyoteJump) ? movementManager.coyoteTimeCounter - Time.deltaTime : movementManager.coyoteTimeCounter = 0.15f;
        movementManager.jumpTimeCounter = (movementManager.isGrounded) ? movementManager.jumpTime : movementManager.jumpTimeCounter;
        //if (!movementManager.isGrounded) this.Wait(movementManager.CoyotoTime, () => movementManager.coyoteJump = false);
        #endregion

        #region Jumping
        if (Inputs.JumpDown == 1)
        {
            movementManager.JumpCutTimeCounter = movementManager.JumpCutTimer;
            ///<summary>
            ///
            /// e�er bir kez z�plarsa a��a��daki ko�ullar� s�ras�yla yerine getirecek. Bas�l� tutma i�e yaramaz, b�t�n fonksiyonlar 
            /// bir kereli�ine �a��r�l�r.
            /// 
            /// 
            ///<summary>///
            if (movementManager.jumpCounter > 0)
            {
                if (!movementManager.buffering && movementManager.isGrounded)
                {
                    movementManager.coyoteJump = false;

                    movementManager.jumpCounter--;
                    movementManager.AddJumpForce();
                }
                else if (!movementManager.isGrounded && movementManager.coyoteTimeCounter <= 0)
                {
                    IsCanDoubleJump = true;
                    if (IsCanDoubleJump)
                    {

                        movementManager.jumpCounter--;
                        movementManager.AddDoubleJumpForce();
                    }
                    movementManager.coyoteJump = true;
                }
                if (!movementManager.isGrounded && !movementManager.buffering)
                {
                    if (movementManager.coyoteTimeCounter > 0)
                    {

                        movementManager.jumpCounter--;
                        movementManager.AddCoyoteTimeForce();
                        movementManager.coyoteJump = false;
                    }
                }
            }
            else if(movementManager.jumpCounter <= 0)
            {
                IsCanDoubleJump = false;

                movementManager.buffering = true;
                movementManager.jumpCounter--;
                this.Wait(movementManager.BufferTime, () => movementManager.buffering = false);
            }
        }

        //buras� s�rekli �al���r bu y�zden burada de�erleri true ||false || calculate yap�lmamas� gerekli.
        if (Inputs.Jump == 1)
        {
            movementManager.jumpTimeCounter -= Time.deltaTime;
            if (movementManager.jumpCounter >= 0)
            {
                if (movementManager.jumpTimeCounter > 0 && !movementManager.isGrounded)
                {
                    movementManager.JumpCut();
                }
            }
        }
        #endregion
    }

    private void FixedUpdate()
    {
        movementManager.isGrounded = Physics2D.OverlapBox(movementManager.checkPos.position, movementManager.checkPosSize, 0, movementManager.layerMask);
        movementManager.OnJump();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        #region Jump Buffer
        #region if we are touched the ground
        if (collision.gameObject.tag == "Ground")
        {
            Vector2 direction = collision.GetContact(0).normal;
            if (direction.y > 0)
            {
                movementManager.coyoteJump = false;
                movementManager.dashCount = 0;
                
                if (movementManager.buffering)
                {
                    movementManager.jumpCounter = 1;
                    Debug.Log("Buffering");
                    movementManager.AddBufferJumpForce();
                    movementManager.buffering = false;
                }
                else if(!movementManager.buffering)
                {
                    movementManager.jumpCounter = movementManager.totalJump;
                }
                

            }
            if (Inputs.Jump == 1 || Inputs.JumpDown == 1)
            {
                if (direction.x < 0)
                {
                    Debug.Log("Sa� k��e de�di");
                    transform.position += Vector3.up * 10 * Time.fixedDeltaTime;
                }
                if (direction.x > 0)
                {
                    Debug.Log("Sol k��e de�di");
                    transform.position += Vector3.up * 10 * Time.fixedDeltaTime;
                }
            }
        }
        #endregion
        #endregion
    }

    #region Jump Support System
    private void JumpSupport()
    {
        if (Inputs.Jump == 1 || Inputs.JumpDown == 1)
        {
            OnJumpingSupportRay();
        }
    }

    private void OnJumpingSupportRay()
    {
        //Sadece bu ikisinden birisi de�iyorsa Corner support �al���cak
        RaycastHit2D TopLeft = Physics2D.Raycast(TopLeftRay, Vector2.up * DistanceOfRays);
        RaycastHit2D TopRight = Physics2D.Raycast(TopRightRay, Vector2.up * DistanceOfRays);

        //Yukar�dakiler de�erken bunlardan ehrhangi biri de�iyorsa corner support �al��mayacak.
        RaycastHit2D Left = Physics2D.Raycast(LeftRay, Vector2.up * DistanceOfRays);
        RaycastHit2D Right = Physics2D.Raycast(RightRay, Vector2.up * DistanceOfRays);

        if (TopLeft.collider == null && TopRight.collider == null) return;
        else
        {
            if (TopRight.collider.tag == "Ground" && Right.collider.tag != "Ground")
            {
                transform.position += Vector3.left * 10 * JumpSupportAmount * Time.deltaTime;
            }
            if (TopLeft.collider.tag == "Ground" && Left.collider.tag != "Ground")
            {
                transform.position += Vector3.right * 10 * JumpSupportAmount * Time.deltaTime;
            }
        }
    }
    #endregion

    #region Draws
    private void OnDrawGizmos()
    {

        Gizmos.DrawWireSphere(movementManager.checkPos.position, 0.3f);

        Debug.DrawRay(TopLeftRay, Vector2.up * DistanceOfRays, Color.yellow);
        Debug.DrawRay(TopRightRay, Vector2.up * DistanceOfRays, Color.yellow);
        Debug.DrawRay(RightRay, Vector2.up * DistanceOfRays, Color.green);
        Debug.DrawRay(LeftRay, Vector2.up * DistanceOfRays, Color.green);
    }
    #endregion
}
