using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    //What ingredient are we?
    public string _recipe;
    
    //Our Components
    private SpriteRenderer _mySR;
    
    //Drag Stuff
    private Vector3 _screenPoint;

    private Vector3 _offset;
    
    private Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();
    public List<SpriteWithKey> SpritesWithKeys;

    private bool _redraw = true;

    private void Awake()
    {
        foreach (SpriteWithKey s in SpritesWithKeys)
        {
            _sprites.Add(s.Key, s.Sprite);
        }

        _mySR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_redraw)
        {
            _redraw = false;
            DrawRecipe();
        }
    }

    void OnMouseDown()
    {
        CalcOffset();
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
    
    private void DrawRecipe()
    {
        if(_recipe != "0")
            _mySR.sprite = _sprites[_recipe];
    }

    public string Recipe
    {
        get { return _recipe; }
        set { _recipe = value; _redraw = true; }
    }

}
