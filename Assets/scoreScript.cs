using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scoreScript : MonoBehaviour
{
    public float score;
    public TextMeshProUGUI amountOfPlaneText;
    private TextMeshProUGUI scoreText;
    public int amountOfPlane;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.parent.GetChild(1).gameObject.SetActive(false);
        scoreText = gameObject.GetComponent<TextMeshProUGUI>();
        scoreText.text = "Score : 0";
    }

    private void Update()
    {
        amountOfPlaneText.text = "Planes : " + amountOfPlane.ToString();
        if (score <= 200 * amountOfPlane && amountOfPlane <= 0)
        {
            GameOver("Losing");

        }else if(score >= 200 * amountOfPlane) 
        {
            GameOver("Winning");
        }
    }

    public void plusScore(float scoreTemp)
    {
        score += scoreTemp;
        scoreText.text = "Score : " + score.ToString();

    }

    private void GameOver(string status)
    {
        if(status == "Winning")
        {
            gameObject.transform.parent.GetChild(1).gameObject.SetActive(true);
            gameObject.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>().text = "You Win!";
        }
        if (status == "Losing")
        {
            gameObject.transform.parent.GetChild(1).gameObject.SetActive(true);
            gameObject.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
    }

    public void reducePlane()
    {
        amountOfPlane--;
    }
}
