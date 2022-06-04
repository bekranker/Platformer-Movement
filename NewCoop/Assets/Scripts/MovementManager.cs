using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
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
    [SerializeField] float CoyotoTime = 0.2f;
    [SerializeField] float CoyotoTimeCounter;
    [SerializeField] float jumpCutMultiplayer;
    [SerializeField] Vector2 checkPosSize;
    [SerializeField] Transform checkPos;
    [SerializeField] LayerMask layerMask;
    private bool IsGrouned;


    [Header("----Componenets-----")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private MovementBehaviour movementBehaviour;

    private void Update()
    {
        if (IsGrouned)
        {
            CoyotoTimeCounter = CoyotoTime;
        }
        else
        {
            CoyotoTimeCounter -= Time.deltaTime;
        }
        if (movementBehaviour.Jump == 1)
        {
            if (CoyotoTimeCounter > 0)
            {
                Jump();
            }
        }
    }

    void FixedUpdate()
    {
        
        #region Run
        // which direction
        float moveInput = movementBehaviour.x;
        //calculatþng direction we want to move in and our desired velocity
        float targetSpeed = moveInput * MainSpeed;
        //calculeting difference between current velocity and desired velocity
        float speedDif = targetSpeed - rb.velocity.x;
        //this part choosing is need acceleration or deccelaration
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        //calculeting movement value
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
        //moving
        rb.velocity = new Vector2(moveInput * MainSpeed, rb.velocity.y);
        //moving with forces
        rb.AddForce(moveInput * Vector2.right);
        #endregion

        #region Jump
        IsGrouned = Physics2D.OverlapBox(checkPos.position, checkPosSize, 0, layerMask);
        
        #endregion

        OnJump();
    }


    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
        
    }

    private void OnJump()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = gravityScale * fallingGravityScale;
        }
        if(rb.velocity.y == 0)
        {
            rb.gravityScale = gravityScale;
        }
    }
}
