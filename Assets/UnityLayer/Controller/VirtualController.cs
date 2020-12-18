using UnityEngine;
using UnityEngine.Events;

public class VirtualController : Common<VirtualController>
{
    public UnityEvent<Vector2> positionUpdateEvent = new UnityEvent<Vector2>();

    [SerializeField] private GameObject objectToBeControlled;

    private Vector2 position;
    private ObjectPositionHandler positionHandler;

    private void Start()
    {
        positionHandler = new ObjectPositionHandler(objectToBeControlled);
    }

    // Update is called once per frame
    private void Update()
    {
        HandlePositionUpdate();
    }

    private void HandlePositionUpdate()
    {
        var newPosition = positionHandler.UpdatePosition();
        if (position != newPosition)
        {
            position = newPosition;
            positionUpdateEvent.Invoke(position);
        }
    }
}
