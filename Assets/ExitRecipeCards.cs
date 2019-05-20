using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 

public class ExitRecipeCards : MonoBehaviour, IPointerDownHandler
{
    private RecipeBook book;

    private void Start()
    {
        book = GameObject.FindWithTag("RecipeBook").GetComponent<RecipeBook>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameManager.Instance.CurrentStoryState == StoryState.ReadingRecipe)
        {
            StartCoroutine(book.ToggleUI());
        }
    }
}
