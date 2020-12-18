using UnityEngine;

public class ParallaxHandler : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        var meshRenderer = GetComponent<MeshRenderer>();
        var backgroundStars = meshRenderer.materials[0];
        backgroundStars.mainTextureOffset = new Vector2
        {
            x = backgroundStars.mainTextureOffset.x,
            y = backgroundStars.mainTextureOffset.y + (Time.deltaTime / 12)
        };
    }
}
