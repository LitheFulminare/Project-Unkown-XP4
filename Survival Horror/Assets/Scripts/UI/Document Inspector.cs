using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocumentInspector : MonoBehaviour
{
    [SerializeField] Image documentImage;

    private DocumentSO _document;

    public void function(DocumentSO document)
    {
        _document = document;
        //documentImage = _document.inspectImage;
        Debug.Log("function was called");
    }
}
