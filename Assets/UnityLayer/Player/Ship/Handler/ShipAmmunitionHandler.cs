namespace UnityLayer.Player.Ship.Handler
{
    using Common;
    using UnityEngine;

    public class ShipAmmunitionHandler : Common<ShipAmmunitionHandler>
    {
        [SerializeField] private GameObject laserTest;

        private void Start()
        {
            var testPosition = new Vector3
            {
                x = 8,
                y = 6,
                z = 0
            };

            var laserInstance = Instantiate<GameObject>(laserTest);
            laserInstance.transform.position = testPosition;
        }
    }
}