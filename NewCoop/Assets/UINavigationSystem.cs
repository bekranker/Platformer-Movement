using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UINavigationSystem : MonoBehaviour
{
    [SerializeField] GameObject SelectOnUp;
    [SerializeField] GameObject SelectOnDown;
    [SerializeField] GameObject SelectOnRight;
    [SerializeField] GameObject SelectOnLeft;

    UIManager uIManager;
    InputSelection inputSelection;

    [SerializeField] bool SelectionButton;
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
            if (!SelectionButton)
            {
            
            #region Keyboard
            if (PlayerPrefs.GetString("Keyboard1" + "Up") != "")
            {
                if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Keyboard1" + "Up"))))
                {
                    SelectUI(SelectOnUp);
                }
            }
            if (PlayerPrefs.GetString("Keyboard2" + "Up") != "")
            {
                if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Keyboard2" + "Up"))))
                {
                    SelectUI(SelectOnUp);
                }
            }

            if (PlayerPrefs.GetString("Keyboard1" + "Down") != "")
            {
                if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Keyboard1" + "Down"))))
                {
                    SelectUI(SelectOnDown);
                }
            }
            if (PlayerPrefs.GetString("Keyboard2" + "Down") != "")
            {
                if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Keyboard2" + "Down"))))
                {
                    SelectUI(SelectOnDown);
                }
            }

            if (PlayerPrefs.GetString("Keyboard1" + "Right") != "")
            {
                if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Keyboard1" + "Right"))))
                {
                    SelectUI(SelectOnRight);
                }
            }
            if (PlayerPrefs.GetString("Keyboard2" + "Right") != "")
            {
                if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Keyboard2" + "Right"))))
                {
                    SelectUI(SelectOnRight);
                }
            }

            if (PlayerPrefs.GetString("Keyboard1" + "Left") != "")
            {
                if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Keyboard1" + "Left"))))
                {
                    SelectUI(SelectOnLeft);
                }
            }
            if (PlayerPrefs.GetString("Keyboard2" + "Left") != "")
            {
                if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Keyboard2" + "Left"))))
                {
                    SelectUI(SelectOnLeft);
                }
            }

            if (PlayerPrefs.GetString("Keyboard1" + "Jump") != "")
            {
                if (Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Keyboard1" + "Jump"))))
                {
                    gameObject.GetComponent<Button>().onClick.Invoke();
                }
            }
            if (PlayerPrefs.GetString("Keyboard2" + "Jump") != "")
            {
                if (Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Keyboard2" + "Jump"))))
                {
                    gameObject.GetComponent<Button>().onClick.Invoke();
                }
            }
            #endregion

            #region Xbox
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

            if(Input.GetButtonUp("XboxButtonA") && inputSelection.Controllers[0] == "Xbox")
            {
                gameObject.GetComponent<Button>().onClick.Invoke();
            }
            if (Input.GetButtonUp("Xbox2ButtonA") && inputSelection.Controllers[1] == "Xbox")
            {
                gameObject.GetComponent<Button>().onClick.Invoke();
            }
            #endregion

            #region Ps
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

            if (Input.GetButtonUp("PsButtonX") && inputSelection.Controllers[0] == "Ps")
            {
                gameObject.GetComponent<Button>().onClick.Invoke();
            }
            if (Input.GetButtonUp("PsButtonX") && inputSelection.Controllers[1] == "Ps")
            {
                gameObject.GetComponent<Button>().onClick.Invoke();
            }
                #endregion

            }
            else
            {
                gameObject.GetComponent<Button>().onClick.Invoke();
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
