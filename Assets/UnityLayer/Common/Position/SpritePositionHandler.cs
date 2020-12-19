namespace UnityLayer.Common.Position
{
    using UnityEngine;

    public class SpritePositionHandler
    {
        private readonly SpriteRenderer _spriteRenderer;

        public SpritePositionHandler(SpriteRenderer spriteRenderer)
        {
            _spriteRenderer = spriteRenderer;
        }

        public Vector2 GetWorldUnitsToCenterPointOfSprite()
        {
            var sprite = _spriteRenderer.sprite;

            var worldUnitsToXCenterPointOfSprite = sprite.rect.center.x / sprite.pixelsPerUnit;
            var worldUnitsToYCenterPointOfSprite = sprite.rect.center.y / sprite.pixelsPerUnit;

            return new Vector2
            {
                x = worldUnitsToXCenterPointOfSprite,
                y = worldUnitsToYCenterPointOfSprite
            };
        }
    }
}