using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject FirstStart, SelectionButton, KeyBindButton, SecondBack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextSceneMethod()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SelectSelectionButton()
    {
        EventSystem.current.SetSelectedGameObject(SelectionButton);
    }

    public void SelectFirstStartButton()
    {
        EventSystem.current.SetSelectedGameObject(FirstStart);
    }

    public void SelectSecondStartButton()
    {
        EventSystem.current.SetSelectedGameObject(SecondBack);
    }
    public void SelectKeyBindButton()
    {
        EventSystem.current.SetSelectedGameObject(KeyBindButton);
    }
}
