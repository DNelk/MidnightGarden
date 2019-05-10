using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Recipe : System.Object
{
    public string Name;
    public List<string> Requiredingredients;
    
    public Recipe(string name, List<string> requiredIngredients)
    {
        Name = name;
        Requiredingredients = requiredIngredients;
    }

    public bool CompareIngredients(string[] compareTo)
    {
        return Requiredingredients.OrderBy(s => s).SequenceEqual(compareTo.OrderBy(s => s));
    }
}

public class AllRecipes : System.Object
{
    public Recipe[] Recipes;

    public AllRecipes(Recipe[] allRecipes)
    {
        Recipes = allRecipes;
    }
}
