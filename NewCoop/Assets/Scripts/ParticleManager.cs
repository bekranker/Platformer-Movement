using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    [Header("-----Particles------")]
    [SerializeField] GameObject jumpParticle;
    [SerializeField] GameObject _walkParticule;
    [SerializeField] GameObject _doubleParticule;
    
    [Header("------Jump Particle Settings------")]
    [Range(1,100)][SerializeField] float velocityAmount;

    //[Header("------Jump Particle Settings------")]


    [Header("-----Managers-----")]
    [SerializeField] MovementBehaviour movementBehaviour;
    [SerializeField] MovementManager movementManager;

    [Header("-----Other------")]
    [SerializeField] Transform groundCheckPos;

    Transform player;
    GameObject a;
    GameObject doubleJumpParticleObject;
    private void Start()
    {
        
    }

    void Update()
    {
        player = gameObject.transform;
        if (movementBehaviour.JumpDown == 1 && movementManager.isGrounded)
        {
            a = Instantiate(jumpParticle, groundCheckPos.position, Quaternion.identity);
        }
        JumpParticle();
        walkParticle();

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
    void walkParticle()
    {
        ParticleSystem ps = _walkParticule.GetComponent<ParticleSystem>();

        if (movementManager.isGrounded)
        {
            _walkParticule.SetActive(true);
        }
        else
        {
            _walkParticule.SetActive(false);
        }
    }

    void DoubleJumpParticle()
    {
        if (_doubleParticule == null) return;

        ParticleSystem ps = _doubleParticule.GetComponent<ParticleSystem>();

        if (!movementManager.isGrounded && movementBehaviour.JumpDown == 1)
        {
            doubleJumpParticleObject = Instantiate(_doubleParticule, groundCheckPos.position, Quaternion.identity);
        }

        var forceOverLifetime = ps.forceOverLifetime;
        forceOverLifetime.enabled = true;

        float distance = Vector2.Distance(doubleJumpParticleObject.transform.position, player.transform.position);
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
