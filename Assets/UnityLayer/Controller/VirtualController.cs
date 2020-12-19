namespace UnityLayer.Controller
{
    using Common;
    using Common.Position;
    using UnityEngine;
    using UnityEngine.Events;
    
    public class VirtualController : Common<VirtualController>
    {
        public UnityEvent<Vector2> positionUpdateEvent = new UnityEvent<Vector2>();

        [SerializeField] private GameObject objectToBeControlled;

        private Vector2 _position;
        private ObjectPositionHandler _positionHandler;

        private void Start()
        {
            _positionHandler = new ObjectPositionHandler(objectToBeControlled);
        }

        // Update is called once per frame
        private void Update()
        {
            HandlePositionUpdate();
        }

        private void HandlePositionUpdate()
        {
            var newPosition = _positionHandler.UpdatePosition();
            if (_position == newPosition) return;
            
            _position = newPosition;
            positionUpdateEvent.Invoke(_position);
        }
    }   
}
