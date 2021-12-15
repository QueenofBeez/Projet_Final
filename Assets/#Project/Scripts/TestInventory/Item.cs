using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{    
    public enum InteractionType { NONE, PickUp, Examine, GrabDrop, Combinable, ListOfTasks, } // CheckTheControls
    public enum ItemType { Static, Consumables, Keys, Tasks } // Controls
    [Header("Attributes")]
    public InteractionType interactType;
    public ItemType type;
    public string itemName;
    public string combineWith;
    public GameObject combinationResult;

    [Header("Examine")]
    public string descriptionText;
    [Header("Custom Events")]
    public UnityEvent customEvent;
    public UnityEvent consumeEvent;
    [SerializeField] private TextMeshProUGUI pickupText;
    [SerializeField] private GameObject listofTasksText;
    // [SerializeField] private GameObject listofControlsText;
    [SerializeField] private KeyType keyType;

    [Header("NPC interaction")]
    public string NPCName;

    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 10;
    }

    private void Start()
    {
        pickupText.gameObject.SetActive(false);
        listofTasksText.gameObject.SetActive(false);
        // listofControlsText.gameObject.SetActive(false);
    }

    public void Interact()
    {
        switch(interactType)
        {
            case InteractionType.PickUp:
                //Add the object to the PickedUpItems list
                FindObjectOfType<InventorySystem>().PickUp(gameObject);
                //Disable
                gameObject.SetActive(false);
                break;
            case InteractionType.Examine:
                //Call the Examine item in the interaction system
                FindObjectOfType<InteractionSystem>().ExamineItem(this);                
                break;
            case InteractionType.ListOfTasks:
                FindObjectOfType<InteractionSystem>().CheckList(this);                
                break;
            // case InteractionType.CheckTheControls:
            //     FindObjectOfType<InteractionSystem>().CheckControls(this);                
            //     break;
            case InteractionType.GrabDrop:
                //Grab interaction
                FindObjectOfType<InteractionSystem>().GrabDrop();
                break;
            default:
                Debug.Log("NULL ITEM");
                break;
        }

        //Invoke (call) the custom event(s)
        customEvent.Invoke();
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            pickupText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            pickupText.gameObject.SetActive(false);
        }
    }

    public enum KeyType 
    {
        El,
        Mel,
        Parents,
        Closet,
        Bathroom,
        FrontDoor,
        None
    }

    public KeyType GetKeyType()
    {
        return keyType;
    }
}
