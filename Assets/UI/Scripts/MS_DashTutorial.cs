using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class MS_DashTutorial : MonoBehaviour
{
    public TMP_Text tutorialText;
    public Image keyboardDash;
    public Sprite keyboardDash_outline;
    public Sprite keyboardDash_solid;
    public Collider2D collisionA;

    private Coroutine swapCoroutine;
    private bool isSwapping = false;

    private void Start()
    {
        tutorialText.gameObject.SetActive(false);
        keyboardDash.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ShowDashTutorial();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        HideTutorial();
    }

    private void ShowDashTutorial()
    {
        tutorialText.gameObject.SetActive(true);

        keyboardDash.sprite = keyboardDash_solid;
        keyboardDash.gameObject.SetActive(true);

        if (!isSwapping)
        {
            swapCoroutine = StartCoroutine(SwapImages());
            isSwapping = true;
        }
    }

    private void HideTutorial()
    {
        tutorialText.gameObject.SetActive(false);
        keyboardDash.gameObject.SetActive(false);
        StopImageSwap();
    }

    private void StopImageSwap()
    {
        if (isSwapping)
        {
            StopCoroutine(swapCoroutine);
            isSwapping = false;
        }
    }

    private IEnumerator SwapImages()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            keyboardDash.sprite = (keyboardDash.sprite == keyboardDash_solid) ? keyboardDash_outline : keyboardDash_solid;
        }
    }
}
