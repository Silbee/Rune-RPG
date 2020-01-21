using UnityEngine;

public class NPC : MonoBehaviour
{
    public Dialogue dialogue;

    void Start()
    {
        DialogueHandler.Instance.PlayDialogue(dialogue);
    }
}
