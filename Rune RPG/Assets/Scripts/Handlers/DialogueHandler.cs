using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


[System.Serializable]
public class Dialogue
{
    public string name = "Name";
    public AudioClip[] dialogueSounds;

    [TextArea] public string[] text;
}

public class DialogueHandler : MonoBehaviour
{
    public static DialogueHandler Instance { get; private set; } // Make this class accessible from any script without all fields and functions having to be static

    public bool dialogueIsPlaying { get; private set; }
    [SerializeField] bool skipDialogue, continueDialogue;
    
    TMP_Text nameText, dialogueText;
    AudioSource dialogueSound;
    Canvas dialogueCanvas;

    public enum DialogueType
    {
        text,
        question
    }
    public enum DialogueSpeed
    {
        normal,
        slow,
        fast
    }

    void Start()
    {
        if (Instance == null) // Create singleton and then initialize it
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        nameText = GameObject.Find("NameText").GetComponent<TMP_Text>();
        dialogueText = GameObject.Find("DialogueText").GetComponent<TMP_Text>();
        dialogueSound = GetComponent<AudioSource>();
        dialogueCanvas = GetComponentInChildren<Canvas>();
    }

    public void PlayDialogue(Dialogue dialogue)
    {
        if (!dialogueIsPlaying)
        {
            StartCoroutine(DialogueRoutine(dialogue));
        }
    }

    public void AdvanceDialogue(InputAction.CallbackContext context)
    {
        if (context.performed && dialogueIsPlaying)
        {
            if (skipDialogue)
            {
                continueDialogue = true;
            }
            else
            {
                skipDialogue = true;
            }
        }
    }

    IEnumerator DialogueRoutine(Dialogue dialogue)
    {
        bool upcomingSpecialChar;
        float timer;

        dialogueIsPlaying = true;
        dialogueCanvas.enabled = true;
        nameText.text = dialogue.name; // I'm assigning the text directly instead of using 'SetText' because 'SetText' behaves wierd when the game is built

        for (int i = 0; i < dialogue.text.Length; i++) // Loop trough each piece of the provided dialogue
        {
            skipDialogue = false;
            continueDialogue = false;
            dialogueText.text = string.Empty; // string.Empty gang

            foreach (char letter in dialogue.text[i]) // Loop trough each character of the provided dialogue
            {
                upcomingSpecialChar = letter == 'ß'; // Checks if the next character is a 'ß', use this character for a pause after a comma or a dot for example

                if (!upcomingSpecialChar)
                {
                    dialogueText.text = dialogueText.text += letter; // Set the displayed dialogue to what it already is but concatenate the following letter of the provided dialogue to the displayed dialogue

                    if (!skipDialogue && !dialogueSound.isPlaying)
                    {
                        dialogueSound.clip = dialogue.dialogueSounds[Random.Range(0, dialogue.dialogueSounds.Length)]; // Play a random dialogue sound 
                        dialogueSound.pitch = Random.Range(0.9F, 1.1F); // Make the pitch random
                        dialogueSound.Play(); // Play the sound :3
                    }
                }

                timer = skipDialogue ? 0 : upcomingSpecialChar ? 0.5F : 0.025F; // If the player has skipped the dialogue the next characters will be displayed instantly, if its a special character the dialogue will wait 0.5 second and if its a regular character it will wait 0.025 second

                while (timer > 0 && !skipDialogue) // Used as a cooldown for displaying the next letter
                {
                    timer -= Time.deltaTime;
                    yield return null;
                }
            }

            skipDialogue = true;

            while (!continueDialogue) // Waits for the player to interact in order to continue the dialogue
            {
                yield return null;
            }
        }

        dialogueIsPlaying = false;
        dialogueCanvas.enabled = false;
    }
}
