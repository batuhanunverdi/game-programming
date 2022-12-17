using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class Score : MonoBehaviour
{
    public GameObject rowPrefab;

    public Transform rowsParent;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void SendLeaderboard(int level)
    {
        var request =
            new UpdatePlayerStatisticsRequest {
                Statistics =
                    new List<StatisticUpdate> {
                        new StatisticUpdate {
                            StatisticName = "LevelBoard",
                            Value = level
                        }
                    }
            };
        PlayFabClientAPI.UpdatePlayerStatistics (
            request,
            OnLeaderboardUpdate,
            OnError
        );
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Succesful leaderboard");
    }

    public void GetLeaderboard()
    {
        var request =
            new GetLeaderboardRequest {
                StatisticName = "LevelBoard",
                StartPosition = 0,
                MaxResultsCount = 10
            };
        PlayFabClientAPI.GetLeaderboard (request, OnLeaderboardGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach(Transform item in rowsParent){
            Destroy(item.gameObject);
        }
        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(rowPrefab, rowsParent);

            TextMeshProUGUI[] texts = newGo.GetComponentsInChildren<TextMeshProUGUI>();
            
            texts[0].text = (item.Position+1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();

            Debug.Log(string.Format("Place: {0} | ID: {1} | VALUE: {2}",item.Position,item.DisplayName,item.StatValue));
        }
    }

    void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    // Update is called once per frame
    void Update()
    {
    }
}
