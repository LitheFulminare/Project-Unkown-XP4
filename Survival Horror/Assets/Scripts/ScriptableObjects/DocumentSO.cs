using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Generic Document", menuName = "ScriptableObject/Document")]
public class DocumentSO : ScriptableObject
{
    public string itemName; // name shown when inspcting and on the inventory

    public bool isCollectable; // whether the document will go to the player's inventory or not

    public bool isFullscreen;

    public string[] paragraphs; // paragraphs of text

    public bool skipLines; // skip lines after each paragraph

    public bool centralizeText;

    public Sprite backgroundImage; // image shown in the background while the player is reading the content

    public Sprite backgroundImageAlt; // alternate version. Ex.: when the player uses the thermal goggles on the 'blank' paper they see another image

    public Sprite inspectImage; // dark version of the backgroundImage when info is on top of the img, like text or something


    // prob wont be used
    public DocumentType type; // whether it's a map, a tour guide or a record of some sort (the enum might be updated so new types will be added)
}
