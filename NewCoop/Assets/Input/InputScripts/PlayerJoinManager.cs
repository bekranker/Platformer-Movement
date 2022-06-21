using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerJoinManager : MonoBehaviour
{
    [SerializeField] GameObject PlayerPrefab;
    [SerializeField] List<GameObject> Players;
    [SerializeField] List<GameObject> PlayerSpawnOrder;
    [SerializeField] GameObject MapSpawnPointsParent;
    [SerializeField] List<GameObject> MapSpawnPoints;
    private void Awake()
    {
        foreach (var item in MapSpawnPointsParent.GetComponentsInChildren<Transform>())
        {
            if(item.gameObject != MapSpawnPointsParent)
            {
                MapSpawnPoints.Add(item.gameObject);
            }          
        }

        for (int i = 1; i < 5; i++)
        {
            string Control = PlayerPrefs.GetString("P" + i);
            if (Control != "")
            {
                GameObject Player = Instantiate<GameObject>(PlayerPrefab,Vector3.zero,Quaternion.identity);
                Players.Add(Player);
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

        PlayerSpawnOrder = new List<GameObject>(Players);

        for (int i = 0; i < PlayerSpawnOrder.Count; i++)
        {
            GameObject temp = PlayerSpawnOrder[i];
            int randomIndex = Random.Range(i, PlayerSpawnOrder.Count);
            PlayerSpawnOrder[i] = PlayerSpawnOrder[randomIndex];
            PlayerSpawnOrder[randomIndex] = temp;
        }

        for (int i = 0; i < PlayerSpawnOrder.Count; i++)
        {
            PlayerSpawnOrder[i].transform.position = MapSpawnPoints[i].transform.position;
        }
    }
}
