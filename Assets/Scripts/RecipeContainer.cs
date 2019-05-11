using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RecipeContainer : MonoBehaviour
{
    private List<string> _contents;

    private Color _myColor;
    private List<Color> _colors;
    private SpriteRenderer _mySR;

    public List<Recipe> Recipes;

    private Text _uiText;

    public Animator PestleAnimator;
    // Start is called before the first frame update
    void Start()
    {
        _contents = new List<string>();
        _mySR = transform.GetComponent<SpriteRenderer>();
        _myColor = Color.white;
        _mySR.color = _myColor;
        _colors = new List<Color>();

        _uiText = GameObject.FindWithTag("UIText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _contents.Clear();
            _colors.Clear();
            _mySR.color = Color.white;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {      
        Ingredient currIngredient = other.gameObject.transform.GetComponent<Ingredient>();
        if (currIngredient != null)
        {
            AddIngredient(currIngredient);
            GameManager.Instance.IngredientsOnBoard.Remove(other.gameObject.GetInstanceID());
            GameObject.Destroy(other.gameObject);
        }
    }
    
    //Add ingredient to list, change color
    private void AddIngredient(Ingredient currIngredient)
    {
        _contents.Add(currIngredient.IngredientType);
        //AddColor(currIngredient.Color);    
    }

    private void AddColor(Color newColor)
    {
        if (_colors.Count == 0)
        {
            _myColor = newColor;
        }
        else
        {
            Color result = new Color(0,0,0,0);
            foreach(Color c in _colors)
            {
                result += c;
            }
            result /= _colors.Count;
            _myColor = result;
        }
        
        _colors.Add(newColor);
        _mySR.color = _myColor;
    }

    public bool CheckRecipeCompleted()
    {
        foreach (Recipe r in Recipes)
        {
            if (r.CompareIngredients(_contents.ToArray()))
            {
                GameManager.Instance.RecipeCreated(r.Name);
                return true;
            }
        }

        return false;
    }

    public void EmptyContainer()
    {
        _contents.Clear();
    }

    private void OnMouseDown()
    {
        if(GameManager.Instance.CurrentStoryState == StoryState.Crafting)
        {
            bool recipeFound = false;
            foreach (Recipe r in Recipes)
            {
                if (r.CompareIngredients(_contents.ToArray()))
                {
                    GameManager.Instance.RecipeCreated(r.Name);
                    recipeFound = true;
                }
            }

            if (!recipeFound)
            {
                GameManager.Instance.RecipeCreated("nothing");
            }
            
            PestleAnimator.SetTrigger("StartGrind");
        }
    }
}
