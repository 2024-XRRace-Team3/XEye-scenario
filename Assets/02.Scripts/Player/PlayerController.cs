using Manager;
using UnityEngine;

namespace Car
{
    public class PlayerController : MonoBehaviour
    {
        public GameObject phoneObj;
        public bool phoneActive = false; 
        
        void Start()
        {
            GameManager.Instance.Player = this.gameObject;
        }
        void Update()
        {
#if UNITY_EDITOR
            this.phoneObj.SetActive(phoneActive);
#endif
        }
    
    }
}
