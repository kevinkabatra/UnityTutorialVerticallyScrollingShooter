namespace UnityLayer.Player.Ship.Handler
{
    using Common;
    using DataModel;
    using UnityEngine;

    public class ShipAmmunitionHandler : Common<ShipAmmunitionHandler>
    {
        [SerializeField] private GameObject laser;
        [SerializeField] private float fireRate = 5f;

        private const float BaseCoolDown = 1f;
        private float _currentCoolDown = BaseCoolDown;

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
            if (_currentCoolDown > 0)
            {
                var coolDownReduction = Time.deltaTime * fireRate;
                _currentCoolDown -= coolDownReduction;
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
            _currentCoolDown = BaseCoolDown;
        }

        private void SetShipPositionForFiring()
        {
            _shipCurrentPosition = _ship.transform.position;
        }
    }
}