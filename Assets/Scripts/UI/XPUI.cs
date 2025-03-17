using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class XPUI : MonoBehaviour
{
    public TextMeshProUGUI XP;
    public TextMeshProUGUI lvl;
    public TextMeshProUGUI nextlvl;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance != null)
        {
            lvl.text = "Level: " + GameManager.Instance.playerLevel;
            nextlvl.text = "Next level: " + GameManager.Instance.experienceToNextLevel;
            XP.text = "Current XP: " + GameManager.Instance.playerXP;
        }
    }
}
