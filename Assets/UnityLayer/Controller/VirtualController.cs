namespace UnityLayer.Controller
{
    using Common;
    using Common.Position;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.InputSystem;

    /// <summary>
    ///     Virtual Controller that uses Unity Events to communicate the status of the controller to listeners.
    /// </summary>
    public class VirtualController : Common<VirtualController>
    {
        public UnityEvent<Vector2> positionUpdateEvent = new UnityEvent<Vector2>();

        [SerializeField] private GameObject objectToBeControlled;

        private PlayerInput _physicalController;
        private bool _physicalControllerUseDeltaChanges;

        private Vector2 _position;
        private ObjectPositionHandler _positionHandler;

        private bool _moveControlIsHeld = false;
        private Vector2 _previousPositionDelta = new Vector2();

        /// <summary>
        ///     Initializes the Virtual Controller.
        /// </summary>
        private void Start()
        {
            _positionHandler = new ObjectPositionHandler(objectToBeControlled);
            _physicalController = FindObjectOfType<PlayerInput>();
            ShouldPhysicalControllerUseDeltaChanges();
        }

        /// <summary>
        ///     Handles polling for controller updates.
        /// </summary>
        private void Update()
        {
            if (_physicalControllerUseDeltaChanges && _moveControlIsHeld)
            {
                DoMove(_previousPositionDelta);
            }
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
            DoMove(changeInPosition);
        }

        /// <summary>
        ///     Logic for handling the movement, supports a player holding
        /// the movement control in a set position.
        /// </summary>
        /// <param name="changeInPosition"></param>
        private void DoMove(Vector2 changeInPosition)
        {
            // If using a mouse the position will not continue to change
            if (_physicalControllerUseDeltaChanges)
            {
                _previousPositionDelta = changeInPosition;

                // If a joystick is let go we should stop updating the movement
                // But if a mouse is at position 0,0 we should update the move
                if (changeInPosition == new Vector2())
                {
                    _moveControlIsHeld = false;
                    return;
                }

                _moveControlIsHeld = true;
            }
            else
            {
                _moveControlIsHeld = false;
            }

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
