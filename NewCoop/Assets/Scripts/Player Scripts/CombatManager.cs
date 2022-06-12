using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Header("-----Statues-----")]
    public bool HasAxe;

    
    [Header("----Components-----")]
    [SerializeField] MovementBehaviour Input;
    [SerializeField] MovementManager movementManager;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 direction = collision.GetContact(0).normal;
        if (collision.gameObject.tag == "Player")
        {
            if (direction.y == -1)
            {
                Debug.Log($"{collision.gameObject.name} is dead !!");
            }
        }
    }
}
