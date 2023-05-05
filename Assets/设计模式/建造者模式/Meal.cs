using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class Meal 
{
    private List<Item> items = new List<Item>();

    public void addItem(Item item)
    {
        items.Add(item);
    }

    public float getCost()
    {
        float cost = 0.0f;
        foreach (Item item in items)
        {
            cost += item.Price();
        }
        return cost;
    }

    public void showItems()
    {
        foreach (Item item in items)
        {
            Debug.Log(("Item :      " + item.name()));
            Debug.Log((", Packing : " + item.packing().pack()));
            Debug.Log((", Price :   " + item.Price()));
        }
    }
}
