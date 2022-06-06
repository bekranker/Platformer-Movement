using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerTools : MonoBehaviour
{
    [Header("-----Dash-----")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float DashSpeed;
    [SerializeField] float DashMultipler;
    [SerializeField] float DashTime;
    [SerializeField] float StartDashTime;
    
    private int direction;
    float a;
    [Header("-----Managers-----")]
    [SerializeField] MovementBehaviour movementBehaviour;
    [SerializeField] MovementManager movementManager;

    private void Start()
    {
        DashTime = StartDashTime;
        a = movementManager.gravityScale;
    }

    private void Update()
    {
        if (direction == 0)
        {
            
            if (movementBehaviour.x == -1)
            {
                direction = 1;
            }
            else if (movementBehaviour.x == 1)
            {
                direction = 2;
            }

        }
        else
        {
            if (DashTime <= 0)
            {
                direction = 0;
                DashTime = StartDashTime;
                movementManager.isDashing = false;
                a = rb.gravityScale;
                rb.gravityScale = movementManager.gravityScale;
            }
            else
            {
                DashTime -= Time.deltaTime;
                if (movementBehaviour.CancelDown == 1 && movementManager.dashCount < 1)
                {
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
                        default:
                            break;
                    }
                }
            }
            
        }
    }
}





