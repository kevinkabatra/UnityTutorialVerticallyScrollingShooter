namespace UnityLayer.Common.Position
{
    using UnityEngine;

    public class ObjectPositionHandler
    {
        private readonly float _screenWidthInUnits;
        private readonly float _screenHeightInUnits;

        private readonly GameObject _gameObject;
        private SpriteRenderer _spriteRenderer;

        public ObjectPositionHandler(GameObject gameObject, float screenWidthInUnits = 16f, float screenHeightInUnits = 12f)
        {
            _gameObject = gameObject;
            _screenWidthInUnits = screenWidthInUnits;
            _screenHeightInUnits = screenHeightInUnits;
        }

        private void Start()
        {
            _spriteRenderer = _gameObject.GetComponent<SpriteRenderer>();
        }

        public Vector2 UpdatePosition()
        {
            var updatedPosition = AdjustPositionToKeepObjectOnScreen(GetPositionInUnits());
            return updatedPosition;
        }

        public Vector2 UpdatePositionX()
        {
            var expectedPosition = new Vector2
            {
                x = GetPositionInUnits().x,
                y = _gameObject.transform.position.y
            };

            var updatedPosition = AdjustPositionToKeepObjectOnScreen(expectedPosition);
            return updatedPosition;
        }

        public Vector2 UpdatePositionY()
        {
            var expectedPosition = new Vector2
            {
                x = _gameObject.transform.position.x,
                y = GetPositionInUnits().y
            };

            var updatedPosition = AdjustPositionToKeepObjectOnScreen(expectedPosition);
            return updatedPosition;
        }

        private Vector2 AdjustPositionToKeepObjectOnScreen(Vector2 expectedPosition)
        {
            var worldUnitsToCenterPointOfSprite = GetWorldUnitsToCenterPointOfSprite();
            
            var minimumPositionXToKeepPaddleOnScreen = 0 + worldUnitsToCenterPointOfSprite.x;
            var maximumPositionXToKeepPaddleOnScreen = _screenWidthInUnits - worldUnitsToCenterPointOfSprite.x;
            var actualPositionX = Mathf.Clamp(expectedPosition.x, minimumPositionXToKeepPaddleOnScreen, maximumPositionXToKeepPaddleOnScreen);

            var minimumPositionYToKeepPaddleOnScreen = 0 + worldUnitsToCenterPointOfSprite.y;
            var maximumPositionYToKeepPaddleOnScreen = _screenHeightInUnits - worldUnitsToCenterPointOfSprite.y;
            var actualPositionY = Mathf.Clamp(expectedPosition.y, minimumPositionYToKeepPaddleOnScreen, maximumPositionYToKeepPaddleOnScreen);

            return new Vector2
            {
                x = actualPositionX,
                y = actualPositionY
            };
        }

        private Vector2 GetPositionInUnits()
        {
            var inputPositionX = 0f;
            var inputPositionY = 0f;

            // Touch support input
            if (Input.touchSupported && Input.touchCount != 0)
            {
                inputPositionX = Input.GetTouch(0).position.x;
                inputPositionY = Input.GetTouch(0).position.y;
            }

            // Non-touch supported input
            if (inputPositionX == 0f)
            {
                inputPositionX = Input.mousePosition.x;
            }
            if(inputPositionY == 0f)
            {
                inputPositionY = Input.mousePosition.y;
            }

            var inputPositionXInUnits = (inputPositionX / Screen.width) * _screenWidthInUnits;
            var inputPositionYInUnits = (inputPositionY / Screen.height) * _screenHeightInUnits;
            return new Vector2
            {
                x = inputPositionXInUnits,
                y = inputPositionYInUnits
            };
        }

        private Vector2 GetWorldUnitsToCenterPointOfSprite()
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