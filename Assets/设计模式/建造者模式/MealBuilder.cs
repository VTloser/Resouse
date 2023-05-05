using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MealBuilder
{
    public Meal prepareVegMeal()
    {
        Meal meal = new Meal();
        meal.addItem(new VegBurger());
        meal.addItem(new Coke());
        return meal;
    }

    public Meal prepareNonVegMeal()
    {
        Meal meal = new Meal();
        meal.addItem(new ChickenBurger());
        meal.addItem(new Pepsi());
        return meal;
    }
}
