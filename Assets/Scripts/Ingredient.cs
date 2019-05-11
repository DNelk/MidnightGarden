using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    //What ingredient are we?
    public string _myIngredient;
    
    //Our Components
    private SpriteRenderer _mySR;
    private Color _color;
    
    //Drag Stuff
    private Vector3 _screenPoint;

    private Vector3 _offset;

    public bool Dragging = false;
    private bool _redraw = false;

    private static Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();
    public static Dictionary<string, string> FlavorTexts = new Dictionary<string, string>();
    public List<SpriteWithKey> SpritesWithKeys;
    
    // Start is called before the first frame update
    void Start()
    {
        _mySR = transform.GetComponent<SpriteRenderer>();
        foreach (SpriteWithKey s in SpritesWithKeys)
        {
            if (!_sprites.ContainsKey(s.Key))
            {
                _sprites.Add(s.Key, s.Sprite);
                FlavorTexts.Add(s.Key, s.Flavor);
            }
        }
        DrawIngredient();
        CalcOffset();
    }

    // Update is called once per frame
    void Update()
    {
        if (_redraw)
        {
            _redraw = false;
            DrawIngredient();
        }
            
        if (Dragging)
        {
            CalcPosOnMouseMove();
        }
    }

    void OnMouseDown()
    {
        if (Dragging)
        {
            Dragging = false;
        }
        else
        {
            CalcOffset();
        }
    }


    void OnMouseDrag()
    {
        CalcPosOnMouseMove();
    }

    private void CalcOffset()
    {
        _screenPoint = Camera.main.WorldToScreenPoint(transform.position);


        _offset = transform.position - Camera.main.ScreenToWorldPoint(
                      new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
    }

    private void CalcPosOnMouseMove()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);


        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + _offset;
        transform.position = curPosition;
        
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 3f, transform.forward);
        if (hit)
        {
            if (hit.transform.CompareTag("RecipeContainer"))
            {
                Vector3 targetDir = hit.transform.position - transform.position;

                // The step size is equal to speed times frame time.
                float step = 0.5f * Time.deltaTime;

                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
                Debug.DrawRay(transform.position, newDir, Color.red);

                // Move our position a step closer to the target.
                transform.rotation = Quaternion.LookRotation(newDir);
            }
        }
    }
    
    private void DrawIngredient()
    {
        if(_myIngredient != "0")
            _mySR.sprite = _sprites[_myIngredient];
    }

    public string IngredientType
    {
        get { return _myIngredient; }
        set { _myIngredient = value; _redraw = true; }
    }

    public Color Color
    {
        get { return _color; }
        set { _color = value; }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 3f);
    }
}

[Serializable]
public struct SpriteWithKey
{
    public string Key;
    public Sprite Sprite;
    public string Flavor;
}