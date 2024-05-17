using UnityEngine;

namespace CrystalProject
{
    public class CameraResolutionController : MonoBehaviour
    {
        [SerializeField] private Vector2 _minTargetAspectRatio;
        [SerializeField] private Vector2 _maxTargetAspectRatio;
        [SerializeField] private Vector2 _defaultRectSize = new Vector2(1, 1);
        [SerializeField] private Vector2 _rectCenter = new Vector2(0.5f, 0.5f);
        [SerializeField] private float _targetViewSize = 6.5f;
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

        private void SetCamera()
        {
            _previousRatio = (float)Screen.width / (float)Screen.height;
            Debug.Log($"Current screen ratio: {_previousRatio}.");

            if (_previousRatio > _minTargetAspectRatio.x / _minTargetAspectRatio.y)
            {
                Camera.main.orthographicSize = _targetViewSize;
                float pixelScale = Screen.height / _minTargetAspectRatio.y;
                float targetWidth = pixelScale * _minTargetAspectRatio.x;
                float relativeWidth = targetWidth / Screen.width;
                Vector2 rectSize = new Vector2(relativeWidth, _defaultRectSize.y);
                Camera.main.rect = new Rect(default, rectSize) { center = _rectCenter };
                Debug.Log($"New camera rect: {rectSize}.");
            }
            else if (_previousRatio < _maxTargetAspectRatio.x / _maxTargetAspectRatio.y)
            {
                SetRelariveCameraViewSize();
                float pixelScale = Screen.width / _maxTargetAspectRatio.x;
                float targetHeight = pixelScale * _maxTargetAspectRatio.y;
                float relativeHeight = targetHeight / Screen.height;
                Vector2 rectSize = new Vector2(_defaultRectSize.x, relativeHeight);
                Camera.main.rect = new Rect(default, rectSize) { center = _rectCenter };
                Debug.Log($"New camera rect: {rectSize}.");
            }
            else
            {
                SetRelariveCameraViewSize();
            }
        }

        private void SetRelariveCameraViewSize()
        {
            float relativeViewSize = _targetViewSize * (_maxTargetAspectRatio.y / _maxTargetAspectRatio.x) / (_minTargetAspectRatio.y / _minTargetAspectRatio.x);
            Camera.main.orthographicSize = relativeViewSize;
            Camera.main.rect = new Rect(default, _defaultRectSize) { center = _rectCenter };
        }
    }
}

