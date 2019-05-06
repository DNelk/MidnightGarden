using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Recipe : System.Object
{
    public string[] _requiredIngredients;
    public string Name;

    public Recipe(string name, String[] requiredIngredients)
    {
        Name = name;
        _requiredIngredients = requiredIngredients;
    }

    public bool CompareIngredients(IngredientType[] compareTo)
    {
        return RequiredIngredients().OrderBy(s => s).SequenceEqual(compareTo.OrderBy(s => s));
    }

    public List<IngredientType> RequiredIngredients()
    {
        List<IngredientType> ingredientsInEnum = new List<IngredientType>();

        foreach (string ingredient in _requiredIngredients)
        {
            ingredientsInEnum.Add((IngredientType)Enum.Parse(typeof(IngredientType), ingredient, true));
        }

        return ingredientsInEnum;
    }
}
