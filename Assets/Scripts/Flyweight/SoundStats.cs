using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundStats", menuName = "Stats/Sound", order = 0)]
public class SoundStats : ScriptableObject {
    [SerializeField] private StatValues _statValues;

    public AudioClip AudioClip => _statValues.AudioClip;
}

[System.Serializable]
public struct StatValues {
    public AudioClip AudioClip;
}
