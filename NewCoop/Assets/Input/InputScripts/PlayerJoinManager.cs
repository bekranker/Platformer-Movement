using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerJoinManager : MonoBehaviour
{
    [SerializeField] GameObject PlayerPrefab;
    private void Awake()
    {
        //int playerNo = 0;
        for (int i = 1; i < 5; i++)
        {
            string Control = PlayerPrefs.GetString("P" + i);
            if (Control != "")
            {
                GameObject Player = Instantiate<GameObject>(PlayerPrefab,Vector3.zero,Quaternion.identity);
                if (Control.Contains("Keyboard"))
                {
                    Player.GetComponent<PlayerInput>().SwitchCurrentControlScheme(Control, InputSystem.devices[0]);
                    Player.GetComponent<MovementBehaviour>().GetData("P" + i, null);                   
                }
                else
                {
                    Debug.Log(Control);
                    Player.GetComponent<PlayerInput>().SwitchCurrentControlScheme(Control, InputSystem.GetDevice<Gamepad>(new InternedString("P"+i)));
                    Player.GetComponent<MovementBehaviour>().GetData("P" + i, InputSystem.GetDevice<Gamepad>(new InternedString("P" + i)));
                }
                Player.name = "P" + i;
            }
        }
    }
}
