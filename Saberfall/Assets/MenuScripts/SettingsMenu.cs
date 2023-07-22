using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    // Sets volume value for audio mixer
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }


    // Sets fullscreen mode togglable
    public void SetFullScreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }
}
