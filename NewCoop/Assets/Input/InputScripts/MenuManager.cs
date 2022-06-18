using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select(GameObject NewButton)
    {
        EventSystem.current.SetSelectedGameObject(NewButton);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GotoScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ChangeMap(string ActionMapName)
    {
        foreach (var playerMenu in GameObject.FindGameObjectsWithTag("PlayerMenu"))
        {
            playerMenu.GetComponent<PlayerInput>().SwitchCurrentActionMap(ActionMapName);
        }
    }
}
