using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float answerTime;
    [SerializeField] float reviewTime;
    [SerializeField] Image timerImage;

    float timeLeft;
    float totalTime;

    public void StartTimer()
    {
        ResetTimer(answerTime);
    }

    void Update()
    {
        UpdateTimer();
        UpdateTimerImage();
    }

    void ResetTimer(float time)
    {
        timeLeft = time;
        totalTime = time;
        timerImage.fillAmount = 1f;
    }

    void UpdateTimer()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                timeLeft = 0;
            }
        }
    }

    void UpdateTimerImage()
    {
        float fillAmount = timeLeft / totalTime;

        timerImage.fillAmount = fillAmount;
    }
}
