using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadDedection : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 direction = collision.GetContact(0).normal;
        if (collision.gameObject.tag == "Player")
        {
            if (direction.y == -1)
            {
                Debug.Log($"{collision.gameObject.name} is dead !!");
                Destroy(transform.parent.transform.parent.gameObject);
            }
        }
    }
}
