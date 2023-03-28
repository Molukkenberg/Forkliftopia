using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider _UISlider;
    [SerializeField] private Slider _BackgroundSlider;
    [SerializeField] private AudioMixer _AudioMixer;

    void Start()
    {
        if (!PlayerPrefs.HasKey("Music_Volume"))
        {
            PlayerPrefs.SetFloat("Music_Volume", -40);
            _BackgroundSlider.value = -40;
            Load();
        }
        else
        {
            Load();
        }
        
        if (!PlayerPrefs.HasKey("UI_Volume"))
        {
            PlayerPrefs.SetFloat("UI_Volume", -40);
            _UISlider.value = -40;
            Load();
        }
        else
        {
            Load();
        }
    }
    
    public void ChangeUIVolume(float Value)
    {
        _AudioMixer.SetFloat("UI_Volume", _UISlider.value);
        PlayerPrefs.SetFloat("UI_Volume", _UISlider.value);
        PlayerPrefs.Save();
    }
    public void ChangeMusicVolume(float Value)
    {
        _AudioMixer.SetFloat("Music_Volume", _BackgroundSlider.value);
        PlayerPrefs.SetFloat("Music_Volume", _BackgroundSlider.value);
        PlayerPrefs.Save();
    }

    private void Load()
    {
        _AudioMixer.SetFloat("UI_Volume", PlayerPrefs.GetFloat("UI_Volume"));
        _AudioMixer.SetFloat("Music_Volume", PlayerPrefs.GetFloat("Music_Volume"));
        _UISlider.value = PlayerPrefs.GetFloat("UI_Volume");
        _BackgroundSlider.value = PlayerPrefs.GetFloat("Music_Volume");
    }
}