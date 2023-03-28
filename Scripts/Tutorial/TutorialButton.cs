using UnityEngine;
using TMPro;

public class TutorialButton : MonoBehaviour
{
    [SerializeField] private Transform _hints;
    [SerializeField] private int _number = 0;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    [SerializeField] private GameObject _deliveryZone;
    [SerializeField] private GameObject _collectorZone;

    private void Start()
    {
        Time.timeScale = 0;
    }

    public void Next()
    {
        _hints.GetChild(_number).transform.gameObject.SetActive(false);
        if (_number <transform.childCount-1)
            _number++;

        _textMeshProUGUI.text = string.Format("{0}/14", _number);
        _hints.GetChild(_number).transform.gameObject.SetActive(true);

        if (_number == 9)
            Time.timeScale = 1;
    }
    public void Previous()
    {
        _hints.GetChild(_number).transform.gameObject.SetActive(false);
        if (_number > 0)
            _number--;

        _textMeshProUGUI.text = string.Format("{0}/14", _number);
        _hints.GetChild(_number).transform.gameObject.SetActive(true);
    }


}
