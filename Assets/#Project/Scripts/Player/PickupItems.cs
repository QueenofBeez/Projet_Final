using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickupItems : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pickupText;

    private bool pickupAllowed;

    private void Start()
    {
        pickupText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (pickupAllowed && Input.GetKeyDown(KeyCode.P))
            Pickup();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            pickupText.gameObject.SetActive(true);
            pickupAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            pickupText.gameObject.SetActive(false);
            pickupAllowed = false;
        }
    }

    private void Pickup()
    {
        Destroy(gameObject);
    }

}
