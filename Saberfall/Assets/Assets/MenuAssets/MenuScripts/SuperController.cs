using UnityEngine;
using UnityEngine.Audio;
public class SuperController : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _mainAudioMixer;

    protected AudioMixer MainAudioMixer { get => _mainAudioMixer; }
}