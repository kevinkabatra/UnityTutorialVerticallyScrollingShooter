namespace UnityLayer.Player.Ship.DataModel
{
    using Common;
    using Common.Position;
    using UnityEngine;

    public class Ship : Common<Ship>
    {
        public SpritePositionHandler SpritePositionHandler;

        private void Start()
        {
            SpritePositionHandler = new SpritePositionHandler(GetComponent<SpriteRenderer>());
        }
    }
}