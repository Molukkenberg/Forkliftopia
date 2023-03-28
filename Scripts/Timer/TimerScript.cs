using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public float TimeLeft;
    public float TimerStartOn;
    public bool TimerOn = false;

    [SerializeField] private TMPro.TextMeshProUGUI TextMeshProUGUI;
    [SerializeField] private TMP_InputField _deliveryTimerInput;
    [SerializeField] private TMP_InputField _collectorTimerInput;

    public bool Spawner;
    public bool Collector;

    void Start()
    {
        TimerOn = true;
        TimerStartOn = TimeLeft;
    }
    void Update()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                if (Spawner)
                {
                    if(SpawnGoods.Instance)
                    SpawnGoods.Instance.InstantiateGoodsInChildren();
                    int.TryParse(_deliveryTimerInput.text, out int _spawnerTimer);
                    TimerStartOn = _spawnerTimer;
                }
                if (Collector)
                {
                    CollectGoods.Instance.CollectAllInAllChildren();
                    int.TryParse(_collectorTimerInput.text, out int _collectorTimer);
                    TimerStartOn = _collectorTimer;
                }
                    
                Debug.Log("Time is up!");
                TimeLeft = TimerStartOn;
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TextMeshProUGUI.text = string.Format("{0:00} : {1:00}",minutes,seconds);
    }
}
