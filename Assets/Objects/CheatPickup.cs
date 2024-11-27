using UnityEngine;

public class CheatPickup : CollectableObject
{
    [SerializeField]
    private Cheats _cheatToUnlock;

    private AvailableCheats cheats;

    public void Start()
    {
        cheats = Resources.Load<AvailableCheats>("Available Cheats");
        gameObject.SetActive(!cheats.HasFoundCheatcode(_cheatToUnlock));
    }

    protected override void onPickup(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            cheats.FoundCheatcode(_cheatToUnlock);
            AkSoundEngine.PostEvent("Player_Regen", gameObject);
            gameObject.SetActive(false);
        }
    }
}
