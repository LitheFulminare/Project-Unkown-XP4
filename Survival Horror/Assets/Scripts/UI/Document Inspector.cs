using System;
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

    public delegate void MakeBackgroundBlack();
    public static MakeBackgroundBlack makeBackgroundBlack;

    [SerializeField] Image documentImage;
    [SerializeField] Text documentText;
    [SerializeField] Image backgroundImage;

    private DocumentSO _document;

    private float spawnTime;

    private InspectorFocusState inspectorState = InspectorFocusState.Image;

    private bool isBackgroundBlack;

    private void Awake()
    {
        spawnTime = Time.time;
    }

    private void OnEnable()
    {
        setDocument += OnSpawn;
        makeBackgroundBlack += InventorySpawn;
    }

    private void OnDisable()
    {
        setDocument -= OnSpawn;
        makeBackgroundBlack -= InventorySpawn;
    }

    private void InventorySpawn()
    {
        // ensures that it only be black if makeBackgroundBlack
        // otherwise just calling setDocument will make the BG gray
        isBackgroundBlack = true;

        if (isBackgroundBlack)
        {
            backgroundImage.color = new Color(0, 0, 0, 1);
        }

        else
        {
            backgroundImage.color = new Color(0, 0, 0, 0.686f);
            isBackgroundBlack = false;
        }

    }

    // when the document is opened
    public void OnSpawn(DocumentSO document)
    {
        PlayerVars.BlockPlayer(true);
        documentImage.preserveAspect = true;
        _document = document;

        documentText.text = "";

        // some 'documents' (mainly random item interactions) don't have background images, so it shows the description
        if (_document.backgroundImage == null)
        {
            ChangeImage();
            inspectorState = InspectorFocusState.Description;
        }

        else
        {
            documentImage.sprite = _document.backgroundImage;
            if (_document.isFullscreen)
            {
                documentImage.rectTransform.localScale = new Vector3(3.2f, 2.4f, 1);
            }
            else
            {
                documentImage.rectTransform.localScale = new Vector3(1, 1, 1);
            }

            inspectorState = InspectorFocusState.Image;
        }
    }

    // when the document inspection screen is closed
    public void Exit()
    {
        Debug.Log($"Current Interaction Obj: {Manager.currentInteractionObj}");
        InteractableDocument.UseBoundObject?.Invoke();
        
        PlayerVars.BlockPlayer(false);
        Destroy(gameObject);
    }

    private void Update()
    {
        //Debug.Log($"Current state: {inspectorState}");

        if (Input.anyKey && Time.time > spawnTime + 0.3f)
        {
            ChangeImage();
        }
    }

    // changes the whole screen
    // the default loop the showing the image -> name with the image grayed out -> item description -> close the screen
    private void ChangeImage()
    {
        if (_document.backgroundImage != null) documentImage.color = Color.gray;
        else documentImage.color = Color.clear;

        if (inspectorState == InspectorFocusState.Image && _document.itemName != "")
        {
            ShowName();
        }
        else if ((inspectorState == InspectorFocusState.Name || _document.itemName == "") && inspectorState != InspectorFocusState.Description)
        {
            ShowDescription();
        }
        else if (inspectorState == InspectorFocusState.Description)
        {
            Exit();
        }

        spawnTime = Time.time;
    }

    private void ShowName()
    {
        documentText.text = _document.itemName;
        documentText.alignment = TextAnchor.MiddleCenter;
        inspectorState = InspectorFocusState.Name;
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
        if (!_document.centralizeText) documentText.alignment = TextAnchor.UpperCenter;
        else documentText.alignment = TextAnchor.MiddleCenter;

        inspectorState = InspectorFocusState.Description;
    }
}
