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
        if (collision.gameObject.tag == "Head")
        {
            Vector2 direction = collision.GetContact(0).normal;
            if (direction.y == 1)
            {
                Debug.Log("Düþman öldü");
            }
        }
    }
}
