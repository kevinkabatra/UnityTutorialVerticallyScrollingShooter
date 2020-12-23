namespace UnityLayer.Common.Position
{
    using UnityEngine;

    public class ObjectPositionHandler
    {
        private readonly float _screenWidthInUnits;
        private readonly float _screenHeightInUnits;
        private readonly Vector2 _worldUnitsToCenterPointOfSprite;

        private readonly GameObject _gameObject;

        public ObjectPositionHandler(GameObject gameObject)
        {
            _gameObject = gameObject;

            var spriteRenderer = _gameObject.GetComponent<SpriteRenderer>();
            var spritePositionHandler = new SpritePositionHandler(spriteRenderer);
            _worldUnitsToCenterPointOfSprite = spritePositionHandler.GetWorldUnitsToCenterPointOfSprite();

            var backgroundSize = GameObject.FindWithTag("Background").transform.localScale;
            _screenWidthInUnits = backgroundSize.x;
            _screenHeightInUnits = backgroundSize.y;
        }

        public Vector2 UpdatePosition(Vector2 changeInPosition, bool positionCoordinatesAreDelta)
        {
            //var updatedPosition = AdjustPositionToKeepObjectOnScreen(GetPositionInUnits());
            var updatedPosition = AdjustPositionToKeepObjectOnScreen(changeInPosition, positionCoordinatesAreDelta);
            return updatedPosition;
        }

        public Vector2 UpdatePositionX(Vector2 changeInPosition, bool positionCoordinatesAreDelta)
        {
            var expectedPosition = new Vector2
            {
                x = changeInPosition.x,
                y = _gameObject.transform.position.y
            };

            var updatedPosition = AdjustPositionToKeepObjectOnScreen(expectedPosition, positionCoordinatesAreDelta);
            return updatedPosition;
        }

        public Vector2 UpdatePositionY(Vector2 changeInPosition, bool positionCoordinatesAreDelta)
        {
            var expectedPosition = new Vector2
            {
                x = _gameObject.transform.position.x,
                y = changeInPosition.y
            };

            var updatedPosition = AdjustPositionToKeepObjectOnScreen(expectedPosition, positionCoordinatesAreDelta);
            return updatedPosition;
        }

        private Vector2 AdjustPositionToKeepObjectOnScreen(Vector2 changeInPosition, bool positionCoordinatesAreDelta)
        {
            Vector2 expectedPosition;
            if (positionCoordinatesAreDelta)
            {
                var positionLocalCache = _gameObject.transform.position;
                expectedPosition = new Vector2
                {
                    x = changeInPosition.x + positionLocalCache.x,
                    y = changeInPosition.y + positionLocalCache.y
                };
            }
            else
            {
                expectedPosition = GetPositionInUnits(changeInPosition);
            }

            var minimumPositionXToKeepPaddleOnScreen = _worldUnitsToCenterPointOfSprite.x;
            var maximumPositionXToKeepPaddleOnScreen = _screenWidthInUnits - _worldUnitsToCenterPointOfSprite.x;
            var actualPositionX = Mathf.Clamp(expectedPosition.x, minimumPositionXToKeepPaddleOnScreen, maximumPositionXToKeepPaddleOnScreen);

            var minimumPositionYToKeepPaddleOnScreen = _worldUnitsToCenterPointOfSprite.y;
            var maximumPositionYToKeepPaddleOnScreen = _screenHeightInUnits - _worldUnitsToCenterPointOfSprite.y;
            var actualPositionY = Mathf.Clamp(expectedPosition.y, minimumPositionYToKeepPaddleOnScreen, maximumPositionYToKeepPaddleOnScreen);

            return new Vector2
            {
                x = actualPositionX,
                y = actualPositionY
            };
        }

        private Vector2 GetPositionInUnits(Vector2 changeInPosition)
        {
            var inputPositionXInUnits = (changeInPosition.x / Screen.width) * _screenWidthInUnits;
            var inputPositionYInUnits = (changeInPosition.y / Screen.height) * _screenHeightInUnits;
            return new Vector2
            {
                x = inputPositionXInUnits,
                y = inputPositionYInUnits
            };
        }
    }
}