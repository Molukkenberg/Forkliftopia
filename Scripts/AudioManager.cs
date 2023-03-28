using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private AudioSource _audioSource;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Modify(int Value)
    {
        _audioSource.volume= Value;
    }
}
