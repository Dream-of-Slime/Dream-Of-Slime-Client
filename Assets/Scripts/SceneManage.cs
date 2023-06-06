using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public void Btn_ToInGameScene()
    {
        Time.timeScale = 1;
        ScoreManager.score = 0;
        SceneManager.LoadScene("InGame");
    }

    public void Btn_ToLobbyScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Lobby");
    }
    
    public void Show_Ranking()
    {
        GPGSBinder.Inst.ShowTargetLeaderboardUI(GPGSIds.leaderboard_ranking);
    }
}
