using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    public string _ingredientToSpawn;

    private void OnMouseDown()
    {
        GameObject newIngredient = Instantiate(Resources.Load<GameObject>("Prefabs/Ingredient"));
        newIngredient.GetComponent<Ingredient>().IngredientType = _ingredientToSpawn;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        newIngredient.transform.position = Camera.main.ScreenToWorldPoint(curScreenPoint);
        
        GameManager.Instance.IngredientsOnBoard.Add(newIngredient.GetInstanceID(), newIngredient);
    }
}
