using UnityEngine;
using TMPro;

public class ChangeScore : MonoBehaviour
{
    public static ChangeScore Instance { get; private set; }

    public int score;
    public TextMeshProUGUI textMeshProUGUI;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

   public void RiseScore()
    {
        score += 20;
        textMeshProUGUI.text = score.ToString();
    }

    public void ModifyScore(int addScore)
    {
        score += addScore;
        textMeshProUGUI.text = score.ToString();
    }
}