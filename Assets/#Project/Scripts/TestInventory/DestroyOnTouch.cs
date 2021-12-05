using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyOnTouch : MonoBehaviour
{
    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     Destroy(gameObject);
    // }

    public UnityEvent whenPickUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            whenPickUp?.Invoke();
            Destroy(gameObject);
        }
    }
} 
