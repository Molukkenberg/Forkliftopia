using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private AudioSource _audioSource;
    
    public void Modify(int Value)
    {
        _audioSource.volume= Value;
    }
}
