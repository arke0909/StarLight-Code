using UnityEngine;
using UnityEngine.UI;

namespace Code.Sounds
{
    public class BtnClickSound : MonoBehaviour
    {
        [HideInInspector]
        public Button button;
        public void Init()
        {
            button = GetComponent<Button>();
        }
    }
}