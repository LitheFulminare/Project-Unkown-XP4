using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentTextButton : MonoBehaviour
{
    public DocumentSO currentDocument;

    public void ButtonClick()
    {
        //Debug.Log($"current Document: {currentDocument.itemName}");
        DocumentImageManager.onHover(currentDocument);
    }
}
