using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelEndMenu : MonoBehaviour {

    #region public variables
    public TextMeshProUGUI scoreText;
    public GameObject goldStarPrefab;
    public Transform star1, star2, star3;
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
        //Time.timeScale = 0;
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

        if (itm.coinQ >= 5)
        {
            starScore++;
            Instantiate(goldStarPrefab, star3);
        }

        GameManager.Instance.gData.levelProgression[currentLevel] = starScore;
        GameManager.Instance.SaveData(GameManager.Instance.gData);
    }


    // Update is called once per frame
    void Update () {
        if (score < levelScore) { 
            score++;
        }
        scoreText.text = score.ToString(); 
	}
}
