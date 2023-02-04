using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scoreScript : MonoBehaviour
{
    public float score;
    private TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.parent.GetChild(1).gameObject.SetActive(false);
        scoreText = gameObject.GetComponent<TextMeshProUGUI>();
        scoreText.text = "Score : 0";
    }

    public void plusScore(float scoreTemp)
    {
        score += scoreTemp;
        scoreText.text = "Score : " + score.ToString();
        if(score >= 400)
        {
            GameOver("Winning");
        }
    }

    private void GameOver(string status)
    {
        if(status == "Winning")
        {
            gameObject.transform.parent.GetChild(1).gameObject.SetActive(true);
            Debug.Log(gameObject.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>().text);
        }
    }
}
