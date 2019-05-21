using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RecipeContainer : MonoBehaviour
{
    private List<string> _contents;
    private List<GameObject> _activePotions;
    
    private Color _myColor;
    private List<Color> _colors;
    private SpriteRenderer _mySR;

    public List<Recipe> Recipes;

    private Text _uiText;

    public Animator PestleAnimator;
    
    private Text _tooltipText;
    private GameObject _tooltipBox;

    public Button EmptyButton;
    // Start is called before the first frame update
    void Start()
    {
        _contents = new List<string>();
        _activePotions = new List<GameObject>();
        _mySR = transform.GetComponent<SpriteRenderer>();
        _myColor = Color.white;
        _mySR.color = _myColor;
        _colors = new List<Color>();

        _uiText = GameObject.FindWithTag("UIText").GetComponent<Text>();
        
        _tooltipBox = GameObject.FindWithTag("Tooltip");
        _tooltipText = _tooltipBox.transform.GetChild(0).GetComponent<Text>();

        EmptyButton.interactable = false; 
        EmptyButton.onClick.AddListener(() => EmptyWithEffect());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            EmptyWithEffect();
        }

        if (GameManager.Instance.CurrentStoryState == StoryState.Crafting)
            EmptyButton.interactable = true;
        else
        {
            EmptyButton.interactable = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {      
        Ingredient currIngredient = other.gameObject.transform.GetComponent<Ingredient>();
        if (currIngredient != null)
        {
            AudioManager.Instance.PlayClip("drop");
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
                //GameManager.Instance.RecipeCreated(r.Name);
                return true;
            }
        }

        return false;
    }

    public void EmptyContainer()
    {
        _contents.Clear();
    }

    public void EmptyWithEffect()
    {
        _contents.Clear();
        foreach (GameObject potion in _activePotions)
        {
            Destroy(potion);
        }
        _activePotions.Clear();
        _colors.Clear();
        _mySR.color = Color.white;
        AudioManager.Instance.PlayClip("sweep");
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
                    StartCoroutine(SpawnPotion(r));
                    EmptyContainer();
                    recipeFound = true;
                }
            }

            if (!recipeFound)
            {
                if (_contents.Count != 0)
                {
                    AudioManager.Instance.PlayClip("gurgle");
                    GameManager.Instance.badParticle.Emit(100);
                }
                EmptyContainer();
                //Didn't make anytging
            }
            
            PestleAnimator.SetTrigger("StartGrind");
            AudioManager.Instance.PlayClip("grind");
        }
    }

    private IEnumerator SpawnPotion(Recipe r)
    {
        yield return new WaitForSeconds(1);
        AudioManager.Instance.PlayClip("pour");
        GameObject potion = Instantiate(Resources.Load<GameObject>("Prefabs/Potion"));
        potion.transform.position = transform.position + Vector3.left * 3;
        potion.GetComponent<Potion>().Recipe = r.Name;  
    }
    
    private void OnMouseOver()
    {
        if(GameManager.Instance.CurrentStoryState != StoryState.Crafting || _contents.Count == 0)
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
        _tooltipBox.GetComponent<RectTransform>().anchoredPosition = worldObject_ScreenPosition + (Vector2.left * 150);
        _tooltipText.text = "Contents: ";
        foreach (string i in _contents)
        {
            _tooltipText.text += Environment.NewLine;
            _tooltipText.text += i;
        }

    }

    private void OnMouseExit()
    {
        _tooltipBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(37, 293);
    }
}
