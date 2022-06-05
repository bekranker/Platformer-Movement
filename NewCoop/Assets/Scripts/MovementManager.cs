using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : Librariy
{
    [Header("-----Move Options-----")]
    [SerializeField] float MainSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float decceleration;
    [SerializeField] float velPower;

    [Header("-----Jump Options-----")]
    [SerializeField] float fallingGravityScale;
    [SerializeField] float gravityScale;
    [SerializeField] float jumpAmount;
    [SerializeField] float coyotoJumpAmount;
    [SerializeField] float doubleJumpAmount;
    [SerializeField] float CoyotoTime = 0.1f;
    [SerializeField] Vector2 checkPosSize;
    [SerializeField] Transform checkPos;
    [SerializeField] LayerMask layerMask;
    public float CoyotoTimeCounter;
    private bool IsGrouned;
    private bool IsJumped;
    public int jumpCounter;

    [Header("----Componenets-----")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private MovementBehaviour movementBehaviour;

    private void Update()
    {
        jumpCounter = (IsGrouned) ? 2 : jumpCounter;
        if (jumpCounter == 0 && IsGrouned)
        {
            CoyotoTimeCounter = 0;
        }
        else if (!IsGrouned && movementBehaviour.Jump == 1 && jumpCounter > 0)
        {
            CoyotoTimeCounter = CoyotoTime;
        }
        else
        {
            CoyotoTimeCounter -= Time.deltaTime;
        }
        

        if (movementBehaviour.Jump == 1)
        {
            if (jumpCounter == 2 && IsGrouned && rb.velocity.y == 0) { AddJumpForce();}
            jumpCounter--;
            if (CoyotoTimeCounter !> 0 && !IsGrouned)
            {
                if (jumpCounter == 1 && (rb.velocity.y != 0)) { AddDoubleJumpForce(); }
            }
            if (CoyotoTimeCounter > 0 && !IsGrouned && jumpCounter != 1) AddCoyotoJumpForce();
        }
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
        IsGrouned = Physics2D.OverlapBox(checkPos.position, checkPosSize, 1, layerMask);
        OnJump();
        #endregion
    }

    #region Jump Functions

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
        rb.velocity = Vector2.zero;
        rb.velocity += Vector2.up * 100 * coyotoJumpAmount * Time.fixedDeltaTime;
    }
    #endregion

    #region On Jumping
    private void OnJump()
    {
        if (rb.velocity.y > 5)
        {
            rb.gravityScale = gravityScale * fallingGravityScale;
        }
        if(rb.velocity.y == 0)
        {
            rb.gravityScale = gravityScale;
        }
    }
    #endregion

    #endregion
}
