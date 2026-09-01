using UnityEngine;
using UnityEngine.UI;

public class Guage : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
    }

    void Update()
    {
        slider.maxValue = GameManager.Instance.MaxHP;
        slider.value = GameManager.Instance.CurrentHP;
    }
}
