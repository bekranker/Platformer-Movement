using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [Header("-----Move Options-----")]
    [SerializeField] float MainSpeed, acceleration, decceleration, velPower;

    [Header("-----Jump Options")]
    [SerializeField] float jumpAmount, lastroundedTime, lastJumpTime, CoyotoTime;
    [SerializeField] Vector2 checkPosSize;
    [SerializeField] Transform checkPos;
    [SerializeField] LayerMask layerMask;

    [Header("----Componenets-----")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private MovementBehaviour movementBehaviour;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movementBehaviour = GetComponent<MovementBehaviour>();
    }

    private void Update()
    {
        #region Timer
        if (lastroundedTime > 0 && lastJumpTime > 0)
        {
            lastJumpTime -= Time.deltaTime;
            lastroundedTime -= Time.deltaTime;
        }
        #endregion
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
        bool IsGrouned = Physics2D.OverlapBox(checkPos.position, checkPosSize, 0, layerMask);

        if (IsGrouned)
        {
            if (movementBehaviour.Jump == 1)
            {
                Jump();
            }
        }
        #endregion
    }


    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
    }
}
