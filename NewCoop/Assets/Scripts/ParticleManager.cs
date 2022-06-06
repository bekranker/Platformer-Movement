using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] GameObject jumpParticle;
    [SerializeField] MovementBehaviour movementBehaviour;
    [SerializeField] MovementManager movementManager;
    [SerializeField] Transform groundCheckPos;
    [Range(1,100)][SerializeField] float velocityAmount;
    Transform player;
    GameObject a;

    private void Start()
    {
        player = gameObject.transform;
    }

    void Update()
    {
        if (movementBehaviour.JumpDown == 1 && movementManager.isGrounded)
        {
            a = Instantiate(jumpParticle, groundCheckPos.position, Quaternion.identity);
        }
        particle();
    }

    void particle()
    {
        if (a == null) return;

        ParticleSystem ps = a.GetComponent<ParticleSystem>();

        var forceOverLifetime = ps.forceOverLifetime;
        forceOverLifetime.enabled = true;

        float distance = Vector2.Distance(a.transform.position, player.transform.position);
        if (distance < 5)
        {
            forceOverLifetime.xMultiplier = movementBehaviour.x * velocityAmount;
            forceOverLifetime.yMultiplier = movementBehaviour.y * velocityAmount;
        }
        else
        {
            forceOverLifetime.xMultiplier = 0;
            forceOverLifetime.yMultiplier =0 ;
        }
        
    }
}
