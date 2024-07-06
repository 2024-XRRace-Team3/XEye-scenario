using System;
using UnityEngine;

namespace Phone
{
    public class PhoneController : MonoBehaviour
    {
        public Transform lookAtTarget; // 녹화를 진행하시는 위치를 지정

        [field: SerializeField] public bool isLookStraight { get; private set; } = false;

        private Camera camera;

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
    }
}