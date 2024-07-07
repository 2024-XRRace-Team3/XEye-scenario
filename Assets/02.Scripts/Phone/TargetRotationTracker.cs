using UnityEngine;

namespace Scripts.Phone
{
    public class TargetRotationTracker  : MonoBehaviour
    {
        public Transform player;         // 플레이어의 트랜스폼
        public Transform target;         // 타깃의 트랜스폼
        public float viewThreshold = 0.95f;  // 타깃을 바라보는 각도 임계값 (1에 가까울수록 타깃을 더 정밀하게 바라봄)
    
        [SerializeField]private float accumulatedRotation = 0f; // 누적 회전 각도
        private Vector3 lastDirection;         // 이전 프레임의 방향

        void Start()
        {
            if (player == null)
                player = Camera.main.transform; // 플레이어의 트랜스폼 설정

            // 초기 방향 설정
            lastDirection = (player.position - target.position).normalized;
        }

        void Update()
        {
            // 현재 프레임에서 타깃을 기준으로 플레이어의 방향
            Vector3 currentDirection = (player.position - target.position).normalized;

            // 이전 프레임과 현재 프레임 간의 각도 차이를 계산
            float angleDelta = Vector3.Angle(lastDirection, currentDirection);

            // 누적 회전 각도 업데이트
            accumulatedRotation += angleDelta;

            // 이전 프레임의 방향 업데이트
            lastDirection = currentDirection;

            // 플레이어가 타깃을 바라보고 있는지 확인
            Vector3 directionToTarget = (target.position - player.position).normalized;
            float dotProduct = Vector3.Dot(player.forward, directionToTarget);

            if (dotProduct > viewThreshold)
            {
                Debug.Log("타깃을 바라보고 있습니다.");
            }

            // 누적 회전 각도가 360도 이상인 경우
            if (accumulatedRotation >= 360f)
            {
                Debug.Log("플레이어가 타깃을 기준으로 360도 회전했습니다.");
                accumulatedRotation = 0f; // 누적 회전 각도 초기화
            }
        }
    }
}
