using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocumentInspector : MonoBehaviour
{
    public delegate void SetDocument(DocumentSO documentSO);
    public static SetDocument setDocument;

    [SerializeField] Image documentImage;

    private DocumentSO _document;

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
        Debug.Log($"function was called, document is: {_document.itemName}");
    }


}
