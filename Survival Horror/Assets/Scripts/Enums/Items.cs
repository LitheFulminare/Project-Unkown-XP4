// this enum stores the items
// you need to assign int values to each item, it's ok if they're completely out of order
// check this thread for more info:
// https://discussions.unity.com/t/re-order-or-delete-enum-entries-will-it-causes-problems/910062

// PLEASE UPDATE THE NUMBER BELOW
// last item: 11

using UnityEngine;

public enum Items
{
    // empty space on the inventory
    empty = 0,

    //ammo
    pistolAmmo = 1,

    // consumable
    syringe = 2,

    // weapon
    pistol = 3,

   // keys
   keyDoor1 = 4,
   breakRoomKey = 5,
   hallKey = 6,

   // puzzle specific items
   bustAGoat= 7,
   bustBBear = 8,
   bustCMonkey = 9,
   bustDBull = 10,
   bustEHorse = 11,
}
