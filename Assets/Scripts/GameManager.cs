using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    
    //UI
    private Canvas uiCanvas;
    private Image dialogueBox;
    private Text dialogue;
    private string currentText;
    
    [Header("Text Speed")]
    public float TextSpeed = 0.1f;

    //Fade
    private bool fadeAnimating;
    private Image fadeOutImg;
    
    //Parser
    private StoryParser storyParser;
    private DataParser jsonParser;
    [Header("Story Information")]
    [SerializeField]
    public StoryState CurrentStoryState;
    [SerializeField] private int chapter;
    
    //Game Vars
    [Header("Character Information")]
    [SerializeField]
    private string currentCharName;
    [SerializeField]
    private string currentDesiredRecipe;
    [SerializeField]
    private GameObject currentCharSprite;
    private RecipeBook recipeBook;

    
    [Header("Sprite Positions")]
    public Transform SpriteStartingPos;
    public Transform[] SpriteExitPos = new Transform[2];
    
    private RecipeContainer container;

    public Dictionary<int, GameObject> IngredientsOnBoard;
    
    
    // Start is called before the first frame update
    private void Awake()
    {
        //Check if Instance already exists
        if (Instance == null)             
            //if not, set Instance to this
            Instance = this;
            
        //If Instance already exists and it's not this:
        else if (Instance != this)   
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one Instance of a GameManager.
            Destroy(gameObject);    
            
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        
        Init();
    }

    //Initialize everything
    private void Init()
    {
        uiCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        dialogueBox = GameObject.FindWithTag("DialogueBox").GetComponent<Image>();
        dialogue = dialogueBox.transform.GetChild(0).GetComponent<Text>();

        // dialogueBox.gameObject.SetActive(false);
        chapter = 1;
        
        fadeAnimating = false;
        fadeOutImg = GameObject.Find("FadeOutImage").GetComponent<Image>();
        fadeOutImg.color = Color.black;
        CurrentStoryState = StoryState.DayStart;

        container = GameObject.FindWithTag("RecipeContainer").GetComponent<RecipeContainer>();
        currentCharSprite = GameObject.FindWithTag("CurrentChar");
        recipeBook = GameObject.FindWithTag("RecipeBook").GetComponent<RecipeBook>();

        IngredientsOnBoard = new Dictionary<int, GameObject>();
        
        jsonParser = new DataParser();
        Recipe[] recipes = jsonParser.LoadRecipes("recipes.json");
        container.Recipes = recipes.OfType<Recipe>().ToList();
        recipeBook.Recipes = recipes;
    }
    
    // Update is called once per frame
    private void Update()
    {
        switch (CurrentStoryState)
        {
            case StoryState.DayStart:
                DayStart();
                break;
            case StoryState.Printing:
                PrintingStoryUpdate();
                break;
            case StoryState.Reading:
                ReadingStoryUpdate();
                break;
            case StoryState.Crafting:
                CraftingUpdate();
                if(Input.GetKeyDown(KeyCode.Space))
                    ClearUnusedIngredients();
                break;
            case StoryState.DayEnd:
                DayEnd();
                break;
        }
    }
    
    //Update for printing text
    private void PrintingStoryUpdate()
    {
        if (storyParser.CanContinue())
        {
            currentText = storyParser.NextLine();
            Debug.Log(currentText);
            
            string lastCharName = currentCharName;
            currentCharName = storyParser.GetVar<string>("name");
            
            if(storyParser.CanContinue())
                CurrentStoryState = StoryState.Reading;
            
            if(currentCharName != lastCharName)
            {
                //load new char sprite
                StartCoroutine(LoadCharacter());
                currentDesiredRecipe = storyParser.GetVar<string>("desiredRecipe");   
            }
            else
            {
                PrintStory();
            }
            
        }
        //Do we have a choice
        else if (storyParser.HasChoice())
        {
            CurrentStoryState = StoryState.Crafting;
        }
        //The Day is over
        else
        {
            CurrentStoryState = StoryState.DayEnd;
        }
    }

    private void PrintStory()
    {
        StartCoroutine(PrintText());   
    }
    
    //Update while the player is reading
    private void ReadingStoryUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CurrentStoryState = StoryState.Printing;
        }
    }
    
    //Update when the player is crafting the recipe
    private void CraftingUpdate()
    {
       // Debug.Log(CurrentDesiredRecipe);
        if(Input.GetKeyDown(KeyCode.Return))
        {
            bool recipeCompleted = container.CheckRecipeCompleted();
        }
    }

    public void RecipeCreated(string recipeName)
    {
        if(CurrentStoryState != StoryState.Crafting)
            return;
        
        int choiceIndex;
        if (recipeName == currentDesiredRecipe)
        {
            choiceIndex = 0;
        }
        else
        {
            choiceIndex = 1;
        }
        
        storyParser.MakeChoice(choiceIndex);
        container.EmptyContainer();
        CurrentStoryState = StoryState.Printing;
    }
    
    //Coroutine to print text letter-by-letter
    private IEnumerator PrintText()
    {
        for (int i = 0; i < currentText.Length; i++)
        {
            string subStr = currentText.Substring(0, i);
            dialogue.text = subStr;
            yield return new WaitForSeconds(TextSpeed);
        }
    }

    private IEnumerator LoadCharacter()
    {
        float tweenTime = 1.0f;
        //Make the old character go away;
        Tween moveCharTween = currentCharSprite.transform.DOMove(SpriteExitPos[Random.Range(0, 2)].position, tweenTime);        
        yield return moveCharTween.WaitForCompletion();
        //Make the new character come up;
        currentCharSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/" + currentCharName);
        currentCharSprite.transform.position = SpriteExitPos[Random.Range(0, 2)].position;
        moveCharTween = currentCharSprite.transform.DOMove(SpriteStartingPos.position, tweenTime);
        yield return moveCharTween.WaitForCompletion();
        PrintStory();
    }

    private IEnumerator StartFade(bool inTrueOutFalse)
    {
        Tween fadeTween;
        float opacity;
        opacity = inTrueOutFalse ? 0.0f : 1.0f;
        fadeTween = fadeOutImg.DOFade(opacity, 1.0f);
        CurrentStoryState = inTrueOutFalse ? StoryState.Printing : StoryState.DayStart;
        yield return fadeTween.WaitForCompletion();
        fadeAnimating = false;
    }

    private void DayStart()
    {
        if (!fadeAnimating)
        {
            TextAsset storyFile = Resources.Load<TextAsset>("Ink/day" + chapter);
            storyParser = new StoryParser(storyFile);
            fadeAnimating = true;
            StartCoroutine(StartFade(true));
        }
    }
    
    private void DayEnd()
    {
        if (!fadeAnimating)
        {
            fadeAnimating = true;
            StartCoroutine(StartFade(false));
            chapter++;
        }
    }

    private void ClearUnusedIngredients()
    {
        foreach (var ingredient in IngredientsOnBoard)
        {
            Destroy(ingredient.Value);
        }
        
        IngredientsOnBoard.Clear();
    }
}

public enum StoryState
{
    DayStart,
    Printing,
    Reading,
    Crafting,
    DayEnd,
    ReadingRecipe
}