using UnityEngine;

public class ObjectPositionHandler
{
    private readonly float screenWidthInUnits;
    private readonly float screenHeightInUnits;

    private readonly GameObject gameObject;

    public ObjectPositionHandler(GameObject _gameObject, float _screenWidthInUnits = 16f, float _screenHeightInUnits = 12f)
    {
        gameObject = _gameObject;
        screenWidthInUnits = _screenWidthInUnits;
        screenHeightInUnits = _screenHeightInUnits;
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
            y = gameObject.transform.position.y
        };

        var updatedPosition = AdjustPositionToKeepObjectOnScreen(expectedPosition);
        return updatedPosition;
    }

    public Vector2 UpdatePositionY()
    {
        var expectedPosition = new Vector2
        {
            x = gameObject.transform.position.x,
            y = GetPositionInUnits().y
        };

        var updatedPosition = AdjustPositionToKeepObjectOnScreen(expectedPosition);
        return updatedPosition;
    }

    private Vector2 AdjustPositionToKeepObjectOnScreen(Vector2 expectedPosition)
    {
        var worldUnitsToCenterPointOfSprite = GetWorldUnitsToCenterPointOfSprite();
        
        var minimumPositionXToKeepPaddleOnScreen = 0 + worldUnitsToCenterPointOfSprite.x;
        var maximumPositionXToKeepPaddleOnScreen = screenWidthInUnits - worldUnitsToCenterPointOfSprite.x;
        var actualPositionX = Mathf.Clamp(expectedPosition.x, minimumPositionXToKeepPaddleOnScreen, maximumPositionXToKeepPaddleOnScreen);

        var minimumPositionYToKeepPaddleOnScreen = 0 + worldUnitsToCenterPointOfSprite.y;
        var maximumPositionYToKeepPaddleOnScreen = screenHeightInUnits - worldUnitsToCenterPointOfSprite.y;
        var actualPositionY = Mathf.Clamp(expectedPosition.y, minimumPositionYToKeepPaddleOnScreen, maximumPositionYToKeepPaddleOnScreen);

        return new Vector2
        {
            x = actualPositionX,
            y = actualPositionY
        };
    }

    private Vector2 GetPositionInUnits()
    {
        float inputPositionX = 0f;
        float inputPositionY = 0f;

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

        var inputPositionXInUnits = (inputPositionX / Screen.width) * screenWidthInUnits;
        var inputPositionYInUnits = (inputPositionY / Screen.height) * screenHeightInUnits;
        return new Vector2
        {
            x = inputPositionXInUnits,
            y = inputPositionYInUnits
        };
    }

    private Vector2 GetWorldUnitsToCenterPointOfSprite()
    {
        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        var sprite = spriteRenderer.sprite;
        
        var worldUnitsToXCenterPointOfSprite = sprite.rect.center.x / sprite.pixelsPerUnit;
        var worldUnitsToYCenterPointOfSprite = sprite.rect.center.y / sprite.pixelsPerUnit;

        return new Vector2
        {
            x = worldUnitsToXCenterPointOfSprite,
            y = worldUnitsToYCenterPointOfSprite
        };
    }
}
