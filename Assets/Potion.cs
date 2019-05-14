using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    //What ingredient are we?
    public string RecipeName;
    
    //Our Components
    private SpriteRenderer _mySR;
    
    //Drag Stuff
    private Vector3 _screenPoint;

    private Vector3 _offset;
    
    private static Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();
    public static Dictionary<string, string> FlavorTexts = new Dictionary<string, string>();
    public List<SpriteWithKey> SpritesWithKeys;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
