using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : Librariy
{

    [Header("-----Physical Settings-----")]

    //R-G-B-A

    [BackgroundColor(1f, 0f, 0f, 1f)] [Range(-50f, 50f)] [SerializeField] float maxFallGravity;
    [BackgroundColor(1f, 0f, 0f, 1f)] [Range(-50f, 50f)] [SerializeField] float minFallGravity;
    [BackgroundColor(1f, 0f, 0f, 1f)] [Range(1f, 100f)] public float AxeMainSpeed;
    [BackgroundColor(1f, 0f, 0f, 1f)] [Range(1f, 50f)] public float AxeAcceleration;
    [BackgroundColor(1f, 0f, 0f, 1f)] [Range(1f, 50f)] public float AxeDecceleration;
    [BackgroundColor(1f, 0f, 0f, 1f)] [Range(0.005f, 1f)] public float AxeVelPower;
    [BackgroundColor(1f, 0f, 0f, 1f)] [Range(0.005f, 100f)] public float ClampGravityMin;
    [BackgroundColor(1f, 0f, 0f, 1f)] [Range(0.005f, 100f)] public float ClampGravityMax;

    [Header("-----Move Options-----")]
    [BackgroundColor(0f, 1f, 0f, 1f)] [Range(1f,100f)] public float MainSpeed;
    [BackgroundColor(0f, 1f, 0f, 1f)] [Range(1f, 50f)] public float acceleration;
    [BackgroundColor(0f, 1f, 0f, 1f)] [Range(1f, 50f)] public float decceleration;
    [BackgroundColor(0f, 1f, 0f, 1f)] [Range(0.005f, 1f)] public float velPower;
    [BackgroundColor(0f, 1f, 0f, 1f)] [Range(1f, 10f)] public float fallingGravityScale;
    [BackgroundColor(0f, 1f, 0f, 1f)] [Range(1f, 10f)] public float gravityScale;

    [Header("-----Jump Settings-----")]
    [BackgroundColor(0f, 0f, 1f, 1f)] [Range(1f, 100f)] public float jumpAmount;
    [BackgroundColor(0f, 0f, 1f, 1f)] [Range(1f, 10f)] public float JumpCutTimer;
    [BackgroundColor(0f, 0f, 1f, 1f)] [Range(0.005f, 1f)] public float jumpCutMultiplier;
    [BackgroundColor(0f, 0f, 1f, 1f)] [Range(0.005f, 1f)] public float jumpTime;
    [BackgroundColor(0f, 0f, 1f, 1f)] [HideInInspector] public float jumpTimeCounter;
    [BackgroundColor(0f, 0f, 1f, 1f)] [HideInInspector] public Vector2 checkPosSize;
    [BackgroundColor(0f, 0f, 1f, 1f)] [HideInInspector] public bool isGrounded = true;
    [BackgroundColor(0f, 0f, 1f, 1f)] [HideInInspector] public bool isJumped;
    public int totalJump;
    public int jumpCounter;
    public Transform checkPos;
    public LayerMask layerMask;


    [Header("-----Coyote Time Settings-----")]
    [BackgroundColor(0f, 1f, 1f, 1f)]
    [Range(0.005f, 1f)] public float CoyotoTime;
    [Range(1f, 100f)] public float CoyoteAmount;
    [HideInInspector] public bool coyoteJump;
    [HideInInspector] public float coyoteTimeCounter;

    [Header("-----Buffering Settings-----")]
    [Range(0.005f, 1f)] public float BufferTime;
    [Range(0.005f, 100f)] public float BufferAmount;
    [HideInInspector] public bool buffering = false;

    [Header("-----Dash Settings-----")]
    [Range(0.005f, 100f)] public float DashDistance;
    [HideInInspector] public bool IsCanDash;
    [HideInInspector] public bool isDashing;
    [HideInInspector] public float JumpCutTimeCounter;
    public int dashCount;

    [Header("-----Double Jump Settings-----")]
    [Range(1f, 100f)] public float doubleJumpAmount;

    [BackgroundColor(1f, 0f, 1f, 1f)]
    [Header("----Componenets-----")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D boxC2D;
    [SerializeField] private MovementBehaviour Input;
    [SerializeField] private CombatManager combatManager;
    private void Start()
    {
        JumpCutTimeCounter = JumpCutTimer;
    }

    private void Update()
    {
        if (Input.JumpDown == 1) CalculatingStatues();
    }

    void FixedUpdate()
    {
        #region Run
        if (!isDashing) Runing();
        #endregion

        #region Physical Settings
        if (!isDashing) rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, minFallGravity, maxFallGravity));
        #endregion
    }

    #region Run Settings
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
    #endregion

    #region Axe Settings
    private void CalculatingStatues()
    {
        float _MainSpeed = MainSpeed;
        float _acceleration = acceleration;
        float _decceleration = decceleration;
        float _velPower = velPower;
        if (combatManager.HasAxe)
        { 
            MainSpeed = AxeMainSpeed;
            acceleration = AxeAcceleration;
            decceleration = AxeDecceleration;
            velPower = AxeVelPower;
            totalJump = 1;
        }
        else if (!combatManager.HasAxe)
        {
            MainSpeed = _MainSpeed;
            acceleration = _acceleration;
            decceleration = _decceleration;
            velPower = _velPower;
            totalJump = 2;
        }
    }
    #endregion

    #region Adding Normal Jump Force
    public void AddJumpForce()
    {
        rb.velocity = Vector2.zero;
        _AddVelocity(rb, jumpAmount);
    }
    #endregion

    #region Adding Double Jump Force
    public void AddDoubleJumpForce()
    {
        rb.velocity = Vector2.zero;
        _AddVelocity(rb, doubleJumpAmount);
    }
    #endregion

    #region Adding Coyote Jump Force
    public void AddCoyoteTimeForce()
    {
        rb.velocity = Vector2.zero;
        _AddVelocity(rb, CoyoteAmount);
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
        //rb.velocity += Vector2.up * jumpCutAmount * Time.fixedDeltaTime;
        if (rb.velocity.y > 0)
        {
            rb.AddForce(Vector2.down * rb.velocity.y * (1 - JumpCutTimeCounter), ForceMode2D.Impulse);
        }
    }
    #endregion
}
