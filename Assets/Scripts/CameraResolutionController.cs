using UnityEngine;

namespace CrystalProject
{
    //SCRIPT WORK IN PROGRESS------------------
    public class CameraResolutionController : MonoBehaviour
    {
        private float _defaultViewSize;
        [SerializeField] private float _targetWidth;
        [SerializeField] private float _targetViewSize;

        private void OnValidate()
        {
            _defaultViewSize = Camera.main.orthographicSize;
            float currentSize = Screen.width / Screen.height * Camera.main.orthographicSize;
            if (currentSize < _targetWidth)
            {
                float x = _defaultViewSize / Screen.height;
                float horizontal = x * Screen.width;
                Camera.main.orthographicSize = _defaultViewSize * _targetWidth / horizontal;
                Vector2 size = new Vector2(1, 1);
                Vector2 rectCenter = new Vector2(0.5f, 0.5f);
                Camera.main.rect = new Rect(default, size) { center = rectCenter };
            }
            else
            {
                Camera.main.orthographicSize = _targetViewSize;
                Vector2 rectCenter = new Vector2(0.5f, 0.5f);

                float yValue = Screen.height / _targetViewSize;
                float xValue = yValue * _targetWidth;
                float scale = xValue / Screen.width;
                Vector2 size = new Vector2(scale, 1);
                Camera.main.rect = new Rect(default, size) { center = rectCenter };
            }
        }
    }
}

