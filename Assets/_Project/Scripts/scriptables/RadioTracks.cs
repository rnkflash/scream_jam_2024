using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.scriptables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/RadioTracks")]
    public class RadioTracks: SerializedScriptableObject
    {
        public AudioClip[] audioClips;
    }
}