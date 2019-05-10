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

    private bool _mouseDragging;
    private bool _redraw = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _mySR = transform.GetComponent<SpriteRenderer>();
        DrawIngredient();
        _mouseDragging = true;
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
            
        if (_mouseDragging)
        {
            CalcPosOnMouseMove();
        }
    }

    void OnMouseDown()
    {
        if (_mouseDragging)
        {
            _mouseDragging = false;
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
    }
    
    private void DrawIngredient()
    {
        //Check What Ingredient we are and assign our sprite
        switch (_myIngredient)
        {
           
        }

        _mySR.color = _color;
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
}