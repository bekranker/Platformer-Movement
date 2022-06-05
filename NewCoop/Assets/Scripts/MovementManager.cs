using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : Librariy
{
    [Header("-----Move Options-----")]
    [Range(1f,100f)][SerializeField] float MainSpeed;
    [Range(1f, 50f)] [SerializeField] float acceleration;
    [Range(1f, 50f)] [SerializeField] float decceleration;
    [Range(0f, 1f)] [SerializeField] float velPower;

    [Header("-----Jump Options-----")]
    [Range(1f, 10f)] [SerializeField] float fallingGravityScale;
    [Range(1f, 10f)] [SerializeField] float gravityScale;
    [Range(1f, 100f)] [SerializeField] float jumpAmount;
    [Range(1f, 100f)] [SerializeField] float coyotoJumpAmount;
    [Range(1f, 100f)] [SerializeField] float doubleJumpAmount;
    [Range(0f, 1f)] [SerializeField] float CoyotoTime = 0.1f;
    [Range(1f, 100f)] [SerializeField] float jumpCutMultiplier;
    [Range(0f, 1f)] [SerializeField] float jumpTime;
    [SerializeField] Vector2 checkPosSize;
    [SerializeField] Transform checkPos;
    [SerializeField] LayerMask layerMask;
    [SerializeField] int totalJump;

    float jumpTimeCounter;
    
    bool isGrounded = true;
    bool multipleJump;
    bool coyoteJump;
    bool isJumped;


    int jumpCunter;
    Collider2D[] IsGrounded;


    [Header("----Componenets-----")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private MovementBehaviour movementBehaviour;

    private void Start()
    {
        jumpTimeCounter = jumpTime;
    }

    private void Update()
    {
        bool wasGrounded = isGrounded;
        if (IsGrounded.Length > 0)
        {
            isGrounded = true;
            isJumped = true;
            jumpTimeCounter = jumpTime;

            if (!wasGrounded)
            {
                jumpCunter = totalJump;
                multipleJump = false;
                Debug.Log("Landed");
            }
        }
        else
        {
            if (wasGrounded)
            {
                StartCoroutine(CoyotoJumpCoroutine());
            }
        }
        isGrounded = (IsGrounded.Length > 0) ? true : false;
        if (movementBehaviour.JumpDown == 1)
        {
            Jump();
            isJumped = true;
        }
        if (isJumped && movementBehaviour.Jump == 1)
        {
            if (jumpTimeCounter > 0)
            {
                JumpCut();
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumped = false;
            }
        }
        if (movementBehaviour.Jump == 0)
        {
            isJumped = false;
        }
        
    }

    IEnumerator CoyotoJumpCoroutine()
    {
        coyoteJump = true;
        yield return new WaitForSeconds(CoyotoTime);
        coyoteJump = false;
    }

    void FixedUpdate()
    {
        #region Run
        //Direction
        float moveInput = movementBehaviour.x;
        //Calculating direction we want to move in and our desired velocity
        float targetSpeed = moveInput * MainSpeed;
        //Calculeting difference between current velocity and desired velocity
        float speedDif = targetSpeed - rb.velocity.x;
        //This part choosing is need acceleration or deccelaration
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        //Calculeting movement value
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
        //Moving
        rb.velocity = new Vector2(moveInput * MainSpeed, rb.velocity.y);
        //Moving with forces
        rb.AddForce(moveInput * Vector2.right);
        #endregion

        #region Jump
        IsGrounded = Physics2D.OverlapBoxAll(checkPos.position, checkPosSize, 0, layerMask);
        OnJump();
        #endregion
    }

    #region Jump Functions

    private void Jump()
    {
        if (isGrounded)
        {
            jumpCunter--;
            AddJumpForce();
            multipleJump = true;
        }
        else
        {
            if (coyoteJump)
            {
                multipleJump = true;
                AddCoyotoJumpForce();
            }
            else if (multipleJump && jumpCunter > 0)
            {
                jumpCunter--;
                AddDoubleJumpForce();
            }
            else
            {
                isJumped = false;
            }
        }
    }

    #region Adding Jump
    private void AddJumpForce()
    {
        rb.velocity = Vector2.zero;
        rb.velocity += Vector2.up * 100 * jumpAmount * Time.fixedDeltaTime;
    }
    #endregion

    #region Adding DoubleJump
    private void AddDoubleJumpForce()
    {
        rb.velocity = Vector2.zero;
        rb.velocity += Vector2.up * 100 * doubleJumpAmount * Time.fixedDeltaTime;
    }
    #endregion

    #region Adding CoyotoJump
    private void AddCoyotoJumpForce()
    {
        Debug.Log("Coyote jump is did");
        rb.velocity += Vector2.up * 100 * coyotoJumpAmount * Time.fixedDeltaTime;
    }
    #endregion

    #region On Jumping
    private void OnJump()
    {
        if (rb.velocity.y > 3)
        {
            rb.gravityScale = gravityScale * fallingGravityScale;
        }
        if(rb.velocity.y == 0)
        {
            rb.gravityScale = gravityScale;
        }
        //if (rb.velocity.y < 0)
        //{
        //    rb.gravityScale = gravityScale * fallingGravityScale;
        //}
    }
    #endregion


    #region JumpCut
    private void JumpCut()
    {
        rb.velocity += Vector2.up * jumpCutMultiplier * jumpAmount * Time.deltaTime;
        if (isGrounded)
        {
            jumpTimeCounter = jumpCunter;
        }
    }
    #endregion

    #endregion
}
