using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicActivation : MonoBehaviour
{
    [SerializeField] string trackName;

    private void OnEnable()
    {
        MusicManager.instance.ActivateMusicTrack(trackName);
    }
}
