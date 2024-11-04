using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InspectorFocusState
{
    Image = 0,
    Name = 1,
    Description = 2,
}

public class DocumentInspector : MonoBehaviour
{
    public delegate void SetDocument(DocumentSO documentSO);
    public static SetDocument setDocument;

    [SerializeField] Image documentImage;
    [SerializeField] Text documentText;

    private DocumentSO _document;

    private float spawnTime;

    private InspectorFocusState inspectorState = InspectorFocusState.Image;

    private void Awake()
    {
        spawnTime = Time.time;
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
        PlayerVars.BlockPlayer(true);
        _document = document;
        documentImage.sprite = _document.backgroundImage;
        documentText.text = "";
    }

    public void Exit()
    {
        PlayerVars.BlockPlayer(false);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.anyKey && Time.time > spawnTime + 0.3f)
        {
            ChangeImage();
        }
    }

    private void ChangeImage()
    {
        documentImage.color = Color.gray;

        switch (inspectorState)
        {
            case InspectorFocusState.Image: ShowName(); break;
            case InspectorFocusState.Name: ShowDescription(); break;          
            case InspectorFocusState.Description: Exit(); break;
        }

        inspectorState = (inspectorState) + 1;

        spawnTime = Time.time;
    }

    private void ShowName()
    {
        documentText.text = _document.itemName;
        documentText.alignment = TextAnchor.MiddleCenter;
    }

    private void ShowDescription()
    {
        string formattedText = "";

        for (int i = 0; i < _document.paragraphs.Length; i++)
        {
            if (i != 0)
            {
                formattedText += "\n";

                if (_document.skipLines) formattedText += "\n";
            }

            formattedText += _document.paragraphs[i];
        }

        documentText.text = formattedText;
        documentText.alignment = TextAnchor.UpperCenter;
    }
}
