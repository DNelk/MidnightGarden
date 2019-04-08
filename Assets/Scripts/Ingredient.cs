using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    //What ingredient are we?
    public IngredientType _myIngredient;
    
    //Our Components
    private SpriteRenderer _mySR;
    private Color _color;
    
    //Drag Stuff
    private Vector3 _screenPoint;

    private Vector3 _offset;
    // Start is called before the first frame update
    void Start()
    {
        _mySR = transform.GetComponent<SpriteRenderer>();
        DrawIngredient();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        _screenPoint = Camera.main.WorldToScreenPoint(transform.position);


        _offset = transform.position - Camera.main.ScreenToWorldPoint(
                     new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));

    }


    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);


        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + _offset;
        transform.position = curPosition;

    }
    private void DrawIngredient()
    {
        //Check What Ingredient we are and assign our sprite
        switch (_myIngredient)
        {
           case IngredientType.Wheat:
               _color = Color.yellow;
               break;
            case IngredientType.Slime:
                _color = Color.magenta;
                break;
            case IngredientType.Kale:
                _color = Color.green;
                break;
        }

        _mySR.color = _color;
    }

    public IngredientType GetIngredientType()
    {
        return _myIngredient;
    }
    
    public Color Color
    {
        get { return _color; }
        set { _color = value; }
    }
}

public enum IngredientType
{
    Wheat,
    Slime,
    Kale
}
