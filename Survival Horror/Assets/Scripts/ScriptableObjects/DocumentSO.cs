using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Generic Document", menuName = "DocumentSO/Document")]
public class DocumentSO : ScriptableObject
{
    public string itemName; // the name of the item shown in-game

    public string[] description; // can store pages of description

    public Image backgroundImage; // image shown in the background while the player is reading the content

    public DocumentType type; // whether it's a map, a tour guide, a staff's record, etc
}
