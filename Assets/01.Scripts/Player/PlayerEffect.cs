using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    [SerializeField] private AudioSource _walkSound;

    public void WalkSound()
    {
        _walkSound.Play();
    }
}
