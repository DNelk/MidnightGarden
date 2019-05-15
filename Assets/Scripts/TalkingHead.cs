using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingHead : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Potion"))
        {
            GameManager.Instance.RecipeServed(other.GetComponent<Potion>().Recipe, other.gameObject);
        }
    }
}
