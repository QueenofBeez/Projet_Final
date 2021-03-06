using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    // [SerializeField] private TextMeshProUGUI displayNameText;
    // [SerializeField] private Animator portraitAnimator;
    // private Animator layoutAnimator;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    public List<GameObject> dialogues;
    public UnityEvent consumeEvent;
    public GameObject selectedDialogue;
    private Story currentStory;
    private InventorySystem inventory;
    private InteractionSystem interact;
    public GameObject glassOfWine;
    public GameObject parentsDoorKey;
    public bool dialogueIsPlaying { get; private set; }

    private static DialogueManager instance;

    // private const string SPEAKER_TAG = "speaker";
    // private const string PORTRAIT_TAG = "portrait";
    // private const string LAYOUT_TAG = "layout";

    private void Awake() 
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
    }

    public static DialogueManager GetInstance() 
    {
        return instance;
    }

    private void Start() 
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

    //     // get the layout animator
    //     layoutAnimator = dialoguePanel.GetComponent<Animator>();

    //     // get all of the choices text 
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices) 
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update() 
    {
        if (dialogueText.text == "[Is it this wine you seek?]\n")
        {
            glassOfWine.SetActive(false);
            parentsDoorKey.SetActive(true);
        }
        // return right away if dialogue isn't playing
        if (!dialogueIsPlaying) 
        {
            return;
        }

        // handle continuing to the next line in the dialogue when submit is pressed
        // NOTE: The 'currentStory.currentChoiecs.Count == 0' part was to fix a bug after the Youtube video was made
        if (InputManager.GetInstance().GetSubmitPressed()) //(currentStory.currentChoices.Count == 0 && InputManager.GetInstance().GetSubmitPressed())
        {
            ContinueStory();
        }
    }

    // public void EnterOrderDialogueMode()
    // {
    //     if (dialogueIsPlaying) return;
    //     int index = Random.Range(0, dialogues.Count);

    //     selectedDialogue = dialogues[index];
    //     DialogueTrigger dialogueTrigger = selectedDialogue.GetComponent<DialogueTrigger>();
    //     if (dialogueTrigger == null)
    //     {
    //         Debug.LogError($"No DialogueTrigger in gameObject \"{selectedDialogue.name}\".", gameObject);
    //     }
    // }

    public void EnterDialogueMode(TextAsset inkJSON) 
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

    //     // reset portrait, layout, and speaker
    //     displayNameText.text = "???";
    //     portraitAnimator.Play("default");
    //     layoutAnimator.Play("right");

        ContinueStory();
    }

    private IEnumerator ExitDialogueMode() 
    {
        yield return new WaitForSeconds(0.2f);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory() 
    {
        if (currentStory.canContinue) 
        {
            // set text for the current dialogue line
            dialogueText.text = currentStory.Continue();
            // display choices, if any, for this dialogue line
            DisplayChoices();
            // handle tags
            // HandleTags(currentStory.currentTags);
        }
        else if (currentStory.currentChoices.Count == 0)
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    // private void HandleTags(List<string> currentTags)
    // {
    //     // loop through each tag and handle it accordingly
    //     foreach (string tag in currentTags) 
    //     {
    //         // parse the tag
    //         string[] splitTag = tag.Split(':');
    //         if (splitTag.Length != 2) 
    //         {
    //             Debug.LogError("Tag could not be appropriately parsed: " + tag);
    //         }
    //         string tagKey = splitTag[0].Trim();
    //         string tagValue = splitTag[1].Trim();
            
    //         // handle the tag
    //         switch (tagKey) 
    //         {
    //             case SPEAKER_TAG:
    //                 displayNameText.text = tagValue;
    //                 break;
    //             case PORTRAIT_TAG:
    //                 portraitAnimator.Play(tagValue);
    //                 break;
    //             case LAYOUT_TAG:
    //                 layoutAnimator.Play(tagValue);
    //                 break;
    //             default:
    //                 Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
    //                 break;
    //         }
    //     }
    // }

    private void DisplayChoices() 
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // defensive check to make sure our UI can support the number of choices coming in
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " 
                + currentChoices.Count);
        }

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach(Choice choice in currentChoices) 
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++) 
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice() 
    {
       // Event System requires we clear it first, then wait
       // for at least one frame before we set the current selected object.
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        InputManager.GetInstance().RegisterSubmitPressed(); // this is specific to my InputManager script
        ContinueStory();
    }

}
