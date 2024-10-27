using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocumentTextManager : MonoBehaviour
{
    public delegate void TextInteraction(DocumentSO documentSO);
    public static TextInteraction ButtonPressed;

    public delegate void UpdateText();
    public static UpdateText updateText;

    [SerializeField] private GameObject documentText;
    [SerializeField] private GameObject panel;

    private List<GameObject> texts = new List<GameObject>();

    private void OnEnable()
    {
        ButtonPressed += InspectDocument;

        updateText += ChangeText;
    }

    private void OnDisable()
    {
        ButtonPressed -= InspectDocument;

        updateText -= ChangeText;
    }

    public void Start()
    {
        texts.AddRange(GetChildren(panel));
    }

    public void ChangeText()
    {
        if (PlayerVars.documentList == null) return;

        while (texts.Count < PlayerVars.documentList.Count)
        {
            GameObject text = Instantiate(documentText);
            text.transform.SetParent(panel.transform);
            texts.Add(text);
        }

        for (int i = 0; i < texts.Count; i++)
        {
            if (i >= PlayerVars.documentList.Count) continue;

            Text text = texts[i].GetComponent<Text>();
            DocumentTextButton docTextButton = texts[i].GetComponent<DocumentTextButton>();

            text.text = PlayerVars.documentList[i].itemName;
            docTextButton.currentDocument = PlayerVars.documentList[i];
        }
    }

    private void InspectDocument(DocumentSO documentSO)
    {
        // still need to call the event lol
        // instatiate the inspector and maybe pass extra parameters to make the screen black instead of fading, idk
        Debug.Log($"InspectDocument parameter {documentSO}");
    }

    private List<GameObject> GetChildren(GameObject gameObject)
    {
        List<GameObject> children = new List<GameObject>();

        foreach (Transform child in gameObject.transform)
        {
            children.Add(child.gameObject);
        }

        return children;
    }
}
