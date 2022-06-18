using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    [Header("-----Particles------")]
    [SerializeField] GameObject jumpParticle;
    [SerializeField] GameObject _doubleParticule;

    [Header("------Jump Particle Settings------")]
    [Range(1, 100)] [SerializeField] float velocityAmount;

    [Header("-----Managers-----")]
    [SerializeField] MovementBehaviour movementBehaviour;
    [SerializeField] MovementManager movementManager;

    [Header("-----Other------")]
    [SerializeField] Transform groundCheckPos;

    Transform player;
    GameObject a;
    GameObject doubleJumpParticleObject;

    void Update()
    {
        player = gameObject.transform;
        if (movementBehaviour.JumpDown == 1 && (movementManager.isGrounded || player.GetComponent<Rigidbody2D>().velocity.y == 0))
        {
            a = Instantiate(jumpParticle, groundCheckPos.position, Quaternion.identity);
        }
        
        if (movementBehaviour.JumpDown == 1)
        {
            if (player.GetComponent<Rigidbody2D>().velocity.y != 0 && !movementManager.isGrounded)
            {
                doubleJumpParticleObject = Instantiate(_doubleParticule, groundCheckPos.position, Quaternion.identity);
            }
        }
        JumpParticle();

    }

    void JumpParticle()
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
            forceOverLifetime.xMultiplier -= Time.deltaTime;
            forceOverLifetime.yMultiplier -= Time.deltaTime;
        }
    }

    void DoubleJumpParticle()
    {
        if (doubleJumpParticleObject == null) return;

        ParticleSystem ps = doubleJumpParticleObject.GetComponent<ParticleSystem>();

        var forceOverLifetime = ps.forceOverLifetime;
        forceOverLifetime.enabled = true;

        float distance = Vector2.Distance(a.transform.position, player.transform.position);
        if (distance < 3)
        {
            forceOverLifetime.xMultiplier = movementBehaviour.x * velocityAmount;
            forceOverLifetime.yMultiplier = movementBehaviour.y * velocityAmount;
        }
        else
        {
            forceOverLifetime.xMultiplier -= Time.deltaTime;
            forceOverLifetime.yMultiplier -= Time.deltaTime;
        }
    }
}
