using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowIcon : MonoBehaviour
{   
    public delegate void ToggleButton();
    public static ToggleButton toggleButton;

    // text compoenent to display item quantity
    [SerializeField] Text text;

    // the Image component that displays the sprites
    [SerializeField] Image iconImg;

    private Items _item;

    private Button button;

    // images must be selected in editor
    public Sprite pistolAmmoImg;
    public Sprite pistolImg;
    public Sprite syringeImg;
    public Sprite key1Img;
    public Sprite emptyImg;

    private void OnEnable()
    {
        toggleButton += ShowOrHide;
    }

    private void OnDisable()
    {
        toggleButton -= ShowOrHide;
    }

    private void Start()
    {
        button = GetComponent<Button>();
    }

    // called by ItemSpawner
    public void ChangeIcon(Items item)
    {
        this._item = item;
        // gets what item should be displayed and sets the Image's sprite to its icon
        switch (item)
        {
            case Items.pistolAmmo:
                iconImg.sprite = pistolAmmoImg; break;
            case Items.pistol:
                iconImg.sprite = pistolImg; break;
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

    private void ShowOrHide()
    {
        if (!button.interactable)
        {
            button.interactable = true;
            text.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            button.interactable = false;
            text.color = new Color32(60, 0, 0, 255);
        }

        /* 

        I know I could have done something like:
         
        button.interactable = !button.interactable;

        but then I would be able to change the color of the text like I did

        */


    }
}
