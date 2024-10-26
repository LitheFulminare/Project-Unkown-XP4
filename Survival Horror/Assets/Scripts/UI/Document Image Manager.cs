using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocumentImageManager : MonoBehaviour
{
    public delegate void OnHover(DocumentSO documentSo);
    public static OnHover onHover;

    [SerializeField] Image documentImage;

    private void OnEnable()
    {
        onHover += ChangeImage;
    }

    private void OnDisable()
    {
        onHover -= ChangeImage;
    }

    public void ChangeImage(DocumentSO documentSO)
    {
        documentImage.sprite = documentSO.inspectImage;
    }
}
