using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    public Slider slider;

    void Update()
    {
        slider.maxValue = GameManager.Instance.MaxHP;
        slider.value = GameManager.Instance.CurrentHP;
    }
}
