using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.RankingBoard
{
    public class RankBox : MonoBehaviour
    {
        [SerializeField] private Color fstColor, sndColor, trdColor; 
        [SerializeField] private Image boxPanel;
        [SerializeField] private TextMeshProUGUI rank;
        [SerializeField] private TextMeshProUGUI userName;
        [SerializeField] private TextMeshProUGUI timeText;

        public void SetRank(int rankValue, string userName, float time)
        {
            if (rankValue == 1)
            {
                boxPanel.color = fstColor;
            }
            else if (rankValue == 2)
            {
                boxPanel.color = sndColor;
            }
            else if (rankValue == 3)
            {
                boxPanel.color = trdColor;
            }
            
            rank.text = rankValue.ToString();
            this.userName.text = userName;
            timeText.text = $"{(int)(time / 60):00}:{(time % 60):00.00}";
        }
    }
}