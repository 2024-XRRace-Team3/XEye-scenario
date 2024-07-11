using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PencilController : MonoBehaviour
{
    public Transform penPoint;
    
    
    public GameObject originalTrailPrefab; // Trail Renderer 프리팹을 Inspector에서 설정할 수 있도록 변수 선언

    public GameObject currentTrailObj = null;
    // long time particle;
    // Input Action Asset에서 오른쪽 컨트롤러의 Select 액션을 참조합니다.
    public InputActionReference rightControllerSelectAction;

    private void OnEnable()
    {
        if (rightControllerSelectAction != null)
        {
            rightControllerSelectAction.action.started += OnSelectStarted;
            rightControllerSelectAction.action.performed += OnSelectPerformed;
            rightControllerSelectAction.action.canceled += OnSelectCanceled;
        }
    }

    private void OnDisable()
    {
        if (rightControllerSelectAction != null)
        {
            rightControllerSelectAction.action.started -= OnSelectStarted;
            rightControllerSelectAction.action.performed -= OnSelectPerformed;
            rightControllerSelectAction.action.canceled -= OnSelectCanceled;
        }
    }

    private void OnSelectStarted(InputAction.CallbackContext context)
    {
        Debug.Log("Right controller Select action started.");
        CreateNewTrail();
    }

    private void OnSelectPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Right controller Select action performed.");
    }

    private void OnSelectCanceled(InputAction.CallbackContext context)
    {
        Debug.Log("Right controller Select action canceled.");
        DeactivateTrail();
    }
    
    
    

    void CreateNewTrail()
    {
        if (currentTrailObj == null) // 현재 Trail이 없을 때만 새로 생성
        {
            currentTrailObj = Instantiate(originalTrailPrefab, penPoint.transform);
        }
    }

    void DeactivateTrail()
    {
        if (currentTrailObj != null)
        {
            currentTrailObj.transform.parent = null;
            currentTrailObj = null;
        }
    }
}
