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
    

    // images must be selected in editor
    public Sprite pistolAmmoImg;
    public Sprite syringeImg;
    public Sprite emptyImg;

    // called by ItemSpawner
    public void ChangeIcon(Items item)
    {
        // gets what item should be displayed and sets the Image's sprite to its icon
        switch (item)
        {
            case Items.pistolAmmo:
                iconImg.sprite = pistolAmmoImg; break;

            case Items.syringe:
                iconImg.sprite = syringeImg; break;
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
        text.text = qtd.ToString();
    }
}
