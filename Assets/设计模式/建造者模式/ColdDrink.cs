using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdDrink : MonoBehaviour, Item
{
    public Packing packing()
    {
        return new Bottle();
    }

    public float Price()
    {
        throw new System.NotImplementedException();
    }

    string Item.name()
    {
        throw new System.NotImplementedException();
    }
}
