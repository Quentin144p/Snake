using UnityEngine;
using TMPro;

public class ScoreDisplayer : MonoBehaviour
{
    private TextMeshPro tmPro;

    private void Awake()
    {
        tmPro = GetComponent<TextMeshPro>();
    }
    public void SetScore(int score)
    {
        tmPro.text = score.ToString("0");
    }
}
