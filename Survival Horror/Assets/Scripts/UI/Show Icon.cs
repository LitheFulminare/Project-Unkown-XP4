using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowIcon : MonoBehaviour
{
    // the Image component that displays the sprites
    [SerializeField] public Image iconImg;

    // images must be selected in editor
    public Sprite pistolAmmoImg;
    public Sprite syringeImg;  

    // called by ItemSpawner
    public void ChangeIcon(Items item) // should also receive how many of these items
    {
        // gets what item should be displayed and sets the Image's sprite to its icon
        switch (item)
        {
            case Items.pistolAmmo:
                iconImg.sprite = pistolAmmoImg; break;

            case Items.syringe:
                iconImg.sprite = syringeImg; break;
        }
    }
}
