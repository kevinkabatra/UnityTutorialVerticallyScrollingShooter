namespace UnityLayer.PlaySpace.Background.Handlers
{
    using UnityEngine;

    public class ParallaxHandler : MonoBehaviour
    {
        public float speed = 1f;

        [SerializeField] private float distance = 1f;

        private MeshRenderer _meshRenderer;
        private Material _backgroundStars;

        // Update is called once per frame
        private void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _backgroundStars = _meshRenderer.materials[0];
        }

        private void Update()
        {
            _backgroundStars.mainTextureOffset = new Vector2
            {
                x = _backgroundStars.mainTextureOffset.x,
                y = _backgroundStars.mainTextureOffset.y + (Time.deltaTime / transform.localScale.y / distance * speed)
            };
        }
    }
}
