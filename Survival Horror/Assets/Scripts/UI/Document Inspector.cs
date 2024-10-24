using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InspectorState
{
    Image = 0,
    Text = 1,
}

public class DocumentInspector : MonoBehaviour
{
    public delegate void SetDocument(DocumentSO documentSO);
    public static SetDocument setDocument;

    [SerializeField] Image documentImage;
    [SerializeField] Text documentText;

    private DocumentSO _document;

    private float spawnTime;

    //private InspectorState inspectorState = InspectorState.Image;

    private void Awake()
    {
        spawnTime = Time.time;
        Debug.Log($"Spawn time: {spawnTime}");
    }

    private void OnEnable()
    {
        setDocument += OnSpawn;
    }

    private void OnDisable()
    {
        setDocument -= OnSpawn;
    }

    public void OnSpawn(DocumentSO document)
    {
        _document = document;
        documentImage.sprite = _document.backgroundImage;
        documentText.text = "";
    }

    public void OnExit()
    {
        PlayerVars.BlockPlayer(true);
    }

    private void Update()
    {
        if (Input.anyKey && Time.time > spawnTime + 1f)
        {
            ChangeImage();
        }
    }

    private void ChangeImage()
    {
        documentImage.color = Color.gray;
        ShowText();

        // this might be used later to know when to exit the inspection screen
        //switch (inspectorState)
        //{
        //    case InspectorState.Image: documentImage.color = Color.gray; break;
        //    case InspectorState.Text: ShowText(); break;
        //}

        //inspectorState = (inspectorState) + 1;

    }

    private void ShowText()
    {
        string formattedText = "";

        for (int i = 0; i < _document.paragraphs.Length; i++)
        {
            if (i != 0)
            {
                formattedText += "\n\n" + _document.paragraphs[i];
            }
            else formattedText += _document.paragraphs[i];
        }

        documentText.text = formattedText;
    }
}
