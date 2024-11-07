using System.Collections;
using DG.Tweening;
using UnityEngine;

public class FadingUI : MonoBehaviour
{
    [SerializeField]
    private float _animationDuration = 0.1f;
    [SerializeField]
    public bool IsVisible = false;

    private CanvasGroup _canvas;

    private void Awake()
    {
        _canvas = GetComponent<CanvasGroup>();
    }

    protected void Start()
    {
        _canvas.alpha = IsVisible ? 1 : 0;
        _canvas.interactable = IsVisible;
        _canvas.blocksRaycasts = IsVisible;
    }

    public void SwitchVisibility()
    {
        if (IsVisible)
            StartCoroutine(hide());
        else
            StartCoroutine(show());
    }

    public IEnumerator show()
    {
        if (!IsVisible)
        {
            _canvas.interactable = true;
            _canvas.blocksRaycasts = true;
            yield return _canvas.DOFade(1, _animationDuration).WaitForCompletion();
            IsVisible = true;
        }
    }

    public virtual IEnumerator hide()
    {
        if (IsVisible)
        {
            yield return _canvas.DOFade(0, _animationDuration).WaitForCompletion();
            _canvas.interactable = false;
            _canvas.blocksRaycasts = false;
            IsVisible = false;
        }
    }
}