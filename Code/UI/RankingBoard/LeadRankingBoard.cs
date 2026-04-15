using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using UnityEngine;

namespace Code.UI.RankingBoard
{
    public class LeadRankingBoard : MajorUI
    {
        [SerializeField] Transform content;
        [SerializeField] RankBox rankBoxPrefab;

        private readonly string leaderboardID = "RankingBoard";
        private List<RankBox> rankBoxes = new List<RankBox>();

        private async void Start()
        {
            await InitializeUGS();
        }

        private async Task InitializeUGS()
        {
            await UnityServices.InitializeAsync();

            if (!AuthenticationService.Instance.IsSignedIn)
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            
            await LoadLeaderboard();
        }

        public async Task LoadLeaderboard()
        {
            ClearRankBoxes();

            var scoresResponse =
                await LeaderboardsService.Instance.GetScoresAsync(leaderboardID,
                    new GetScoresOptions { Limit = 10 });
            if (scoresResponse == null || scoresResponse.Results == null || scoresResponse.Results.Count == 0)
            {
                return;
            }

            int rank = 1;
            foreach (var score in scoresResponse.Results)
            {
                RankBox rankBox = Instantiate(rankBoxPrefab, content);
                rankBox.SetRank(rank, score.PlayerName.Split('#')[0], (float)score.Score);
                rankBoxes.Add(rankBox);
                rank++;
            }
        }

        private void ClearRankBoxes()
        {
            foreach (var rankBox in rankBoxes)
            {
                Destroy(rankBox.gameObject);
            }

            rankBoxes.Clear();
        }

        public async void RefreshLeaderboard()
        {
            await LoadLeaderboard();
        }
    }
}