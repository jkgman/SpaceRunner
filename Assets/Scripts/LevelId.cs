using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelId : MonoBehaviour{

    public int levelNumber;
    public int starRating;
    public int score;
    private bool locked = true;

    public Transform[] stars;
    public TextMeshProUGUI scoreText;

    public GameObject grayStar, goldStar;

    private void Start()
    {
        scoreText.text = "0";
        if (GameManager.Instance.gData.levelProgression.Length > 0) { 
            starRating = GameManager.Instance.gData.levelProgression[levelNumber];
            score = GameManager.Instance.gData.levelScores[levelNumber];
            
            if (starRating < 4)
            {
                scoreText.text = score.ToString();
                for (int i = 0; i < starRating; i++)
                {
                    
                    Instantiate(goldStar, stars[i]);

                }
            }

        }
    }

}
