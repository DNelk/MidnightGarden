using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Recipe : System.Object
{
    public string Name;
    public List<string> RequiredIngredients;
    public string Flavor;
    
    public Recipe(string name, List<string> requiredIngredients, string flavor)
    {
        Name = name;
        RequiredIngredients = requiredIngredients;
        Flavor = flavor;
    }

    public bool CompareIngredients(string[] compareTo)
    {
        return RequiredIngredients.OrderBy(s => s).SequenceEqual(compareTo.OrderBy(s => s), StringComparer.OrdinalIgnoreCase);
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
