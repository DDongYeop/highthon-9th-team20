using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    [SerializeField] private AudioSource _walkSound, _camera;

    public void WalkSound()
    {
        _walkSound.Play();
    }
    public void CameraSound()
    {
        _camera.Play();
    }
}
