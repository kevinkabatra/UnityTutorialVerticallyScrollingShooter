namespace UnityLayer.Controller
{
    using Common;
    using Common.Position;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.InputSystem;

    public class VirtualController : Common<VirtualController>
    {
        public UnityEvent<Vector2> positionUpdateEvent = new UnityEvent<Vector2>();

        [SerializeField] private GameObject objectToBeControlled;

        private PlayerInput _physicalController;
        private bool _physicalControllerUseDeltaChanges;

        private Vector2 _position;
        private ObjectPositionHandler _positionHandler;

        private void Start()
        {
            _positionHandler = new ObjectPositionHandler(objectToBeControlled);
            _physicalController = FindObjectOfType<PlayerInput>();
            ShouldPhysicalControllerUseDeltaChanges();
        }

        /// <summary>
        ///     Handles controller switching.
        /// </summary>
        /// <remarks>
        ///     This is a Unity Message from the Input System.
        /// </remarks>
        private void OnControlsChanged()
        {
            ShouldPhysicalControllerUseDeltaChanges();
        }

        /// <summary>
        ///     Handles movement.
        /// </summary>
        /// <remarks>
        ///     This is a Unity Message from the Input System.
        /// </remarks>
        private void OnMove(InputValue value)
        {
            var changeInPosition = value.Get<Vector2>();
            var actualNewPosition = _positionHandler.UpdatePosition(changeInPosition, _physicalControllerUseDeltaChanges);

            if (_position == actualNewPosition) return;

            _position = actualNewPosition;
            positionUpdateEvent.Invoke(_position);
        }

        /// <summary>
        ///     The mouse requires the use of its position versus its delta.
        /// </summary>
        private void ShouldPhysicalControllerUseDeltaChanges()
        {
            _physicalControllerUseDeltaChanges = _physicalController.currentControlScheme != ControlSchemes.KeyboardMouse.ToString();
        }
    }
}
