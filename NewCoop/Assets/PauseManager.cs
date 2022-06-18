using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    bool isPaused;
    [SerializeField] GameObject PausePanel;
    // Update is called once per frame
    void Update()
    {
        PausePanel.SetActive(isPaused);
    }

    public void Pause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }
}
