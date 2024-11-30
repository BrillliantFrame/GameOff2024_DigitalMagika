using DG.Tweening;
using System.Collections;
using UnityEngine;

public class MS_EndingSceneManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform _creditsPanel;
    [SerializeField]
    private FadingUI _finalScene;
    [SerializeField]
    private float _creditsRollDuration = 5f;

    void Start()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StartCoroutine(rollCredits());
    }

    private IEnumerator rollCredits()
    {
        TweenParams tParms = new TweenParams().SetEase(Ease.Linear);
        var tween = _creditsPanel.DOAnchorPosY(0, _creditsRollDuration, true).SetAs(tParms);
        yield return tween.WaitForCompletion();
        yield return _finalScene.show();
    }

    public void ReturnToMainMenu()
    {
        AppCore.Instance?.CreditsToMain();
        AkSoundEngine.SetRTPCValue("LowPassPauseMenu_RTPC", 100f);
        AkSoundEngine.SetRTPCValue("PauseMenu_RTPC", 100f);
        AkSoundEngine.PostEvent("Stop_All", gameObject);
    }
}
