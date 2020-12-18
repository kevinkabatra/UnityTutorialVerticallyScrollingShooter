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
        var updatedPositionX = AdjustPositionToKeepObjectOnScreen(GetPositionInUnits().x);
        var updatedPositionY = AdjustPositionToKeepObjectOnScreen(GetPositionInUnits().y);

        var newPosition = new Vector2(updatedPositionX, updatedPositionY);
        return newPosition;
    }

    public Vector2 UpdatePositionX()
    {
        var updatedPositionX = AdjustPositionToKeepObjectOnScreen(GetPositionInUnits().x);
        
        var newPosition = new Vector2(updatedPositionX, gameObject.transform.position.y);
        return newPosition;

    }

    public Vector2 UpdatePositionY()
    {
        var updatedPositionY = AdjustPositionToKeepObjectOnScreen(GetPositionInUnits().y);
        
        var newPosition = new Vector2(gameObject.transform.position.x, updatedPositionY);
        return newPosition;
    }

    private float AdjustPositionToKeepObjectOnScreen(float expectedPosition)
    {
        var worldUnitsToCenterPointOfSprite = GetWorldUnitsToCenterPointOfSprite();
        var minimumPositionToKeepPaddleOnScreen = 0 + worldUnitsToCenterPointOfSprite;
        var maximumPositionToKeepPaddleOnScreen = screenWidthInUnits - worldUnitsToCenterPointOfSprite;
        var actualPosition = Mathf.Clamp(expectedPosition, minimumPositionToKeepPaddleOnScreen, maximumPositionToKeepPaddleOnScreen);
        return actualPosition;
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

    private float GetWorldUnitsToCenterPointOfSprite()
    {
        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        var sprite = spriteRenderer.sprite;
        var worldUnitsToCenterPointOfSprite = sprite.rect.center.x / sprite.pixelsPerUnit;
        return worldUnitsToCenterPointOfSprite;
    }
}
