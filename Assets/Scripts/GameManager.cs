using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public ParticleSystem badParticle;
    public Button ClearButton;
    
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
    private bool textDone = false;
    
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

    public Character[] CharacterData;

    private Dictionary<string, Character> characters;
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
        //DontDestroyOnLoad(gameObject);
        
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

        characters = new Dictionary<string, Character>();
        foreach (Character character in CharacterData)
        {
            characters.Add(character.Name, character);
        }
        
        ClearButton.onClick.AddListener(() => ClearUnusedIngredients());
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

        if (CurrentStoryState == StoryState.Crafting)
            ClearButton.interactable = true;
        else
        {
            ClearButton.interactable = false;
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
            
            CurrentStoryState = StoryState.Reading;

            if(currentCharName == "player" || currentCharName == "")
            {
                textDone = false;
                PrintStory(Color.yellow);
            }
            else if(currentCharName != lastCharName && lastCharName != "player")
            {
                textDone = false;
                //load new char sprite
                StartCoroutine(LoadCharacter());
                currentDesiredRecipe = storyParser.GetVar<string>("desiredRecipe");   
            }
            else
            {
                textDone = false;
                PrintStory(characters[currentCharName].TextColor);
            }
            
        }
        //Do we have a choice
        else if (storyParser.HasChoice())
        {
            if (storyParser.GetVar<string>("whileCraftingText") != "")
            {
                currentText = storyParser.GetVar<string>("whileCraftingText");
                if(currentCharName == "player")
                    PrintStory(Color.yellow);
                else
                    PrintStory(characters[currentCharName].TextColor);
            }
            CurrentStoryState = StoryState.Crafting;
        }
        //The Day is over
        else
        {
            CurrentStoryState = StoryState.DayEnd;
        }
    }

    private void PrintStory(Color textColor)
    {
        StartCoroutine(PrintText(textColor));   
    }
    
    //Update while the player is reading
    private void ReadingStoryUpdate()
    {
        if (textDone && (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))){
            CurrentStoryState = StoryState.Printing;
        }
    }
    
    //Update when the player is crafting the recipe
    private void CraftingUpdate()
    {
       /*// Debug.Log(CurrentDesiredRecipe);
        if(Input.GetKeyDown(KeyCode.Return))
        {
            bool recipeCompleted = container.CheckRecipeCompleted();
        }*/
    }

    public void RecipeServed(string recipeName, GameObject potion)
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

        Destroy(potion);
        CurrentStoryState = StoryState.Printing;
    }
    
    //Coroutine to print text letter-by-letter
    private IEnumerator PrintText(Color textColor)
    {
        for (int i = 0; i < currentText.Length; i++)
        {
            string subStr = currentText.Substring(0, i);
            dialogue.text = subStr;
            dialogue.color = textColor;
            if (currentCharName != "player")
                AudioManager.Instance.PlayClip("blip", characters[currentCharName].VoicePitch, characters[currentCharName].VoiceVolume);
            else
                AudioManager.Instance.PlayClip("blip", 0.7f);
            yield return new WaitForSeconds(TextSpeed);
        }

        textDone = true;
    }

    private IEnumerator LoadCharacter(bool noCharacter = false)
    {
        float tweenTime = 1.0f;
        //Make the old character go away;
        Tween moveCharTween = currentCharSprite.transform.DOMove(SpriteExitPos[Random.Range(0, 2)].position, tweenTime).SetEase(Ease.InOutSine);        
        yield return moveCharTween.WaitForCompletion();
        if (!noCharacter)
        {
            //Make the new character come up;
            currentCharSprite.GetComponent<SpriteRenderer>().sprite = characters[currentCharName].Sprite;
            currentCharSprite.transform.position = SpriteExitPos[Random.Range(0, 2)].position;
            moveCharTween = currentCharSprite.transform.DOMove(SpriteStartingPos.position, tweenTime).SetEase(Ease.InOutSine);
            yield return moveCharTween.WaitForCompletion();
            PrintStory(characters[currentCharName].TextColor);
        }
        else
        {
            currentCharSprite.GetComponent<SpriteRenderer>().sprite = null;
        }
    }

    private IEnumerator StartFade(bool inTrueOutFalse)
    {
        Tween fadeTween;
        float opacity;
        opacity = inTrueOutFalse ? 0.0f : 1.0f;
        fadeTween = fadeOutImg.DOFade(opacity, 1.0f);
        yield return fadeTween.WaitForCompletion();
        if (chapter > 4)
        {
            SceneManager.LoadScene("End");
        }
        else
        {
            if (inTrueOutFalse)
            {
                fadeAnimating = false;
                CurrentStoryState = StoryState.Printing;
            }
            else
            {
                yield return new WaitForSeconds(2);
                CurrentStoryState = StoryState.DayStart;
                fadeAnimating = false;
            }
        }
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
            StartCoroutine(LoadCharacter(true));
            fadeAnimating = true;
            chapter++;
            dialogue.text = "";
            currentCharName = "";
            StartCoroutine(StartFade(false));
            
        }
    }

    private void ClearUnusedIngredients()
    {
        foreach (var ingredient in IngredientsOnBoard)
        {
            Destroy(ingredient.Value);
        }
        
        IngredientsOnBoard.Clear();
        AudioManager.Instance.PlayClip("sweep");
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

[Serializable]
public struct Character
{
    public string Name;
    public Sprite Sprite;
    public Color TextColor;
    public float VoicePitch;
    public float VoiceVolume;
}