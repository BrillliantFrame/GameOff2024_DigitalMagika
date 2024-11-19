using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class MS_TutorialLevelOne : MonoBehaviour
{
    [Header("Tutorial Texts")]
    public TMP_Text tutorialText1;  // For CollisionA
    public TMP_Text tutorialText2;  // For CollisionB
    public TMP_Text tutorialText3;  // For CollisionC

    [Header("Keyboard Images")]
    public Image keyboardA;
    public Image keyboardD;
    public Sprite keyboardA_outline;
    public Sprite keyboardD_outline;
    public Sprite keyboardA_solid;
    public Sprite keyboardD_solid;

    [Header("Collision Areas")]
    public Collider2D collisionA;
    public Collider2D collisionB;
    public Collider2D collisionC;

    private Coroutine swapCoroutine;
    private bool isSwapping = false;

    private void Start()
    {
        // Initially hide all texts and keyboard images
        tutorialText1.gameObject.SetActive(false);
        tutorialText2.gameObject.SetActive(false);
        tutorialText3.gameObject.SetActive(false);
        keyboardA.gameObject.SetActive(false);
        keyboardD.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered Collision: " + collision.name); // Log the collider name when it enters

        if (collision == collisionA)
        {
            ShowMoveTutorial();
        }
        else if (collision == collisionB)
        {
            ShowHeartTutorial();
        }
        else if (collision == collisionC)
        {
            ShowPortalTutorial();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exited Collision: " + collision.name); // Log the collider name when it exits

        if (collision == collisionA)
        {
            HideTutorial(tutorialText1);
        }
        else if (collision == collisionB)
        {
            HideTutorial(tutorialText2);
        }
        else if (collision == collisionC)
        {
            HideTutorial(tutorialText3);
        }
    }

    private void ShowMoveTutorial()
    {
        tutorialText1.text = "You can move around using the A and D keys";
        tutorialText1.gameObject.SetActive(true);

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

    private void ShowHeartTutorial()
    {
        tutorialText2.text = "Hearts can be picked up to regain health when you lose one";
        tutorialText2.gameObject.SetActive(true);
        StopImageSwap();
    }

    private void ShowPortalTutorial()
    {
        tutorialText3.text = "You can advance through the forest by going through these doors";
        tutorialText3.gameObject.SetActive(true);
        StopImageSwap();
    }

    private void HideTutorial(TMP_Text tutorialText)
    {
        tutorialText.gameObject.SetActive(false);

        // Hide keyboard images if MoveTutorial is being hidden
        if (tutorialText == tutorialText1)
        {
            keyboardA.gameObject.SetActive(false);
            keyboardD.gameObject.SetActive(false);
            StopImageSwap();
        }
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
            yield return new WaitForSeconds(0.5f);  // interval for swapping
            keyboardA.sprite = (keyboardA.sprite == keyboardA_solid) ? keyboardA_outline : keyboardA_solid;
            keyboardD.sprite = (keyboardD.sprite == keyboardD_solid) ? keyboardD_outline : keyboardD_solid;
        }
    }
}
