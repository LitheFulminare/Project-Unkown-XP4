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

    private CollectableSO _item;

    private Button button;

    // images must be selected in editor
    public Sprite pistolAmmoImg;
    public Sprite pistolImg;
    public Sprite syringeImg;
    public Sprite key1Img;
    public Sprite emptyImg;
    public Sprite bearBustImg;
    public Sprite bullBustImg;
    public Sprite goatBustImg;
    public Sprite horseBustImg;
    public Sprite monkeyBustImg;

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
    public void ChangeIcon(CollectableSO item)
    {
        Debug.Log("Change Icon was called");
        this._item = item;
        iconImg.sprite = item.iconImg;
        // gets what item should be displayed and sets the Image's sprite to its icon
        //switch (item)
        //{
        //    // regular items
        //    case Items.pistolAmmo:
        //        iconImg.sprite = pistolAmmoImg; break;
        //    case Items.pistol:
        //        iconImg.sprite = pistolImg; break;
        //    case Items.syringe:
        //        iconImg.sprite = syringeImg; break;
        //    case Items.keyDoor1:
        //        iconImg.sprite = key1Img; break;

        //    // busts
        //    case Items.bustAGoat:
        //        iconImg.sprite= goatBustImg; break;
        //    case Items.bustBBear:
        //        iconImg.sprite = bearBustImg; break;
        //    case Items.bustCMonkey:
        //        iconImg.sprite = monkeyBustImg; break;
        //    case Items.bustDBull:
        //        iconImg.sprite = bullBustImg; break;
        //    case Items.bustEHorse:
        //        iconImg.sprite = horseBustImg; break;

        //    // empty
        //    case Items.empty:
        //        iconImg.sprite = emptyImg; break;
        //}
    }

    public void Show(bool shouldShow)
    {
        gameObject.SetActive(shouldShow);
    }

    // called by ItemSpawner
    public void ChangeText(int value)
    {
        Debug.Log($"ChangeText was called with parameter {value}");

        // prob will have to rewrite the code

        // checks if the item is not a weapon
        //if (_item.inventoryName != "pistol")
        //{
        //    if (value != 0)
        //    {
        //        text.text = value.ToString();
        //    }
        //    else // won't show anything if 'itemCount[i]' is '0'
        //    {
        //        text.text = "";
        //    }
        //}
        //// if it is, it will instead show the ammo
        //else
        //{
        //    text.text = Pistol.GetLoadedBullets().ToString();
        //}            
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
