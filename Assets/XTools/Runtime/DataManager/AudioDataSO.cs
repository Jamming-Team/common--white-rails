using System;
using System.Collections.Generic;
using UnityEngine;

namespace XTools {
    [CreateAssetMenu(fileName = "AudioDataSO", menuName = "XTools/Audio/AudioDataSO", order = 0)]
    public class AudioDataSO : ScriptableObject {
        [Range(0.01f, 1f)] public float musicVolume = 0.8f;
        [Range(0.01f, 1f)] public float sfxVolume = 0.8f;
        public MusicData music;
    }

    [Serializable]
    public class MusicData {
        public bool hasMusic = false;
        // public List<AudioClip> audioClips;
        public List<MusicBundle> bundles = new();

        public float crossFadeTime = 2.0f;
    }
}