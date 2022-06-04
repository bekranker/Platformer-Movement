using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject PlayerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer("P1");
        SpawnPlayer("P2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnPlayer(string Player)
    {
        GameObject NewPlayer;
        switch (PlayerPrefs.GetString(Player))
        {         
            case "Keyboard1":
                NewPlayer = Instantiate<GameObject>(PlayerPrefab, Vector3.zero, Quaternion.identity);
                NewPlayer.GetComponent<Movement>().GetControl(Controls.Keyboard1);
                break;
            case "Keyboard2":
                NewPlayer = Instantiate<GameObject>(PlayerPrefab, Vector3.zero, Quaternion.identity);
                NewPlayer.GetComponent<Movement>().GetControl(Controls.Keyboard2);
                break;
            case "Xbox":
                NewPlayer = Instantiate<GameObject>(PlayerPrefab, Vector3.zero, Quaternion.identity);
                NewPlayer.GetComponent<Movement>().GetControl(Controls.Xbox);
                break;
            case "Xbox2":
                NewPlayer = Instantiate<GameObject>(PlayerPrefab, Vector3.zero, Quaternion.identity);
                NewPlayer.GetComponent<Movement>().GetControl(Controls.Xbox2);
                break;
            case "Ps":
                NewPlayer = Instantiate<GameObject>(PlayerPrefab, Vector3.zero, Quaternion.identity);
                NewPlayer.GetComponent<Movement>().GetControl(Controls.Ps);
                break;
            case "Ps2":
                NewPlayer = Instantiate<GameObject>(PlayerPrefab, Vector3.zero, Quaternion.identity);
                NewPlayer.GetComponent<Movement>().GetControl(Controls.Ps2);
                break;
            default:
                break;
        }
    }
}
