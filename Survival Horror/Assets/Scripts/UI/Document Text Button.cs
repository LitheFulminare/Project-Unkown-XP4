using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentTextButton : MonoBehaviour
{
    public DocumentSO currentDocument;

    // rn the image changes when the player clicks the button, should be on hover
    public void ButtonClick()
    {
        //Debug.Log($"current Document: {currentDocument.itemName}");
        DocumentImageManager.UpdateImage(currentDocument);
    }
}
