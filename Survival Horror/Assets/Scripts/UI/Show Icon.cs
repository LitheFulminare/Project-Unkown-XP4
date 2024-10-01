using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowIcon : MonoBehaviour
{   
    // text compoenent to display item quantity
    [SerializeField] public Text text;

    // the Image component that displays the sprites
    [SerializeField] public Image iconImg;

    private Items _item;

    // images must be selected in editor
    public Sprite pistolAmmoImg;
    public Sprite syringeImg;
    public Sprite key1Img;
    public Sprite emptyImg;

    // called by ItemSpawner
    public void ChangeIcon(Items item)
    {
        this._item = item;
        // gets what item should be displayed and sets the Image's sprite to its icon
        switch (item)
        {
            case Items.pistolAmmo:
                iconImg.sprite = pistolAmmoImg; break;
            case Items.syringe:
                iconImg.sprite = syringeImg; break;
            case Items.keyDoor1:
                iconImg.sprite = key1Img; break;
            case Items.empty:
                iconImg.sprite = emptyImg; break;
        }
    }

    public void Show(bool shouldShow)
    {
        gameObject.SetActive(shouldShow);
    }

    // called by ItemSpawner
    public void ChangeText(int qtd)
    {
        // first checks if the item is not a weapon
        if (_item != Items.pistol)
        {
            if (qtd != 0)
            {
                text.text = qtd.ToString();
            }
            else // won't show anything if 'itemCount[i]' is '0'
            {
                text.text = "";
            }
        }
        // if it is, it will instead show the ammo
        else
        {
            text.text = Pistol.GetLoadedBullets().ToString();
        }
            
    }

    // called when the icon is pressed by 'Button' component
    public void Use()
    {
        InventoryController.useItem(_item);
        //Debug.Log($"Item type: {_item}");
    }
}
