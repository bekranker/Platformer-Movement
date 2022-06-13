using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Header("-----Axe Physics-----")]
    [SerializeField] float AxeSpeed;

    [Header("-----Object's Settings-----")]
    [SerializeField] GameObject AxePrefab;
    [SerializeField] SpriteRenderer AxeSprite;
    [SerializeField] Transform AxeSpawner;


    [Header("-----Statues-----")]
    public bool HasAxe = true;
    public int AxeCount;


    [Header("----Components-----")]
    [SerializeField] MovementBehaviour Inputs;
    [SerializeField] MovementManager movementManager;

    private bool IsPressing;
    Vector2 Axedirection = Vector2.zero;

    private void Update()
    {
        #region Shooting
        if (HasAxe)
        {
            if (Inputs.Attack == 0)
            {
                if (IsPressing)
                {
                    //Fýrlatma ayarlarý burasý
                    AxeSprite.enabled = false;
                    GameObject AxeObject = Instantiate(AxePrefab, transform.position, Quaternion.identity);
                    Rigidbody2D AxeRb = AxeObject.GetComponent<Rigidbody2D>();
                    AxeRb.velocity = (Axedirection * Time.fixedDeltaTime * AxeSpeed);
                    Debug.Log("Fýrlattý");
                    this.Wait(0.2f, () => movementManager.IsCanMove = true);
                    HasAxe = false;
                }
            }
            else if (Inputs.Attack == 1)
            {
                //eðim alma ayarlarý burasý
                Debug.Log("Eðim alýnýyor");
                IsPressing = true;
                movementManager.IsCanMove = false;
                Axedirection = ShootingDirection();
            }
        }
        #endregion

        #region Melee Combot
        if (HasAxe)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Meele Combat With AXE");
            }
        }
        #endregion
    }

    #region Shooting direction settings
    private int ShootDirectionSettings()
    {
        if (Inputs.x == 0 && Inputs.y == 1)
        {
            return 1;
        }
        if (Inputs.x == -1 && Inputs.y == 1)
        {
            return 2;
        }
        if (Inputs.x == -1 && Inputs.y == 0)
        {
            return 3;
        }
        if (Inputs.x == -1 && Inputs.y == -1)
        {
            return 4;
        }
        if (Inputs.x == 0 && Inputs.y == -1)
        {
            return 5;
        }
        if (Inputs.x == 1 && Inputs.y == -1)
        {
            return 6;
        }
        if (Inputs.x == 1 && Inputs.y == 0)
        {
            return 7;
        }
        if (Inputs.x == 1 && Inputs.y == 1)
        {
            return 8;
        }
        else
        {
            return 0;
        }
    }
    #endregion

    #region Shooting Direction
    private Vector2 ShootingDirection()
    {
        Debug.ClearDeveloperConsole();
        Debug.Log($"Direction Index: {ShootDirectionSettings()}");
        Vector2 a = Vector2.zero;
        switch (ShootDirectionSettings())
        {
            case 1:
                a = Vector2.up;
                break;
            case 2:
                a = Vector2.up + Vector2.left;
                break;
            case 3:
                a = Vector2.left;
                break;
            case 4:
                a = Vector2.down + Vector2.left;
                break;
            case 5:
                a = Vector2.down;
                break;
            case 6:
                a = Vector2.right + Vector2.down;
                break;
            case 7:
                a = Vector2.right;
                break;
            case 8:
                a = Vector2.right + Vector2.up;
                break;
            case 0:
                Debug.Log("Lost Direction");
                break;
            default:
                break;
        }
        return a;
    }
    #endregion
}
