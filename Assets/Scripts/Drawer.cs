using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drawer : MonoBehaviour
{
    public string _ingredientToSpawn;

    private Text _tooltipText;
    private GameObject _tooltipBox;

    public bool IsBottle = false;
    
    private void Start()
    {
        _tooltipBox = GameObject.FindWithTag("Tooltip");
        _tooltipText = _tooltipBox.transform.GetChild(0).GetComponent<Text>();   
    }

    private void OnMouseDown()
    {
        if(GameManager.Instance.CurrentStoryState != StoryState.Crafting)
            return;
        if (IsBottle)
        {
            AudioManager.Instance.PlayClip("clink");
        }
        else
        {
            AudioManager.Instance.PlayClip("openDrawer");
        }
        GameObject newIngredient = Instantiate(Resources.Load<GameObject>("Prefabs/Ingredient"));
        newIngredient.GetComponent<Ingredient>().IngredientType = _ingredientToSpawn;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        newIngredient.transform.position = Camera.main.ScreenToWorldPoint(curScreenPoint);
        newIngredient.GetComponent<Ingredient>().Dragging = true;
        GameManager.Instance.IngredientsOnBoard.Add(newIngredient.GetInstanceID(), newIngredient);
    }

    private void OnMouseOver()
    {
        if(GameManager.Instance.CurrentStoryState != StoryState.Crafting)
            return;
        //first you need the RectTransform component of your canvas
        RectTransform canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
 
        //then you calculate the position of the UI element
        //0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.
 
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        Vector2 worldObject_ScreenPosition = new Vector2(
            ((viewportPosition.x * canvasRect.sizeDelta.x)-(canvasRect.sizeDelta.x*0.5f)),
            ((viewportPosition.y * canvasRect.sizeDelta.y)-(canvasRect.sizeDelta.y*0.5f)));
 
        //now you can set the position of the ui element
        _tooltipBox.GetComponent<RectTransform>().anchoredPosition = worldObject_ScreenPosition + (Vector2.left * 120);
        _tooltipText.text = Ingredient.FlavorTexts[_ingredientToSpawn];

    }

    private void OnMouseExit()
    {
        _tooltipBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(37, 293);
    }
}
