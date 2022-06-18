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
    [SerializeField] Vector2 _startForce;

    [Space(10)]
    [Header("-----Object's Settings-----")]
    [BackgroundColor(0, 1, 0, 1)]
    [Space(25)]
    [SerializeField] GameObject AxePrefab;
    [SerializeField] SpriteRenderer AxeSprite;
    [SerializeField] SpriteRenderer OtherAxeSprite;
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
    private float Axedirection = 0f;
    [HideInInspector] public GameObject myAxe;
    [HideInInspector] public Rigidbody2D rb;
    private bool IsAttackOn;

    private void Update()
    {
        #region Shooting
        if (HasAxe)
        {
            if (Inputs.Attack == 1)
            {
                //eðim alma ayarlarý burasý
                calculating();
            }
            if (IsAttackOn)
            {
                if (Inputs.Attack == 0)
                {
                    if (IsPressing)
                    {
                        AxeSprite.enabled = false;
                        creatingComponents();
                        FalseAllArrows();
                        myAxe.GetComponent<Transform>().Rotate(0, 0, Axedirection);
                        AxeCount--;
                        if (AxeCount == 0) HasAxe = false;
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
                    if (AxeCount == 1)
                    {
                        AxeSprite.enabled = true;
                        OtherAxeSprite.enabled = false;
                    }
                    if (AxeCount == 2)
                    {
                        AxeSprite.enabled = false;
                        OtherAxeSprite.enabled = true;
                    }
                    Destroy(myAxe);
                    AxeCount++;
                    this.Wait(0.2f, () => HasAxe = true);
                }
            }
        }
        #endregion
    }

    #region calculate
    private void calculating()
    {
        IsPressing = true;
        movementManager.IsCanMove = false;
        GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Clamp(GetComponent<Rigidbody2D>().velocity.x, 0,0), GetComponent<Rigidbody2D>().velocity.y);
        Axedirection = ShootingDirection();
        IsAttackOn = true;
    }
    #endregion

    #region Create and give components
    private void creatingComponents()
    {
        myAxe = Instantiate(AxePrefab, transform.position, Quaternion.identity);
        rb = myAxe.GetComponent<Rigidbody2D>();
    }
    #endregion

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
    private float ShootingDirection()
    {
        Debug.ClearDeveloperConsole();
        float a = 0;
        switch (ShootDirectionSettings())
        {
            case 1:
                ArrowSystem(0);
                a = 0f;
                break;
            case 2:
                ArrowSystem(1);
                a = -315f;
                break;
            case 3:
                ArrowSystem(2);
                a = -270;
                break;
            case 4:
                ArrowSystem(3);
                a = -225;
                break;
            case 5:
                ArrowSystem(4);
                a = 180;
                break;
            case 6:
                ArrowSystem(5);
                a = -135;
                break;
            case 7:
                ArrowSystem(6);
                a = -90;
                break;
            case 8:
                ArrowSystem(7);
                a = -45;
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
    
    private void FalseAllArrows()
    {
        for (int i = 0; i < Arrows.Length; i++)
        {
            Arrows[i].SetActive(false);
        }
    }
    
    
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
