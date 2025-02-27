using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public Slider healthSlider;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            healthSlider.maxValue = GameManager.Instance.playerHealth;
            healthSlider.direction = Slider.Direction.LeftToRight;
            healthSlider.value = GameManager.Instance.playerHealth;
        }
    }

    private void Update()
    {
        if (GameManager.Instance != null)
        {
            healthText.text = "Health: " + GameManager.Instance.playerHealth;
            healthSlider.value = GameManager.Instance.playerHealth;
        }
    }
}