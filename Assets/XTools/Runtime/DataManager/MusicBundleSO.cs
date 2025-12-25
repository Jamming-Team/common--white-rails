using System.Collections.Generic;
using UnityEngine;

namespace XTools {
    [CreateAssetMenu(fileName = "MusicBundleSO", menuName = "XTools/Audio/MusicBundleSO", order = 0)]
    public class MusicBundle : ScriptableObject {
        public MusicBundleType type;
        public List<AudioClip> audioClips;
        public bool shouldLoopFirstClip;
    }

    public enum MusicBundleType {
        MainMenu,
        Gameplay
    }
}