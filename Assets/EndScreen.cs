using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public Text ContinueText;

    private bool textLoaded = false;
    
    // Start is called before the first frame update
    void Start()
    {
        ContinueText.DOFade(0, 0);
        StartCoroutine(FadeText());
    }

    // Update is called once per frame
    void Update()
    {
        if (textLoaded)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Title");
            }
        }
            
    }

    private IEnumerator FadeText()
    {
        Tween fadeTween = ContinueText.DOFade(1.0f,5f);
        yield return fadeTween.WaitForCompletion();
        textLoaded = true;
    }
}
