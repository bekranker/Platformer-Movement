using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class GetControllers : MonoBehaviour
{
    [SerializeField] List<InputDevice> Gamepads = new List<InputDevice>(4);
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
            Debug.Log(device);
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
        Debug.Log("Keyboard" + KeyboardIndex);
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
        if (Gamepad.all.ToList().Contains(device) && !Gamepads.Contains(device))
        {
            GamepadCount++;
            Debug.Log("Index :" + (1 + GamepadCount));
            PlayerInput Gamepad = Instantiate<GameObject>(PlayerMenuPrefab).GetComponent<PlayerInput>();

            Gamepad.GetComponent<PlayerMenuScript>().GetPlayPanel(PlayPanel);
            for (int i = 0; i < Gamepads.Count; i++)
            {
                if (Gamepads[i] == null)
                {
                    Gamepad.SwitchCurrentControlScheme("Controller" + (i + 1), device);
                    Gamepads[i] = device;
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

            for (int i = 0; i < Gamepads.Count; i++)
            {
                if (Gamepads[i] == device)
                {
                    Gamepads[i] = null;
                    break;
                }
            }
            GamepadCount--;
        }      
    }
}
