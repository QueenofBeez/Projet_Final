using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneChanger : MonoBehaviour
{

    public bool isOpen = true;
    public Sprite doorOpen;
    public Sprite doorClosed;

void Start()
    {
        //Set the tag of this GameObject to Player
        gameObject.tag = "Player";
        if(isOpen)
        {
            GetComponent<SpriteRenderer>().sprite = doorOpen;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = doorClosed;
        }
    }

    public UnityEvent whenEnter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Check to see if the tag on the collider is equal to Enemy
        if (other.CompareTag("Player") && isOpen)
        {
            whenEnter?.Invoke();
        }
        //if(other.CompareTage("Player"))
        //{
            //int index = SceneManager.GetActiveScene().buildindex;
            //SceneManager.LoadScene(index + 1);
        //}
    }

    public void Unlock()
    {
        isOpen = true;
        {
            GetComponent<SpriteRenderer>().sprite = doorOpen;
        }
    }

    public void Load()
    {       
            int index = SceneManager.GetActiveScene().buildIndex; //charge l'index de sc�ne suivante o/ et apr�s on load la sc�ne
            SceneManager.LoadScene(index + 1);
            //Debug.Log("ok I'm in");
    }

        public void Exit()
    {
        Application.Quit();
    }   
}
