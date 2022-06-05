using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UINavigationSystem : MonoBehaviour
{
    [SerializeField] GameObject SelectOnUp;
    [SerializeField] GameObject SelectOnDown;
    [SerializeField] GameObject SelectOnRight;
    [SerializeField] GameObject SelectOnLeft;

    UIManager uIManager;
    InputSelection inputSelection;
    // Start is called before the first frame update
    void Start()
    {
        uIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        inputSelection = GameObject.FindGameObjectWithTag("InputSelection").GetComponent<InputSelection>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            //Keyboard
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                SelectUI(SelectOnUp);
            }
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                SelectUI(SelectOnDown);
            }
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                SelectUI(SelectOnRight);
            }
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                SelectUI(SelectOnLeft);
            }

            //Xbox
            if ((Input.GetAxisRaw("XboxDpadVertical") > 0 || Input.GetAxisRaw("Xbox2DpadVertical") > 0) && inputSelection.Controllers.Contains("Xbox"))
            {
                SelectUI(SelectOnUp);
            }
            if ((Input.GetAxisRaw("XboxDpadVertical") < 0 || Input.GetAxisRaw("Xbox2DpadVertical") > 0) && inputSelection.Controllers.Contains("Xbox"))
            {
                SelectUI(SelectOnDown);
            }
            if ((Input.GetAxisRaw("XboxDpadHorizontal") > 0 || Input.GetAxisRaw("Xbox2DpadHorizontal") > 0) && inputSelection.Controllers.Contains("Xbox"))
            {
                SelectUI(SelectOnRight);
            }
            if ((Input.GetAxisRaw("XboxDpadHorizontal") < 0 || Input.GetAxisRaw("Xbox2DpadHorizontal") > 0) && inputSelection.Controllers.Contains("Xbox"))
            {
                SelectUI(SelectOnLeft);
            }

            //Ps
            if ((Input.GetAxisRaw("PsDpadVertical") > 0 || Input.GetAxisRaw("Ps2DpadVertical") > 0) && inputSelection.Controllers.Contains("Ps"))
            {
                SelectUI(SelectOnUp);
            }
            if ((Input.GetAxisRaw("PsDpadVertical") < 0 || Input.GetAxisRaw("Ps2DpadVertical") > 0) && inputSelection.Controllers.Contains("Ps"))
            {
                SelectUI(SelectOnDown);
            }
            if ((Input.GetAxisRaw("PsDpadHorizontal") > 0 || Input.GetAxisRaw("Ps2DpadHorizontal") > 0) && inputSelection.Controllers.Contains("Ps"))
            {
                SelectUI(SelectOnRight);
            }
            if ((Input.GetAxisRaw("PsDpadHorizontal") < 0 || Input.GetAxisRaw("Ps2DpadHorizontal") > 0) && inputSelection.Controllers.Contains("Ps"))
            {
                SelectUI(SelectOnLeft);
            }
        }
    }
    
    void SelectUI(GameObject Button)
    {
        if(uIManager.isReady() && Button != null)
        {
            EventSystem.current.SetSelectedGameObject(Button);
        }   
    }
}
