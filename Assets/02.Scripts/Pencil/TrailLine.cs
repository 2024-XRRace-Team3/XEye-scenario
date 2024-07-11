using System;
using UnityEngine;

public class TrailLine : MonoBehaviour
{
    public TrailRenderer trailPrefab; // Trail Renderer 프리팹을 Inspector에서 설정할 수 있도록 변수 선언

    private TrailRenderer currentTrail; // 현재 활성화된 Trail Renderer


    private void OnEnable()
    {
        throw new NotImplementedException();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) // 마우스 왼쪽 버튼이 눌리면
        {
            CreateNewTrail(); // 새로운 Trail 생성
        }

        if (Input.GetKeyUp(KeyCode.Mouse0)) // 마우스 왼쪽 버튼이 떼지면
        {
            DeactivateTrail(); // 현재 Trail 비활성화
        }
    }

    void CreateNewTrail()
    {
        if (currentTrail == null) // 현재 Trail이 없을 때만 새로 생성
        {
            currentTrail = Instantiate(trailPrefab, transform.position, Quaternion.identity);
        }
    }

    void DeactivateTrail()
    {
        if (currentTrail != null)
        {
            currentTrail.emitting = false; // Trail Renderer를 더 이상 발생하지 않도록 설정
            currentTrail = null; // 현재 Trail 변수 초기화
        }
    }
}