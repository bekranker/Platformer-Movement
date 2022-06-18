using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugManager : MonoBehaviour
{
    [SerializeField] MovementBehaviour Ipnuts;

    PauseManager pauseManager;
    private void Start()
    {
        pauseManager = GameObject.Find("PauseManager").GetComponent<PauseManager>();
    }

    void Update()
    {
        if (Ipnuts.Start == 1)
        {
            pauseManager.Pause();
        }
    }
}
