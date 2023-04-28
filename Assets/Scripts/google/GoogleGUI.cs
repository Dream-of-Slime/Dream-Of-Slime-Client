using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoogleGUI : MonoBehaviour
{
    private string log;

    void OnGUI()
    {
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one * 3);
        
        if (GUILayout.Button("ClearLog"))
            log = "";

        if (GUILayout.Button("Login"))
            GPGSBinder.Inst.Login((success, localUser) =>
                log = $"{success}, {localUser.userName}, {localUser.id}, {localUser.state}, {localUser.underage}");

        if (GUILayout.Button("Logout"))
            GPGSBinder.Inst.Logout();
        
        if (GUILayout.Button("SaveCloud"))
            GPGSBinder.Inst.SaveCloud("mysave", "want data", success => log = $"{success}");

        if (GUILayout.Button("LoadCloud"))
            GPGSBinder.Inst.LoadCloud("mysave", (success, data) => log = $"{success}, {data}");

        if (GUILayout.Button("DeleteCloud"))
            GPGSBinder.Inst.DeleteCloud("mysave", success => log = $"{success}");
        
        if (GUILayout.Button("ReportLeaderboard_num"))
            GPGSBinder.Inst.ReportLeaderboard(GPGSIds.leaderboard_ranking, 1000, success => log = $"{success}");

        if (GUILayout.Button("ShowTargetLeaderboardUI_num"))
            GPGSBinder.Inst.ShowTargetLeaderboardUI(GPGSIds.leaderboard_ranking);

        GUILayout.Label(log);
    }
}