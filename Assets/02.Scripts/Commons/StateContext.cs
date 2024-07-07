using UnityEngine;

namespace Common.State
{
    public interface IState<TController>
    {
        void SetState(TController controller);
    }
    
    public class StateContext<TController> : MonoBehaviour where TController : MonoBehaviour
    {
        private IState<TController> _currentState;
        public IState<TController> currentState
        {
            get => _currentState;
            set
            {
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                _currentState = value;
                _currentState.SetState(_controller);
            }
        }

        private readonly TController _controller;

        public StateContext(TController controller) {
            _controller = controller;
        }
    }
}