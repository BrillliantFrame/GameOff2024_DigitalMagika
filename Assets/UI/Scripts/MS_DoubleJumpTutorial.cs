using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class MS_DoubleJumpTutorial : MonoBehaviour
{
    public TMP_Text tutorialText;
    public Image keyboardSpace;
    public Sprite keyboardSpace_outline;
    public Sprite keyboardSpace_solid;
    public Collider2D collisionA;

    private Coroutine swapCoroutine;
    private bool isSwapping = false;

    private void Start()
    {
        tutorialText.gameObject.SetActive(false);
        keyboardSpace.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ShowJumpTutorial();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        HideTutorial();
    }

    private void ShowJumpTutorial()
    {
        tutorialText.gameObject.SetActive(true);

        keyboardSpace.sprite = keyboardSpace_solid;
        keyboardSpace.gameObject.SetActive(true);

        if (!isSwapping)
        {
            swapCoroutine = StartCoroutine(SwapImages());
            isSwapping = true;
        }
    }

    private void HideTutorial()
    {
        tutorialText.gameObject.SetActive(false);
        keyboardSpace.gameObject.SetActive(false);
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
            keyboardSpace.sprite = (keyboardSpace.sprite == keyboardSpace_solid) ? keyboardSpace_outline : keyboardSpace_solid;
        }
    }
}
