using UnityEngine;

public class MS_CheatMenu : MonoBehaviour
{
    [SerializeField]
    private Transform _itemList;
    [SerializeField]
    private MS_CheatCodeItem _itemPrefab;
    [SerializeField]
    private Transform _itemNotFoundPrefab;

    void Start()
    {
        var availableCheats = Resources.Load<AvailableCheats>("Available Cheats");
        foreach (var cheat in availableCheats.Cheats)
        {
            if (cheat.Found)
            {
                var instance = Instantiate(_itemPrefab, new Vector3(), Quaternion.identity, _itemList);
                instance.Initialize(cheat);
            }
            else
            {
                Instantiate(_itemNotFoundPrefab, new Vector3(), Quaternion.identity, _itemList);
            }
        }
    }
}
