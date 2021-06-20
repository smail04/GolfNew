using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    public Text text;
    private float time = 0;   
    private bool isCount;

    public float Value { get => time; }

    void Update()
    {
        if (isCount)
        {
            AddTime(Time.unscaledDeltaTime);
        }
    }

    public void StartTimer()
    {
        time = 0;
        isCount = true;
    }

    public void StopTimer()
    {
        isCount = false;
    }

    public void Clear()
    {
        AddTime(-time);
        StopTimer();
    }

    private void AddTime(float time)
    {
        this.time += time;
        text.text = string.Format("{0:0.00}", this.time);
    }


}
