using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayIntro : MonoBehaviour
{
    // Start is called before the first frame update
    public Text[] titleInfo;
    private bool ready = false;
    void Start()
    {
        foreach (Text t in titleInfo)
        {
            t.DOFade(0, 0);
        }
        StartCoroutine(FadeInText());
    }

    private IEnumerator FadeInText()
    {
        yield return new WaitForSeconds(2f);
        Tween fadeTween = titleInfo[0].DOFade(1, 1f);
        yield return fadeTween.WaitForCompletion();
        fadeTween = titleInfo[1].DOFade(1, 1f);
        yield return fadeTween.WaitForCompletion();
        ready = true;
    }

    private void Update()
    {
        if (Input.anyKeyDown && ready)
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
