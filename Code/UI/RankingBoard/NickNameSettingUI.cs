using TMPro;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Code.UI.RankingBoard
{
    public class NickNameSettingUI : MajorUI
    {
        [SerializeField] private RankingBoardSystem rankingSystem;
        [SerializeField] TMP_InputField nicknameText;
        private readonly string playerNameKey = "PlayerName";
        
        public UnityEvent OnNicknameChanged;

        public void OnSave()
        {
            string playerName = nicknameText.text.Trim();

            if (!string.IsNullOrEmpty(playerName) && !playerName.Contains(" ") && !playerName.Contains("\n"))
            {
                if (playerName.Length > 10)
                {
                    Debug.LogWarning("Player name is too long. Maximum length is 10 characters.");
                    nicknameText.text = string.Empty;
                    nicknameText.placeholder.GetComponent<TMP_Text>().text =
                        "<color=red>닉네임이 10글자보다 깁니다.</color>";
                    return;
                }

                rankingSystem.CheckDuplicatedNameCoroutine(playerName, isDuplicated =>
                {
                    if (isDuplicated)
                    {
                        Debug.LogWarning("Player name already exists.");
                        nicknameText.text = string.Empty; // Clear the input field
                        nicknameText.placeholder.GetComponent<TMP_Text>().text =
                            "<color=red>중복된 닉네임입니다.</color>";
                        return;
                    }

                    PlayerPrefs.SetString(playerNameKey, playerName);
                    PlayerPrefs.Save();
                    
                    rankingSystem.SubmitScoreCoroutine(3600);
                    
                    ControlMenu();
                    nicknameText.text = string.Empty;
                    OnNicknameChanged?.Invoke();
                });
            }
            else
            {
                Debug.LogWarning("Player name cannot be empty.");
                nicknameText.text = string.Empty;
                nicknameText.placeholder.GetComponent<TMP_Text>().text = "<color=red>닉네임은 공백이거나 포함이 될 수 없습니다.</color>";
            }
        }
    }
}