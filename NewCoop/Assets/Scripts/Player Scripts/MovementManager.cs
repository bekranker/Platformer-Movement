using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : Librariy
{

    [Header("-----Physical Settings-----")]
    [Range(-50f, 50f)] [SerializeField] float maxFallGravity;
    [Range(-50f, 50f)] [SerializeField] float minFallGravity;


    [Header("-----Move Options-----")]
    [Range(1f,100f)] public float MainSpeed;
    [Range(1f, 50f)] public float acceleration;
    [Range(1f, 50f)] public float decceleration;
    [Range(0.005f, 1f)] public float velPower;

    [Header("-----Jump Options-----")]
    [Range(1f, 10f)] public float fallingGravityScale;
    [Range(1f, 10f)] public float gravityScale;
    [Range(1f, 100f)] public float jumpAmount;
    [Range(1f, 100f)] public float doubleJumpAmount;
    [Range(0.005f, 1f)] public float CoyotoTime;
    [Range(0.005f, 1f)] public float BufferTime;
    [Range(0.005f, 100f)] public float BufferAmount;
    [Range(0.005f, 1f)] public float jumpCutMultiplier;
    [Range(0.005f, 1f)] public float jumpTime;
    [Range(0.005f, 100f)] public float DashDistance;
    [Range(0.005f, 100f)] public float ClampGravityMin;
    [Range(0.005f, 100f)] public float ClampGravityMax;

    [HideInInspector] public float jumpTimeCounter;
    [HideInInspector] public Vector2 checkPosSize;
    [HideInInspector] public bool isGrounded = true;
    [HideInInspector] public bool coyoteJump;
    [HideInInspector] public float coyoteTimeCounter;
    [HideInInspector] public bool isJumped;
    [HideInInspector] public bool buffering = false;
    [HideInInspector] public bool IsCanDash;
    [HideInInspector] public bool isDashing;

    public int dashCount;
    public Transform checkPos;
    public LayerMask layerMask;
    public int totalJump;
    public int jumpCounter;

    [Header("----Componenets-----")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D boxC2D;
    [SerializeField] private MovementBehaviour Input;


    void FixedUpdate()
    {
        #region Run
        if (!isDashing)
        {
            Runing();
        }
        #endregion

        #region Physical Settings
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, minFallGravity, maxFallGravity));
        #endregion
    }

    private void Runing()
    {
        //Direction
        float moveInput = Input.x;
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
    }


    #region Adding Normal Jump Force
    public void AddJumpForce()
    {
        rb.velocity = Vector2.zero;
        _AddVelocity(rb, jumpAmount);
    }
    #endregion

    #region Add Jump Buffer Force
    public void AddBufferJumpForce()
    {
        rb.velocity = Vector2.zero;
        _AddVelocity(rb, BufferAmount);
    }
    #endregion

    #region On Jumping
    public void OnJump()
    {
        if (rb.velocity.y > 3)
        {
            rb.gravityScale = gravityScale * fallingGravityScale;
        }
        if (rb.velocity.y == 0)
        {
            rb.gravityScale = gravityScale;
        }
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = gravityScale * fallingGravityScale;
        }
    }
    #endregion    
    
    #region Jump Cut
    public void JumpCut()
    {
        rb.velocity += Vector2.up * jumpAmount / jumpCutMultiplier * Time.deltaTime;
    }

    #endregion
}
