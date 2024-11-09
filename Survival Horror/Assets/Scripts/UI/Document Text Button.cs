using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentTextButton : MonoBehaviour
{
    public DocumentSO currentDocument;

    public void ButtonHover()
    {
        DocumentImageManager.UpdateImage(currentDocument);
    }

    public void ButtonPressed()
    {
        // opens the document and starts inpecting
        DocumentTextManager.ButtonPressed(currentDocument);
    }
}
