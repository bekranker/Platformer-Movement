using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class GetControllers : MonoBehaviour
{
    //[SerializeField] List<InputDevice> Gamepads = new List<InputDevice>(4);
    [SerializeField] List<int> GamepadsId = new List<int>(4);
    [SerializeField] int GamepadCount;
    [SerializeField] GameObject PlayPanel;

    [SerializeField] GameObject PlayerMenuPrefab;
    // Start is called before the first frame update
    void Start()
    {
        #region Keyboard Join
       
        
        for (int i = 1; i < 3; i++)
        {
            AddKeyboard(i.ToString());
        }

        #endregion

        #region Controller Join
        foreach (InputDevice device in InputSystem.devices)
        {

            if (Gamepad.all.ToList().Contains(device))
            {                
                AddGamepad(device);
            }
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region Gamepad Plug In And Out
        InputSystem.onDeviceChange +=
        (device, change) =>
        {
            switch (change)
            {
                case InputDeviceChange.Added:
                    // New Device.
                    AddGamepad(device);
                    Debug.LogWarning("Added");
                    break;
                case InputDeviceChange.Disconnected:
                    // Device got unplugged.
                    RemoveGamepad(device);
                    Debug.LogWarning("Disconnected");
                    break;
                case InputDeviceChange.Reconnected:
                    Debug.LogWarning("Reconnected");
                    break;
                default:
                    // See InputDeviceChange reference for other event types.
                    break;
            }
        };
        #endregion
    }

    void AddKeyboard(string KeyboardIndex)
    {
        GameObject playerMenu = Instantiate<GameObject>(PlayerMenuPrefab);         
        PlayerInput Keyboard = playerMenu.GetComponent<PlayerInput>();

        Keyboard.GetComponent<PlayerMenuScript>().GetPlayPanel(PlayPanel);
        if(KeyboardIndex == "1")
        {
            Keyboard.SwitchCurrentControlScheme("Keyboard", InputSystem.devices[0]);
        }
        else
        {
            Keyboard.SwitchCurrentControlScheme("Keyboard2", InputSystem.devices[0]);
        }
    }


    void AddGamepad(InputDevice device)
    {
        if (Gamepad.all.ToList().Contains(device) && !GamepadsId.Contains(device.deviceId))
        {
            GamepadCount++;
            Debug.Log("Index :" + (1 + GamepadCount));
            PlayerInput Gamepad = Instantiate<GameObject>(PlayerMenuPrefab).GetComponent<PlayerInput>();

            Gamepad.GetComponent<PlayerMenuScript>().GetPlayPanel(PlayPanel);
            Debug.Log(device.deviceId);
            for (int i = 0; i < GamepadsId.Count; i++)
            {
                if (GamepadsId[i] == 0)
                {
                    Debug.Log(i+ " " + GamepadsId.Count + " " + GamepadsId[i]);
                    Gamepad.SwitchCurrentControlScheme("Controller" + (i + 1), device);
                    GamepadsId[i] = device.deviceId;
                    break;
                }
            }
        }      
    }

    void RemoveGamepad(InputDevice device)
    {
        Debug.Log("Bekir");
        if(gameObject != null)
        {
            Debug.Log(":D" + device.name + gameObject.name);

            for (int i = 0; i < GamepadsId.Count; i++)
            {
                if (GamepadsId[i] == device.deviceId)
                {
                    GamepadsId[i] = 0;
                    break;
                }
            }
            GamepadCount--;
        }      
    }
}
