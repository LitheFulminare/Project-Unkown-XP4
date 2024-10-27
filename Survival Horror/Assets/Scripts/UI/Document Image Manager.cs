using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocumentImageManager : MonoBehaviour
{
    public delegate void OnHover(DocumentSO documentSo);
    public static OnHover UpdateImage;

    public delegate void ImageState();
    public static ImageState HideImage;
    public static ImageState ShowImage;

    [SerializeField] Image documentImage;

    private void OnEnable()
    {
        UpdateImage += ChangeImage;
        HideImage += Hide;
    }

    private void OnDisable()
    {
        UpdateImage -= ChangeImage;
        HideImage -= Hide;
    }

    public void ChangeImage(DocumentSO documentSO)
    {
        Debug.Log($"Parameter: {documentSO}");
        documentImage.color = Color.white;
        documentImage.sprite = documentSO.backgroundImage;
    }

    private void Hide()
    {
        documentImage.color = Color.clear;
    }
}
