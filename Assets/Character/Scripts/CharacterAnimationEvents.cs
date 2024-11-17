using System;
using UnityEngine;

public class CharacterAnimationEvents : MonoBehaviour
{

    public event Action OnDeathAnimationEnd;

    public void DeathAnimationEnded()
    {
        OnDeathAnimationEnd?.Invoke();
    }

}
