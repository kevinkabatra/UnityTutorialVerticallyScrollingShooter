namespace UnityLayer.Player.Ship.Handler
{
    using Common;
    using DataModel;
    using UnityEngine;

    public class ShipAmmunitionHandler : Common<ShipAmmunitionHandler>
    {
        [SerializeField] private GameObject laserTest;

        private Ship _ship;
        private Vector2 _centerPointOfShip;

        private void Start()
        {
            _ship = Ship.Get();
            _centerPointOfShip = _ship.SpritePositionHandler.GetWorldUnitsToCenterPointOfSprite();
        }

        private void Update()
        {
            FireMainCannon();
        }

        private void FireMainCannon()
        {
            var shipCurrentPosition = _ship.transform.position;

            var laserInstance = Instantiate<GameObject>(laserTest);
            laserInstance.transform.position = new Vector3
            {
                x = shipCurrentPosition.x,
                y = shipCurrentPosition.y + _centerPointOfShip.y,
                z = shipCurrentPosition.z - 1
            };
        }
    }
}