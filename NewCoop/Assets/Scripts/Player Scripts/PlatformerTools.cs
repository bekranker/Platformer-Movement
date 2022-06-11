using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerTools : MonoBehaviour
{

    [Header("-----Jump Support to Corner-----")]
    [SerializeField] Transform rayPointTopLeft;
    [SerializeField] Transform rayPointTopRight;
    [SerializeField] Transform rayPointLeft;
    [SerializeField] Transform rayPointRight;
    [SerializeField] Vector3 TopRayDistance;
    [SerializeField] Vector3 RayDistance;
    [Range(0f, 100f)][SerializeField] float DistanceOfRays;
    [Range(0.005f, 100f)][SerializeField] float JumpSupportAmount;
    [Range(0.005f, 1f)][SerializeField] float JumpSupportMultiplyAmount;


    [Header("-----Dash-----")]
    [Range(1f,100f)][SerializeField] float DashSpeed;
    [Range(1f, 500f)] [SerializeField] float DashMultipler;
    [Range(0.005f, 1f)] [SerializeField] float DashTime;
    [Range(0.005f, 1f)] [SerializeField] float StartDashTime;
    

    [Header("-----Managers-----")]
    [SerializeField] MovementBehaviour Inputs;
    [SerializeField] MovementManager movementManager;

    [Header("-----Others-----")]
    [SerializeField] Rigidbody2D rb;

    private int direction;
    private float _gravity;

    private Vector3 TopRightRay;
    private Vector3 TopLeftRay;
    private Vector3 RightRay;
    private Vector3 LeftRay;


    private bool leftContact;
    private bool RightContact;

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
        movementManager.coyoteTimeCounter = (movementManager.coyoteJump) ? movementManager.coyoteTimeCounter - Time.deltaTime : movementManager.coyoteTimeCounter = 0.15f;
        movementManager.jumpTimeCounter = (movementManager.isGrounded) ? movementManager.jumpTime : movementManager.jumpTimeCounter;
        if (!movementManager.isGrounded) this.Wait(movementManager.CoyotoTime, () => movementManager.coyoteJump = false);
        #endregion

        #region Jumping
        if (Inputs.JumpDown == 1)
        {
            ///<summary>
            ///
            /// eðer bir kez zýplarsa aþþaðýdaki koþullarý sýrasýyla yerine getirecek. Basýlý tutma iþe yaramaz, bütün fonksiyonlar 
            /// bir kereliðine çaðýrýlýr.
            /// 
            /// 
            ///<summary>///
            if (movementManager.jumpCounter > 0)
            {
                if (movementManager.coyoteTimeCounter > 0 && !movementManager.buffering)
                {
                    Debug.Log("Normal Jump is working");
                    movementManager.jumpCounter--;
                    movementManager.AddJumpForce();
                }
            }
            else
            {
                movementManager.buffering = true;
                movementManager.jumpCounter--;
                this.Wait(movementManager.BufferTime, () => movementManager.buffering = false);
            }
        }

        if (Inputs.Jump == 1)
        {
            //burasý sürekli çalýþýr bu yüzden burada deðerleri true ||false || calculate yapýlmamasý gerekli.
            movementManager.JumpCut();
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
            if (direction.y == 1)
            {
                movementManager.dashCount = 0;
                movementManager.jumpCounter = movementManager.totalJump;
                if (!movementManager.coyoteJump && movementManager.buffering)
                {
                    movementManager.jumpCounter--;
                    movementManager.AddBufferJumpForce();
                    movementManager.buffering = false;
                }
            }
            if (direction.x == 1)
            {
                transform.position += Vector3.up + Vector3.right * JumpSupportMultiplyAmount * JumpSupportAmount * Time.fixedDeltaTime;
            }
            if (direction.x == -1)
            {
                transform.position += Vector3.up + Vector3.left * JumpSupportMultiplyAmount * JumpSupportAmount * Time.fixedDeltaTime;
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
        //Sadece bu ikisinden birisi deðiyorsa Corner support çalýþýcak
        RaycastHit2D TopLeft = Physics2D.Raycast(TopLeftRay, Vector2.up * DistanceOfRays);
        RaycastHit2D TopRight = Physics2D.Raycast(TopRightRay, Vector2.up * DistanceOfRays);

        //Yukarýdakiler deðerken bunlardan ehrhangi biri deðiyorsa corner support çalýþmayacak.
        RaycastHit2D Left = Physics2D.Raycast(LeftRay, Vector2.up * DistanceOfRays);
        RaycastHit2D Right = Physics2D.Raycast(RightRay, Vector2.up * DistanceOfRays);

        if (TopLeft.collider == null && TopRight.collider == null) return;
        else
        {
            if (TopRight.collider.tag == "Ground" && Right.collider.tag != "Ground")
            {

                transform.position += Vector3.left * 10 * JumpSupportAmount;
            }
            if (TopLeft.collider.tag == "Ground" && Left.collider.tag != "Ground")
            {
                Debug.Log("en sol deðdi");
                transform.position += Vector3.right * 10 * JumpSupportAmount;
            }
        }
    }
    #endregion

    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.yellow;
        Gizmos.DrawWireSphere(movementManager.checkPos.position, 0.3f);

        Debug.DrawRay(TopLeftRay, Vector2.up * DistanceOfRays, Color.yellow);
        Debug.DrawRay(TopRightRay, Vector2.up * DistanceOfRays, Color.yellow);
        Debug.DrawRay(RightRay, Vector2.up * DistanceOfRays, Color.green);
        Debug.DrawRay(LeftRay, Vector2.up * DistanceOfRays, Color.green);
    }
}

