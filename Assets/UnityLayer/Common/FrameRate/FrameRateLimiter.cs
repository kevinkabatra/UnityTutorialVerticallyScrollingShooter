namespace UnityLayer.Common.FrameRate
{
    using UnityEngine;

    public class FrameRateLimiter : MonoBehaviour
    {
        private void Start()
        {
            Application.targetFrameRate = 60;
        }
    }
}