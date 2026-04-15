using UnityEngine;

namespace Code.UI
{
    public class ETCButtonEvent : MonoBehaviour
    {
        [SerializeField] private MajorUI majorUI;
        
        public void OnMajorMenuControl()
        {
            majorUI.ControlMenu();
        }

        public void OnExit()
        {
            Application.Quit();
        }
    }
}