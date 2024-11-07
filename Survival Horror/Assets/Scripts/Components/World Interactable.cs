using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInteractable : MonoBehaviour, IInteractable
{
    public delegate void ConfirmAction(bool playerConfirmed, GameObject itemChecker);
    public static ConfirmAction confirm;

    [SerializeField] DocumentSO documentSO;

    [SerializeField] GameObject documentInspectorObj;
    //private DocumentInspector documentInspector;

    //public bool puzzleComplete = false;

    public Interaction player_interaction;

    private void Start()
    {
        player_interaction = GameObject.FindGameObjectWithTag("Player").GetComponent<Interaction>();

        confirm = checkIfPlayerConfirmed;
    }

    // called when the player presses 'F' near the document
    public void Interact()
    {
        Debug.Log("Player interacted with door");

        // keeps track of what the player is currently interacting with
        Manager.currentInteractionObj = gameObject;

        if (documentInspectorObj != null) Instantiate(documentInspectorObj);
        DocumentInspector.setDocument(documentSO);
    }

    // this probably wont be used
    public void checkIfPlayerConfirmed(bool confirm, GameObject itemChecker)
    {
        //Debug.Log("checkIfPlayerConfirmed parameter: " + confirm);
        if (confirm)// && itemChecker == gameObject)
        {
            //player_interaction.interact(item);

        }

        Debug.Log("checkIfPlayerConfirmed parameter: " + confirm);
    }
}
