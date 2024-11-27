using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Available Cheats", menuName = "ScriptableObjects/Core Objects/Available Cheats", order = 2)]
public class AvailableCheats : ScriptableObject
{
    [SerializeField]
    public List<Cheat> Cheats = new List<Cheat>();

    public bool HasFoundCheatcodes()
    {
        return Cheats.FindAll(x => x.Found == true).Count > 0;
    }

    public bool HasFoundCheatcode(Cheats cheat)
    {
        return Cheats.Find(x => x.CheatName == cheat).Found;
    }

    public void FoundCheatcode(Cheats cheat)
    {
        for (int i = 0; i < Cheats.Count; i++)
        {
            if (Cheats[i].CheatName == cheat)
            {
                Cheats[i].Found = true;
                return;
            }
        }
    }
}

[Serializable]
public class Cheat
{
    public Cheats CheatName;
    public string CheatDescription;
    public bool Enabled = false;
    public bool Found = false;
}