using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class MoveTutorial : MonoBehaviour
{
    public TMP_Text tutorialText;
    public Image keyboardA;
    public Image keyboardD;
    public Sprite keyboardA_outline;
    public Sprite keyboardD_outline;
    public Sprite keyboardA_solid;
    public Sprite keyboardD_solid;
    public Collider2D collisionA;

    private Coroutine swapCoroutine;
    private bool isSwapping = false;

    private void Start()
    {
        tutorialText.gameObject.SetActive(false);
        keyboardA.gameObject.SetActive(false);
        keyboardD.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered collision in MoveTutorial"); // or HeartTutorial / PortalTutorial
        ShowMoveTutorial();  // or ShowHeartTutorial / ShowPortalTutorial
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exited collision in MoveTutorial"); // or HeartTutorial / PortalTutorial
        HideTutorial();
    }

    private void ShowMoveTutorial()
    {
        tutorialText.text = "You can move around using the A and D keys";
        tutorialText.gameObject.SetActive(true);

        keyboardA.sprite = keyboardA_solid;
        keyboardD.sprite = keyboardD_solid;
        keyboardA.gameObject.SetActive(true);
        keyboardD.gameObject.SetActive(true);

        if (!isSwapping)
        {
            swapCoroutine = StartCoroutine(SwapImages());
            isSwapping = true;
        }
    }

    private void HideTutorial()
    {
        tutorialText.gameObject.SetActive(false);
        keyboardA.gameObject.SetActive(false);
        keyboardD.gameObject.SetActive(false);
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
            keyboardA.sprite = (keyboardA.sprite == keyboardA_solid) ? keyboardA_outline : keyboardA_solid;
            keyboardD.sprite = (keyboardD.sprite == keyboardD_solid) ? keyboardD_outline : keyboardD_solid;
        }
    }
}
