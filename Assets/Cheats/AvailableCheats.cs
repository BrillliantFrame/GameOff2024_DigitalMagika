using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Available Cheats", menuName = "ScriptableObjects/Core Objects/Available Cheats", order = 2)]
public class AvailableCheats : ScriptableObject
{
    [SerializeField]
    public List<Cheat> Cheats = new List<Cheat>();


}

[Serializable]
public class Cheat
{
    public Cheats CheatName;
    public bool Enabled = false;
    public bool Found = false;
}