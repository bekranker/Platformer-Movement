using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CombatManager : MonoBehaviour
{
    [Space(10)]
    [Header("-----Axe Physics-----")]
    [Space(25)]
    [BackgroundColor(1, 0, 0, 1)]
    [SerializeField] float AxeSpeed;

    [Space(10)]
    [Header("-----Object's Settings-----")]
    [BackgroundColor(0, 1, 0, 1)]
    [Space(25)]
    [SerializeField] GameObject AxePrefab;
    [SerializeField] SpriteRenderer AxeSprite;
    [SerializeField] Transform AxeSpawner;
    [SerializeField] GameObject[] Arrows;
    [SerializeField] LayerMask AxeMask;
    
    [Space(10)]
    [Header("-----Statues-----")]
    [Space(25)]
    [BackgroundColor(0,0,1,1)]
    public bool HasAxe = true;
    public int AxeCount;
    private Collider2D IsTouchingAxe;

    [Space(10)]
    [Header("----Components-----")]
    [Space(25)]
    [BackgroundColor(0, 1, 1, 1)]
    [SerializeField] MovementBehaviour Inputs;
    [SerializeField] MovementManager movementManager;

    private bool IsPressing;
    private Vector2 Axedirection = Vector2.zero;
    private GameObject myAxe;
    private bool IsAttackOn;

    private void Update()
    {
        Debug.Log("Axe count: " + AxeCount);
        #region Shooting
        if (HasAxe)
        {
            if (Inputs.Attack == 1)
            {
                //eðim alma ayarlarý burasý
                Debug.Log("Eðim alýnýyor");
                IsPressing = true;
                movementManager.IsCanMove = false;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                Axedirection = ShootingDirection();
                
                IsAttackOn = true;
            }
            if (IsAttackOn)
            {
                if (Inputs.Attack == 0)
                {
                    if (IsPressing)
                    {
                        movementManager.CalculatingStatues();
                        //Fýrlatma ayarlarý burasý
                        AxeSprite.enabled = false;
                        myAxe = Instantiate(AxePrefab, transform.position, Quaternion.identity);
                        Rigidbody2D AxeRb = myAxe.GetComponent<Rigidbody2D>();
                        AxeRb.velocity = (Axedirection * Time.fixedDeltaTime * 100 * AxeSpeed);
                        Debug.Log("Fýrlattý");
                        for (int i = 0; i < Arrows.Length; i++)
                        {
                            Arrows[i].SetActive(false);
                        }
                        AxeCount--;
                        if (AxeCount == 0)
                        {
                            HasAxe = false;
                           
                        }
                        IsAttackOn = false;
                        this.Wait(0.2f, () => movementManager.IsCanMove = true);
                    }
                }
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

        #region Taking axe back
        if (AxeCount <= 2)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (IsTouchingAxe)
                {
                    myAxe.transform.position = Vector2.Lerp(myAxe.transform.position, transform.position, 1f);
                    AxeSprite.enabled = true;
                    Destroy(myAxe);
                    AxeCount++;
                    this.Wait(0.2f, () => HasAxe = true);
                }
            }
        }
        #endregion
    }

    private void FixedUpdate()
    {
        IsTouchingAxe = Physics2D.OverlapBox(transform.position, Vector2.one, default, AxeMask);
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
                ArrowSystem(0);
                a = Vector2.up;
                break;
            case 2:
                ArrowSystem(1);
                a = Vector2.up + Vector2.left;
                break;
            case 3:
                ArrowSystem(2);
                a = Vector2.left;
                break;
            case 4:
                ArrowSystem(3);
                a = Vector2.down + Vector2.left;
                break;
            case 5:
                ArrowSystem(4);
                a = Vector2.down;
                break;
            case 6:
                ArrowSystem(5);
                a = Vector2.right + Vector2.down;
                break;
            case 7:
                ArrowSystem(6);
                a = Vector2.right;
                break;
            case 8:
                ArrowSystem(7);
                a = Vector2.right + Vector2.up;
                break;
            case 0:
                ArrowSystem(9);
                Debug.Log("Lost Direction");
                break;
            default:
                break;
        }
        return a;
    }
    #endregion

    #region Arrow System
    private void ArrowSystem(int key)
    {
        if (key == 9)
        {
            for (int i = 7; i >= 0; i--)
            {
                Arrows[i].SetActive(false);
            }
        }
        else if (key == 8)
        {
            Arrows[7].SetActive(true);
            int i = key - 1;
            for (; i >= 0; i--)
            {
                Arrows[i].SetActive(false);
            }
        }
        else
        {
            Arrows[key].SetActive(true);
            int i = key + 1;
            int a = key - 1;
            if (key == 0)
            {
                for (; i < Arrows.Length; i++)
                {
                    Arrows[i].SetActive(false);
                }
            }
            else
            {
                for (; i < Arrows.Length; i++)
                {
                    Arrows[i].SetActive(false);
                }
                for (; a >= 0; a--)
                {
                    Arrows[a].SetActive(false);
                }
            }
            
        }

    }
    #endregion
}
