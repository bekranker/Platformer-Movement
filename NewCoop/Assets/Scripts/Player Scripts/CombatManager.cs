using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{

    [Header("----Components-----")]
    [SerializeField] MovementBehaviour Input;
    [SerializeField] MovementManager movementManager;


    private bool IsPressedOnes;

    private void Update()
    {
        if (Input.Attack == 1)
        {
            
        }
        
    }
}
