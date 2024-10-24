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

    private float spawnTime;

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
    }

    private void Update()
    {
        if (Input.anyKey && Time.time > spawnTime + 1.5f)
        {
            ChangeImage();
        }
    }

    private void ChangeImage()
    {
        documentImage.color = Color.gray;
    }
}
