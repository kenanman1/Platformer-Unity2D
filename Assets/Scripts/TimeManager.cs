using System;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] TextMeshPro timeText;

    float time = 0;
    void Start()
    {
        timeText.text = $"Time in this level: {time}";
    }

    void Update()
    {
        time += Time.deltaTime;
        timeText.text = $"Time in this level: {Math.Round(time, 2)}";
    }
}
