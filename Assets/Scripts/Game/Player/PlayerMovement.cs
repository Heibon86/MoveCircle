using DG.Tweening;
using UnityEngine;

namespace Game.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private const float SpeedMove = 3f;
        
        [SerializeField] private AnimationCurve _animationCurve;
        public void MoveToPoint(Vector3 pointTo)
        {
            float duration = CalculateDuration(pointTo);
            
            transform.DOMove(pointTo, duration).SetEase(_animationCurve);
        }

        public void MoveToPath(Vector3[] path)
        {
            float duration = CalculateDuration(path);
            transform.DOPath(path, 1).SetEase(_animationCurve);
        }

        public void StopMove()
        {
            DOTween.KillAll();
        }

        private float CalculateDuration(Vector3 pointTo)
        {
            float distance = Vector3.Distance(transform.position, pointTo);
            float duration = distance / SpeedMove;

            return duration;
        }
        
        private float CalculateDuration(Vector3[] path)
        {
            float distance = 0;
            
            for (int i = 0; i < path.Length - 1; i++)
            {
                distance = Vector3.Distance(path[i], path[i + 1]);
            }
            
            float duration = distance / SpeedMove;

            return duration;
        }
    }
}
