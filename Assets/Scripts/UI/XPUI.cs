using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class XPUI : MonoBehaviour
{
    public TextMeshProUGUI XP;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance != null)
        {
            XP.text = "EXP: " + GameManager.Instance.playerXP;
          
        }
    }
}
