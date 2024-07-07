using System;
using UnityEngine;

namespace Phone
{
    public class PhoneController : MonoBehaviour
    {
        public Transform lookAtTarget; // 녹화를 진행하시는 위치를 지정
        public GameObject cameraModel;
        [field: SerializeField] public bool isLookStraight { get; private set; } = false;
        public bool virtualCapturing = false; // true일 경우 해당 폰의 위치를 기준으로 카메라가 생성됨.
        public float virtualCapturingStamp = 3f;
        
        
        private Camera camera;
        private float time = 0f;
        
        private void Awake()
        {
            camera = GetComponentInChildren<Camera>();
        }

        private void Start()
        {
        }

        private void Update()
        {
            isLookStraight = IsObjectVisible(lookAtTarget);
            time += Time.deltaTime;
            if (time > virtualCapturingStamp)
            {
                if (isLookStraight && virtualCapturing)
                {
                    CreateWayPoint();
                }
                time = 0f;
            }
        }
        
        public bool IsObjectVisible(Transform target)
        {
            var planes = GeometryUtility.CalculateFrustumPlanes(camera);
            var point = target.position;
            foreach (var plane in planes)
            {
                if (plane.GetDistanceToPoint(point) < 0)
                    return false;
            }
            return true;
        }

        public void CreateWayPoint()
        {
            Instantiate(cameraModel, transform.position, transform.rotation);
        }
        
        // Phone UI 내에서 사용
        public void SetVirtualCapturing(bool enable) => virtualCapturing = enable;
    }
}