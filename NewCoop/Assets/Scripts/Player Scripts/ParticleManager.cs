using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [Space(25)]
    [Header("-----Particles------")]
    [Space(15)]
    [BackgroundColor(1f, 1f, 0f, 1f)] [SerializeField] GameObject jumpParticle;
    [BackgroundColor(1f, 1f, 0f, 1f)] [SerializeField] GameObject _doubleParticule;

    [Space(25)]
    [Header("------Jump Particle Settings------")]
    [Space(15)]
    [BackgroundColor(0f, 1f, 1f, 1f)] [Range(1, 100)] [SerializeField] float velocityAmount;

    [Space(25)]
    [Header("-----Managers-----")]
    [Space(15)]
    [BackgroundColor(0f, 1f, 1f, 1f)] [SerializeField] MovementBehaviour movementBehaviour;
    [BackgroundColor(0f, 1f, 1f, 1f)] [SerializeField] MovementManager movementManager;

    [Space(25)]
    [Header("-----Other------")]
    [Space(15)]
    [BackgroundColor(1f, 1f, 0f, 1f)] [SerializeField] Transform groundCheckPos;

    Transform player;
    GameObject a;

    void Update()
    {
        player = gameObject.transform;
        if (movementBehaviour.JumpDown == 1 && (movementManager.isGrounded || player.GetComponent<Rigidbody2D>().velocity.y == 0))
        {
            a = Instantiate(jumpParticle, groundCheckPos.position, Quaternion.identity);
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
}
