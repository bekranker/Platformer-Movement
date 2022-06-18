using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeOwnManager : MonoBehaviour
{
    [Space(10)]
    [Header("-----Axe Direction Settings-----")]
    [Space(20)]
    [BackgroundColor(1,0,0,1)]
    [Range(1, 300)] [SerializeField] float directionForce;
    [Range(1, 100)] [SerializeField] float directionForceCuter;
    [Range(1, 300)] [SerializeField] float AxeForce;
    [Range(1, 100)] [SerializeField] float AxeForceCuter;
    [Range(0.005f, 10)] [SerializeField] float AxeGravityCounter;

    [Space(10)]
    [Header("-----Axe Settings-----")]
    [Space(20)]
    [BackgroundColor(0, 1, 0, 1)]
    [SerializeField] LayerMask _mask;
    [SerializeField] Rigidbody2D rbAxe;
    private bool _IsTouchingToGround;
    private bool _IsTouchingToHead;
    private bool _IsCanKill;
    private float AxeForceCounter;


    [Space(10)]
    [Header("-----Components-----")]
    [Space(20)]
    [BackgroundColor(0,0,1,1)]
    [SerializeField] MovementBehaviour Inputs;

    private void Start()
    {
        AxeForceCounter = AxeForce - 50;
    }
    #region Add Force To Angle and Rotate
    private void AddForceToAxe()
    {
        if (_IsTouchingToGround) return;
        else
        {
            if (Inputs.RL == 1)
            {
                transform.Rotate(new Vector3(0, 0, 1 * directionForce * Time.deltaTime));
                Debug.Log("RL yönünde force var");
            }
            if (Inputs.RR == 1)
            {
                transform.Rotate(new Vector3(0, 0, -1 * directionForce * Time.deltaTime));
                Debug.Log("RR yönünde force var");
            }
        }
    }
    #endregion

    #region Collision Settings
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _IsTouchingToGround = true;
            rbAxe.gravityScale = 0;
            rbAxe.velocity = Vector2.zero;
        }
        if (_IsCanKill && collision.gameObject.tag == "head")
        {
            Debug.Log("Player is dead !!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _IsTouchingToGround = false;
        }
        if (collision.gameObject.tag == "head")
        {
            _IsCanKill = true;
        }
    }
    #endregion

    #region Draw Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, transform.localScale.x / 2);
    }
    #endregion

    void Update()
    {
        if (!_IsTouchingToGround)
        {
            rbAxe.velocity = new  Vector2(Mathf.Clamp(rbAxe.velocity.x, -20, 20), Mathf.Clamp(rbAxe.velocity.y, -20, 20));
            rbAxe.velocity += new Vector2(transform.up.x, transform.up.y) * Time.fixedDeltaTime * AxeForce;
            AxeForce -= Time.deltaTime * AxeForceCuter;
            if (AxeForce > AxeForceCounter) AxeForce -= Time.deltaTime * AxeForceCuter;
            this.Wait(AxeGravityCounter, () => rbAxe.gravityScale = 0.7f);
            directionForce -= Time.deltaTime * directionForceCuter;
        }
        else
        {
            rbAxe.velocity = Vector2.zero;
            rbAxe.gravityScale = 0;
        }
        AddForceToAxe();
    }
}
