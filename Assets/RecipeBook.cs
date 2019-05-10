using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RecipeBook : MonoBehaviour
{
    public Image FadeImage;
    public Text[] RecipeContents;
    public GameObject RecipeUI;
    public Button[] Arrows;

    private float _initialY = -206.785f;
    private float _endY = 552.8f;

    private bool _UIActive;

    private int _currentRecipeIndex;
    private Recipe[] recipes;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Button arrow in Arrows)
        {
            arrow.GetComponent<Image>().DOFade(0f, 0f);
        }

        _UIActive = false;
        _currentRecipeIndex = 0;
        Arrows[0].onClick.AddListener(() => ChangePage(false));
        Arrows[1].onClick.AddListener(() => ChangePage(true));
    }

    // Update is called once per frame
    void Update()
    {
        if (_UIActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                StartCoroutine(ToggleUI());
        }
    }

    private void OnMouseDown()
    {
        if(GameManager.Instance.CurrentStoryState == StoryState.Crafting)
            StartCoroutine(ToggleUI());
    }

    private IEnumerator ToggleUI()
    {
        if (_UIActive)
        {
            foreach (Button arrow in Arrows)
            {
                arrow.GetComponent<Image>().DOFade(0f, 0f);
            }
            RecipeUI.transform.DOMoveY(_initialY, 1f);
            Tween fadeTween = FadeImage.DOFade(0f, 1f);
            yield return fadeTween.WaitForCompletion();
            _UIActive = false;
            GameManager.Instance.CurrentStoryState = StoryState.Crafting;
        }
        else
        {
            RecipeUI.transform.DOMoveY(_endY, 1f);
            Tween fadeTween = FadeImage.DOFade(.7f, 1f);
            yield return fadeTween.WaitForCompletion();
            foreach (Button arrow in Arrows)
            {
                arrow.GetComponent<Image>().DOFade(1f, 0f);
            }
            _UIActive = true;
            GameManager.Instance.CurrentStoryState = StoryState.ReadingRecipe;
        }
    }

    public Recipe[] Recipes
    {
        get { return recipes; }
        set { recipes = value;
            SetCurrentRecipe();
        }
    }
    
    public void ChangePage(bool trueRightFalseLeft)
    {
        if (trueRightFalseLeft && _currentRecipeIndex != recipes.Length - 1)
        {
            _currentRecipeIndex++;
        }
        else if (!trueRightFalseLeft && _currentRecipeIndex != 0)
        {
            _currentRecipeIndex--;
        }
        else
        {
            return;
        }
        
        SetCurrentRecipe();  
    }

    private void SetCurrentRecipe()
    {
        Recipe currentRecipe = recipes[_currentRecipeIndex];
        RecipeContents[0].text = currentRecipe.Name;
        RecipeContents[1].text = "";
        foreach (string ingredient in currentRecipe.Requiredingredients)
        {
            RecipeContents[1].text += ingredient + Environment.NewLine;
        }

        RecipeContents[1].text += "Mix until done!";
    }
}
