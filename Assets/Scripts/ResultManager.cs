using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    public static ResultManager instance;

    [SerializeField] GameObject _result;
    [SerializeField] Text _scoreText;
    [SerializeField] Text _comboText;
    
    private string log;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _result.SetActive(false);
    }

    public void GameOver()
    {
        GPGSBinder.Inst.ReportLeaderboard(GPGSIds.leaderboard_ranking, ScoreManager.score, success => log = $"{success}");
        Time.timeScale = 0;
        _scoreText.text = "Score: " + ScoreManager.score.ToString();
        _comboText.text = "Max Combo: " + SkillManager.instance._highestCombo.ToString();
        _result.SetActive(true);
    }
}
