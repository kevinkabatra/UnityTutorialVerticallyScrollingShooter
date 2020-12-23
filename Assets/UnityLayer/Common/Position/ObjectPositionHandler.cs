namespace UnityLayer.Common.Position
{
    using UnityEngine;

    /// <summary>
    ///     Handler to control the position of any GameObject with a sprite.
    /// </summary>
    public class ObjectPositionHandler
    {
        private readonly float _screenWidthInUnits;
        private readonly float _screenHeightInUnits;
        private readonly Vector2 _worldUnitsToCenterPointOfSprite;

        private readonly GameObject _gameObject;

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="gameObject">The Game Object whose position will be controlled.</param>
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

        /// <summary>
        ///     Determines the new position that the Game Object should be in, will ensure that the object remains on the screen.
        /// </summary>
        /// <param name="changeInPosition">The change in position.</param>
        /// <param name="positionCoordinatesAreDelta">Should the change in position be added to the previous position or should it be replaced by it?</param>
        /// <returns></returns>
        public Vector2 UpdatePosition(Vector2 changeInPosition, bool positionCoordinatesAreDelta)
        {
            var updatedPosition = AdjustPositionToKeepObjectOnScreen(changeInPosition, positionCoordinatesAreDelta);
            return updatedPosition;
        }

        /// <summary>
        ///     Determines the new position that the Game Object should be in, will ensure that the object remains on the screen.
        /// </summary>
        /// <param name="changeInPosition">The change in position.</param>
        /// <param name="positionCoordinatesAreDelta">Should the change in position be added to the previous position or should it be replaced by it?</param>
        /// <returns></returns>
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

        /// <summary>
        ///     Determines the new position that the Game Object should be in, will ensure that the object remains on the screen.
        /// </summary>
        /// <param name="changeInPosition">The change in position.</param>
        /// <param name="positionCoordinatesAreDelta">Should the change in position be added to the previous position or should it be replaced by it?</param>
        /// <returns></returns>
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

        /// <summary>
        ///     Adjusts the expected change in position so that the new position will enable the game object to remain on the screen.
        /// </summary>
        /// <param name="changeInPosition">The change in position.</param>
        /// <param name="positionCoordinatesAreDelta">Should the change in position be added to the previous position or should it be replaced by it?</param>
        /// <returns></returns>
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

        /// <summary>
        ///     Converts the change in position to world units, only used with non-delta based positioning (mouse).
        /// </summary>
        /// <param name="changeInPosition">The change in position.</param>
        /// <returns></returns>
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