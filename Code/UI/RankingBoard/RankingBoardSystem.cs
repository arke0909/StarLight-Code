using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using UnityEngine;

namespace Code.UI.RankingBoard
{
    public class RankingBoardSystem : MonoBehaviour
    {
        private readonly string leaderboardID = "RankingBoard";
        private readonly string playerNameKey = "PlayerName";

        private void Start()
        {
            StartCoroutine(InitializeUGS());
        }

        private IEnumerator InitializeUGS()
        {
            yield return AwaitTask(UnityServices.InitializeAsync());

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                yield return AwaitTask(AuthenticationService.Instance.SignInAnonymouslyAsync());
            }
        }

        public void SubmitScoreCoroutine(float score)
        {
            StartCoroutine(SubmitScoreRoutine(score));
        }

        private IEnumerator SubmitScoreRoutine(float score)
        {
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                yield return AwaitTask(AuthenticationService.Instance.SignInAnonymouslyAsync());
            }

            string playerName = PlayerPrefs.GetString(playerNameKey);
            if (string.IsNullOrEmpty(playerName))
            {
                playerName = "Player_" + AuthenticationService.Instance.PlayerId.Substring(0, 6); // �⺻ �̸� ����
            }

            yield return AwaitTask(AuthenticationService.Instance.UpdatePlayerNameAsync(playerName));
            yield return AwaitTask(LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardID, score));
        }

        

        private IEnumerator AwaitTask(Task task)
        {
            if (task == null)
                yield break;

            while (!task.IsCompleted)
                yield return null;

            if (task.IsFaulted)
            {
                Debug.LogError(task.Exception);
            }
        }

        public void CheckDuplicatedNameCoroutine(string playerName, Action<bool> callback)
        {
            StartCoroutine(CheckDuplicatedNameRoutine(playerName, callback));
        }

        private IEnumerator CheckDuplicatedNameRoutine(string playerName, Action<bool> callback)
        {
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                yield return AwaitTask(AuthenticationService.Instance.SignInAnonymouslyAsync());
            }

            bool isDuplicated = false;

            var task = LeaderboardsService.Instance.GetScoresAsync(leaderboardID, new GetScoresOptions { Limit = 30 });
            yield return AwaitTask(task);

            if (!task.IsFaulted)
            {
                var value = task.Result;
                foreach (var entry in value.Results)
                {
                    string entryName = entry.PlayerName;
                    string entryBaseName = entryName.Split('#')[0];
                    if (entryBaseName == playerName)
                    {
                        isDuplicated = true;
                        break;
                    }
                }
            }
            else
            {
                isDuplicated = false;
            }

            yield return AwaitTask(AuthenticationService.Instance.UpdatePlayerNameAsync(playerName));
            callback?.Invoke(isDuplicated);
        }
    }
}