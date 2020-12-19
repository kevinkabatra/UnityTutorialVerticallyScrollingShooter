namespace UnityLayer.Player.Ship.Handler
{
    using Common;
    using DataModel;
    using UnityEngine;

    public class ShipAmmunitionHandler : Common<ShipAmmunitionHandler>
    {
        [SerializeField] private GameObject laser;
        [SerializeField] private float fireRate = .0001f;

        private float _coolDown = 0f;
        private Ship _ship;
        private Vector2 _shipCurrentPosition;
        private Vector2 _centerPointOfShip;

        private void Start()
        {
            _ship = Ship.Get();
            _centerPointOfShip = _ship.SpritePositionHandler.GetWorldUnitsToCenterPointOfSprite();
        }

        private void Update()
        {
            if (_coolDown > 0)
            {
                _coolDown -= Time.deltaTime;
                Debug.Log(_coolDown);
                return;
            }

            HandleFiring();
        }

        private void FireMainCannon()
        {
            var laserInstance = Instantiate<GameObject>(laser);
            laserInstance.transform.position = new Vector2
            {
                x = _shipCurrentPosition.x,
                y = _shipCurrentPosition.y + _centerPointOfShip.y
            };
        }

        private void HandleFiring()
        {
            SetShipPositionForFiring();
            FireMainCannon();
            _coolDown = fireRate;
        }

        private void SetShipPositionForFiring()
        {
            _shipCurrentPosition = _ship.transform.position;
        }
    }
}