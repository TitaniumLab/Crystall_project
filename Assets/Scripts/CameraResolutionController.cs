using UnityEngine;

namespace CrystalProject
{
    public class CameraResolutionController : MonoBehaviour
    {
        [SerializeField] private Vector2 _minTargetAspectRatio = new Vector2(3, 4);
        [SerializeField] private Vector2 _maxTargetAspectRatio = new Vector2(9, 19.5f);
        [SerializeField] private Vector2 _defaultRectSize = new Vector2(1, 1);
        [SerializeField] private Vector2 _rectCenter = new Vector2(0.5f, 0.5f);
        [SerializeField] private float _minHViewSize = 4f;
        [SerializeField] private float _minViewSize = 6.5f;

        private float _previousRatio;

        private void Start()
        {
            SetCamera();
        }

        private void FixedUpdate()
        {
            float currentRario = (float)Screen.width / (float)Screen.height;
            if (_previousRatio != currentRario)
            {
                SetCamera();
            }
        }

        public void SetCamera()
        {
            _previousRatio = (float)Screen.width / (float)Screen.height;

            // If screen higher then target camera view
            if (_previousRatio < _maxTargetAspectRatio.x / _maxTargetAspectRatio.y)
            {
                SetRelariveCameraViewSize(_maxTargetAspectRatio);
#if UNITY_IOS || UNITY_ANDROID || UNITY_WP_8_1
                float pixelScale = Screen.width / _maxTargetAspectRatio.x;
                float targetHeight = pixelScale * _maxTargetAspectRatio.y;
                float relativeHeight = targetHeight / Screen.height;
                Vector2 rectSize = new Vector2(_defaultRectSize.x, relativeHeight);
                Camera.main.rect = new Rect(default, rectSize) { center = _rectCenter };
#endif
            }
            // If screen wider then target camera view
            else if (_previousRatio > _minTargetAspectRatio.x / _minTargetAspectRatio.y)
            {
                SetRelariveCameraViewSize(_minTargetAspectRatio);

#if UNITY_IOS || UNITY_ANDROID || UNITY_WP_8_1
                float pixelScale = Screen.height / _minTargetAspectRatio.y;
                float targetWidth = pixelScale * _minTargetAspectRatio.x;
                float relativeWidth = targetWidth / Screen.width;
                Vector2 rectSize = new Vector2(relativeWidth, _defaultRectSize.y);
                Camera.main.rect = new Rect(default, rectSize) { center = _rectCenter };
#endif
            }
            else
            {
                SetRelariveCameraViewSize(new Vector2(Screen.width, Screen.height));
            }
        }

        private void SetRelariveCameraViewSize(Vector2 targetRatio)
        {
            Camera.main.rect = new Rect(default, _defaultRectSize) { center = _rectCenter };
            float relativeHViewSize = targetRatio.y / (targetRatio.x / _minHViewSize);
            float viewSize = (relativeHViewSize < _minViewSize) ? _minViewSize : relativeHViewSize;
            Camera.main.orthographicSize = viewSize;
        }
    }
}

