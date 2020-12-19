namespace UnityLayer.Player.Ship.Handler
{
    using Controller;
    using UnityEngine;

    public class ShipPositionHandler : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            //var positionUpdateAction = new UnityAction(() => { UpdatePositionOfShip(newPosition); });
            var controller = VirtualController.Get();
            controller.positionUpdateEvent.AddListener(UpdatePositionOfShip);
        }

        private void UpdatePositionOfShip(Vector2 newPosition)
        {
            transform.position = newPosition;
        }
    }    
}