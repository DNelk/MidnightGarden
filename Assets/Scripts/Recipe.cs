using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Recipe : System.Object
{
    public IngredientType[] RequiredIngredients;
    public string Name;

    public Recipe(string name, IngredientType[] requiredIngredients)
    {
        Name = name;
        RequiredIngredients = requiredIngredients;
    }

    public bool CompareIngredients(IngredientType[] compareTo)
    {
        return RequiredIngredients.OrderBy(s => s).SequenceEqual(compareTo.OrderBy(s => s));
    }
}
