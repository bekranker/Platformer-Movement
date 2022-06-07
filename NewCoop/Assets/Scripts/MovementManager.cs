using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [Header("-----Move Options-----")]
    [Range(1f,100f)] public float MainSpeed;
    [Range(1f, 50f)] public float acceleration;
    [Range(1f, 50f)] public float decceleration;
    [Range(0.005f, 1f)] public float velPower;

    [Header("-----Jump Options-----")]
    [Range(1f, 10f)] public float fallingGravityScale;
    [Range(1f, 10f)] public float gravityScale;
    [Range(1f, 100f)] public float jumpAmount;
    [Range(1f, 100f)] public float coyotoJumpAmount;
    [Range(1f, 100f)] public float doubleJumpAmount;
    [Range(0.005f, 1f)] public float CoyotoTime;
    [Range(0.005f, 1f)] public float BufferTime;
    [Range(0.005f, 100f)] public float BufferAmount;
    [Range(1f, 100f)] public float jumpCutMultiplier;
    [Range(0.005f, 1f)] public float jumpTime;
    [Range(0.005f, 100f)] public float DashDistance;
    public Vector2 checkPosSize;
    public Transform checkPos;
    public LayerMask layerMask;
    public int totalJump;

    public float jumpTimeCounter;
    public float bufferTimeCounter;

    public bool isGrounded = true;
    public bool multipleJump;
    public bool coyoteJump;
    public bool isJumped;
    public bool buffering = false;
    public bool IsCanDash;
    public bool isDashing;

    public int jumpCounter;
    public int dashCount;
    public int coyotoJumpCounter = 0;

    Collider2D[] IsGrounded;


    [Header("----Componenets-----")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D boxC2D;
    [SerializeField] private MovementBehaviour movementBehaviour;

    //private void Start()
    //{
    //    jumpTimeCounter = jumpTime;
    //}

    //private void Update()
    //{
    //    #region Ground Check and calculeting bools
    //    bool wasGrounded = isGrounded;
    //    if (IsGrounded.Length > 0)
    //    {
    //        isGrounded = true;
    //        isJumped = true;
    //        buffering = false;
    //        jumpTimeCounter = jumpTime;
    //        coyotoJumpCounter = 0;

    //        if (!wasGrounded)
    //        {
    //            jumpCounter = totalJump;
    //            multipleJump = false;
    //            Debug.Log("Landed");
    //        }
    //    }

    //    else
    //    {
    //        if (wasGrounded)
    //        {
    //            StartCoroutine(CoyotoJumpCoroutine());
    //        }
    //    }

    //    isGrounded = (IsGrounded.Length > 0) ? true : false;
    //    #endregion

    //    if (!isDashing)
    //    {
    //        #region Jumping With JumpCut

    //        if (movementBehaviour.Jump == 0)
    //        {
    //            isJumped = false;
    //        }
    //        #endregion

    //        #region Buffer And Other Jump
    //        if (movementBehaviour.JumpDown == 1)
    //        {
    //            Jump();
    //            isJumped = true;
    //            if (jumpCounter <= 0) //burda
    //            {
    //                Debug.Log("jumpCounter þuan da:" + jumpCounter);
    //                buffering = true;
    //            }

    //        }
    //        #region buffer calculeting
    //        if (buffering)
    //        {
    //            bufferTimeCounter = BufferTime;
    //        }
    //        else
    //        {
    //            bufferTimeCounter -= Time.deltaTime;
    //        }
    //        #endregion

    //        #endregion

    //        #region Doing Buffer Jump
    //        if (bufferTimeCounter > 0)
    //        {
    //            if (isGrounded)
    //            {
    //                multipleJump = true;
    //                coyotoJumpCounter++;
    //                jumpCounter--;
    //                AddBufferJumpForce();
    //                buffering = false;
    //            }
    //        }
    //        #endregion
    //    }

    //    this.Wait(BufferTime, () => buffering = false);
    //}

    void FixedUpdate()
    {
        #region Run
        if (!isDashing)
        {
            Runing();
        }
        #endregion
    }

    private void Runing()
    {
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
    }

    //#region Jump Functions
    //private void Jump()
    //{
    //    #region Normal Jump
    //    if (isGrounded)
    //    {
    //        jumpCounter--;
    //        AddJumpForce();
    //        multipleJump = true;
    //    }
    //    #endregion

    //    #region Not Normal
    //    else
    //    {
    //        #region Coyoto Jump
    //        if (coyoteJump)
    //        {
    //            jumpCounter--;
    //            AddCoyotoJumpForce();
    //        }
    //        #endregion

    //        #region Muliple Jumping
    //        if (multipleJump && jumpCounter > 0)
    //        {
    //            Debug.Log("double jump");
    //            jumpCounter--;
    //            AddDoubleJumpForce();
    //        }
    //        #endregion

    //        else
    //        {
    //            isJumped = false;
    //        }
    //    }
    //    #endregion
    //}

    #region Adding Normal Jump Force
    public void AddJumpForce()
    {
        rb.velocity = Vector2.zero;
        rb.velocity += Vector2.up * 100 * jumpAmount * Time.fixedDeltaTime;
    }
    #endregion

    #region Adding Double Jump Force
    public void AddDoubleJumpForce()
    {
        rb.velocity = Vector2.zero;
        rb.velocity += Vector2.up * 100 * doubleJumpAmount * Time.fixedDeltaTime;
    }
    #endregion

    #region Adding Coyoto Jump Force
    public void AddCoyotoJumpForce()
    {
        Debug.Log("Coyote jump is did");

        if (coyotoJumpCounter == 0)
        {
            rb.velocity = Vector2.zero;
            rb.velocity += Vector2.up * 100 * coyotoJumpAmount * Time.fixedDeltaTime;
            coyotoJumpCounter++;
        }
    }
    #endregion

    #region Add Jump Buffer Force
    public void AddBufferJumpForce()
    {
        Debug.Log("Buffer jump is did");
        rb.velocity = Vector2.zero;
        rb.velocity += Vector2.up * 100 * BufferAmount * Time.fixedDeltaTime;
    }
    #endregion

    //#region On Jumping
    //private void OnJump()
    //{
    //    if (rb.velocity.y > 3)
    //    {
    //        rb.gravityScale = gravityScale * fallingGravityScale;
    //    }
    //    if(rb.velocity.y == 0)
    //    {
    //        rb.gravityScale = gravityScale;
    //    }
    //    if (rb.velocity.y < 0)
    //    {
    //        rb.gravityScale = gravityScale * fallingGravityScale;
    //    }
    //}
    //#endregion



    //#endregion


}
