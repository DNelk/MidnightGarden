using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    
    //UI
    private Canvas uiCanvas;
    private Image dialogueBox;
    private Text dialogue;
    private string currentText;
    [Header("Text Speed")]
    public float TextSpeed = 0.1f;
    
    //Parser
    private StoryParser storyParser;
    public StoryState CurrentStoryState;
    
    //Game Vars
    public string CurrentCharName;
    public string CurrentDesiredRecipe;
    
    // Start is called before the first frame update
    private void Awake()
    {
        //Check if instance already exists
        if (instance == null)             
            //if not, set instance to this
            instance = this;
            
        //If instance already exists and it's not this:
        else if (instance != this)   
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
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

        TextAsset storyFile = Resources.Load<TextAsset>("Ink/test");
        storyParser = new StoryParser(storyFile);
        CurrentStoryState = StoryState.Printing;
    }
    
    // Update is called once per frame
    private void Update()
    {
        switch (CurrentStoryState)
        {
            case StoryState.Printing:
                PrintingStoryUpdate();
                break;
            case StoryState.Reading:
                ReadingStoryUpdate();
                break;
            case StoryState.Crafting:
                CraftingUpdate();
                break;
        }
    }
    
    //Update for printing text
    private void PrintingStoryUpdate()
    {
        //string lastCharName = CurrentCharName;
        //CurrentCharName = storyParser.GetVar("Name");
        //if(CurrentCharName != lastCharName)
        //{
            //load new char sprite
            CurrentDesiredRecipe = storyParser.GetVar<string>("desiredRecipe");
            
        //}
        if (storyParser.CanContinue())
        {
            currentText = storyParser.NextLine();
            Debug.Log(currentText);
            
            StartCoroutine(PrintText());   
            if(storyParser.CanContinue())
                CurrentStoryState = StoryState.Reading;
        }
        //Oh now we have a choice
        if (storyParser.HasChoice())
        {
            CurrentStoryState = StoryState.Crafting;
        }
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
    }

    public void RecipeCreated(string recipeName)
    {
        if(CurrentStoryState != StoryState.Crafting)
            return;
        
        int choiceIndex;
        if (recipeName == CurrentDesiredRecipe)
        {
            choiceIndex = 0;
        }
        else
        {
            choiceIndex = 1;
        }
        
        storyParser.MakeChoice(choiceIndex);
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
}

public enum StoryState
{
    Printing,
    Reading,
    Crafting
}