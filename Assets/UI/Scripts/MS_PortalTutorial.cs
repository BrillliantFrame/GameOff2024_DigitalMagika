using UnityEngine;
using TMPro;

public class PortalTutorial : MonoBehaviour
{
    public TMP_Text tutorialText;
    public Collider2D collisionC;

    private void Start()
    {
        tutorialText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered collision in PortalTutorial");
        ShowPortalTutorial();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Entered collision in PortalTutorial");
        HideTutorial();
    }

    private void ShowPortalTutorial()
    {
        tutorialText.text = "You can advance through the forest by going through these doors";
        tutorialText.gameObject.SetActive(true);
    }

    private void HideTutorial()
    {
        tutorialText.gameObject.SetActive(false);
    }
}
