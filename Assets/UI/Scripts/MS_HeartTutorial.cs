using UnityEngine;
using TMPro;

public class HeartTutorial : MonoBehaviour
{
    public TMP_Text tutorialText;

    private void Start()
    {
        tutorialText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered collision in hearth Tutorial");
        ShowHeartTutorial();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Entered collision in hearth Tutorial");
        HideTutorial();
    }

    private void ShowHeartTutorial()
    {
        tutorialText.text = "Hearts can be picked up to regain health when you lose one";
        tutorialText.gameObject.SetActive(true);
    }

    private void HideTutorial()
    {
        tutorialText.gameObject.SetActive(false);
    }
}
