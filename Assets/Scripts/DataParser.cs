using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataParser
{
    public string path;
    
    public void LoadData(string newPath)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, newPath);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);

            Recipe recipe = JsonUtility.FromJson<Recipe>(dataAsJson);
            
            Debug.Log("recipe loaded");
        }
    }
}
