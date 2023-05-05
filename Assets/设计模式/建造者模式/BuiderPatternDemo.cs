using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuiderPatternDemo : MonoBehaviour
{
    private void Awake()
    {
        MealBuilder mealBuilder = new MealBuilder();

        Meal vegMeal = mealBuilder.prepareVegMeal();
        Debug.Log(("Veg Meal"));
        vegMeal.showItems();
        Debug.Log(("Total Cost: " + vegMeal.getCost()));

        Meal nonVegMeal = mealBuilder.prepareNonVegMeal();
        Debug.Log("\n\nNon-Veg Meal");
        nonVegMeal.showItems();
        Debug.Log("Total Cost: " + nonVegMeal.getCost());
    }
}
