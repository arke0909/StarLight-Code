using UnityEngine;

namespace Code.Patterns.PatternDatas
{
    [CreateAssetMenu(fileName = "MoveData", menuName = "SO/Pattern/MoveData", order = 0)]
    public class MoveDataSO : PatternSO
    {
        public int moveIdx;
        public float speed;
    }
}