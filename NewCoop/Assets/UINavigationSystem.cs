using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UINavigationSystem : MonoBehaviour
{
    [SerializeField] GameObject SelectOnUp;
    [SerializeField] GameObject SelectOnDown;
    [SerializeField] GameObject SelectOnRight;
    [SerializeField] GameObject SelectOnLeft;

    UIManager uIManager;
    // Start is called before the first frame update
    void Start()
    {
        uIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                SelectUI(SelectOnUp);
            }
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                SelectUI(SelectOnDown);
            }
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                SelectUI(SelectOnRight);
            }
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                SelectUI(SelectOnLeft);
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
