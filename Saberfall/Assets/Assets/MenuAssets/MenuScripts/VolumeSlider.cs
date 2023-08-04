using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider volumeSlider;
    private void Awake()
    {
        volumeSlider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        volumeSlider.onValueChanged.AddListener(delegate { MenuController.menuController.setMusicVolume(volumeSlider.value); });
    }

    private void OnDisable()
    {
        volumeSlider.onValueChanged.RemoveAllListeners();
    }
}
