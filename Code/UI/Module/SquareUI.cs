using UnityEngine;

namespace Code.UI.Module
{
    [ExecuteAlways]
    public class SquareUI : MonoBehaviour
    {
         private RectTransform rectTrm;
        
            private void Awake()
            {
              rectTrm =GetComponent<RectTransform>();
            }
          
            private void Update()
            {
                    float width = rectTrm.rect.width;
                    rectTrm.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, width);
            }
    }
}