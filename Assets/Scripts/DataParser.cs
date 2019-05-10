using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataParser
{
    public Recipe[] LoadRecipes(string newPath)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, newPath);
        AllRecipes recipes = null;
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);

            recipes = JsonUtility.FromJson<AllRecipes>(dataAsJson);

            Debug.Log("recipe loaded");
        }
        return recipes.Recipes;
    }
}