using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelEndMenu : MonoBehaviour {

    #region public variables
    public TextMeshProUGUI scoreText;
    public GameObject goldStarPrefab;
    public Transform star1, star2, star3;
    public float scoreTimeLength = 1;
    private float scoreTimerStart;
    #endregion

    #region private variables
    private int score;
    private int levelScore;
    private int currentLevel;
    private ItemManager itm;
    private int starScore =1;
    #endregion

    // Use this for initialization
    void OnEnable () {
        itm = FindObjectOfType<ItemManager>();
        currentLevel = GameManager.Instance.currentLevel;
        levelScore = itm.coinQ * 100;
        GameManager.Instance.gData.levelScores[currentLevel] = levelScore;
        Instantiate(goldStarPrefab, star1);
        if (PlayerHandle.instance != null && PlayerHandle.instance.hitCount == 0)
        {
            starScore++;
            Instantiate(goldStarPrefab, star2);
        }

        if (itm.coinQ >= 10)
        {
            starScore++;
            Instantiate(goldStarPrefab, star3);
        }



        GameManager.Instance.gData.levelProgression[currentLevel] = starScore;
        GameManager.Instance.SaveData(GameManager.Instance.gData);
        scoreTimerStart = Time.time;
    }

    
    // Update is called once per frame
    void Update () {
        if (score < levelScore) {
            score = Mathf.Clamp(Mathf.RoundToInt(Mathf.Lerp(0,levelScore, (Time.time - scoreTimerStart)/scoreTimeLength)),0, levelScore);
            scoreText.text = score.ToString();
        } else
        {
            scoreText.text = levelScore.ToString();
        }
        
	}
}
