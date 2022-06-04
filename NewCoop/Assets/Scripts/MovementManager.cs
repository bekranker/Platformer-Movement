using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [Header("-----Options-----")]
    [SerializeField] float MainSpeed, acceleration, decceleration, velPower;


    [Header("----Componenets-----")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private MovementBehaviour movementBehaviour;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movementBehaviour = GetComponent<MovementBehaviour>();
    }

    void FixedUpdate()
    {
        

        // which direction
        float moveInput = movementBehaviour.x;

        //calculatþng direction we want to move in and our desired velocity
        float targetSpeed = moveInput * MainSpeed;
        //calculeting difference between current velocity and desired velocity
        float speedDif = targetSpeed - rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
        rb.velocity = new Vector2(moveInput * MainSpeed, rb.velocity.y);
        rb.AddForce(moveInput * Vector2.right);
    }
}
