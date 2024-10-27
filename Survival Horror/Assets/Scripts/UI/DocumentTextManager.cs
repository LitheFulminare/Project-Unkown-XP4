using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocumentTextManager : MonoBehaviour
{
    public delegate void UpdateText();
    public static UpdateText updateText;

    [SerializeField] private GameObject documentText;
    [SerializeField] private GameObject panel;

    private List<GameObject> texts = new List<GameObject>();

    private void OnEnable()
    {
        updateText += ChangeText;
    }

    private void OnDisable()
    {
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
