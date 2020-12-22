namespace UnityLayer.Controller
{
    using Common;
    using Common.Position;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;
    using UnityEngine.InputSystem;

    public class VirtualController : Common<VirtualController>
    {
        public UnityEvent<Vector2> positionUpdateEvent = new UnityEvent<Vector2>();
        public EventTrigger.TriggerEvent testEventTrigger = new EventTrigger.TriggerEvent();

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
            //HandlePositionUpdate();
        }

        /// <summary>
        ///     Handles movement.
        /// </summary>
        /// <remarks>This is a Unity Message from the Input System.</remarks>
        private void OnMove(InputValue value)
        {
            var changeInPosition = value.Get<Vector2>();
            var actualNewPosition = _positionHandler.UpdatePosition(changeInPosition);
            Debug.Log("Change in position: " + changeInPosition + ". Actual position: " + actualNewPosition);

            if (_position == actualNewPosition) return;

            _position = actualNewPosition;
            positionUpdateEvent.Invoke(_position);
        }
    }
}
