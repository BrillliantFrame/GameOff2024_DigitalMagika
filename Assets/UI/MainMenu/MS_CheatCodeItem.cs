using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MS_CheatCodeItem : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField]
    private TMP_Text _cheatName;
    [SerializeField]
    private TMP_Text _cheatDescription;
    [SerializeField]
    private Image _toggle;
    [SerializeField]
    private Sprite _activeToggle;
    [SerializeField]
    private Sprite _inactiveToggle;

    private Cheat _currentCheat;

    public void Initialize(Cheat cheat)
    {
        _currentCheat = cheat;
        _cheatName.text = _currentCheat.CheatName.ToString();
        _cheatDescription.text = _currentCheat.CheatDescription;
        _toggle.sprite = _currentCheat.Enabled ? _activeToggle : _inactiveToggle;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _currentCheat.Enabled = !_currentCheat.Enabled;
        _toggle.sprite = _currentCheat.Enabled ? _activeToggle : _inactiveToggle;
        Debug.Log("sprite toggled");
    }
}
