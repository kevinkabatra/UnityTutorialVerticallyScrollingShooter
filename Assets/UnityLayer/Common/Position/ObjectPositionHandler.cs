namespace UnityLayer.Common.Position
{
    using UnityEngine;

    public class ObjectPositionHandler
    {
        private readonly float _screenWidthInUnits;
        private readonly float _screenHeightInUnits;
        private readonly Vector2 _worldUnitsToCenterPointOfSprite;

        private readonly GameObject _gameObject;

        public ObjectPositionHandler(GameObject gameObject, float screenWidthInUnits = 16f, float screenHeightInUnits = 12f)
        {
            _gameObject = gameObject;

            var spriteRenderer = _gameObject.GetComponent<SpriteRenderer>();
            var spritePositionHandler = new SpritePositionHandler(spriteRenderer);
            _worldUnitsToCenterPointOfSprite = spritePositionHandler.GetWorldUnitsToCenterPointOfSprite();

            _screenWidthInUnits = screenWidthInUnits;
            _screenHeightInUnits = screenHeightInUnits;
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
    }
}