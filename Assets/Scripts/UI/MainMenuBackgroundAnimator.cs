using UnityEngine;
using UnityEngine.UI;

namespace CrystalProject.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class MainMenuBackgroundAnimator : MonoBehaviour
    {
        [SerializeField] private Image[] _images;
        [SerializeField] private float _imgSize = 20;
        [SerializeField] private float _spawnFrequency = 1.0f;
        [SerializeField] private Vector2 _fallSpeed = new Vector2(1, 2);
        [SerializeField] private Vector2 _rotationSpeed = new Vector2(-2, 2);
        private RectTransform _rectTransform;

        void Start()
        {
            _rectTransform = GetComponent<RectTransform>();

            var img = Instantiate(_images[0], transform);
            float horizontalSize = (img.rectTransform.sizeDelta.x / img.rectTransform.sizeDelta.y) * _imgSize;
            var size = new Vector2(horizontalSize, _imgSize);
            img.rectTransform.sizeDelta = size;

            Debug.Log(_rectTransform.rect.size);
            // Debug.Log(_rectTransform.anchoredPosition);

            float xSize = _rectTransform.rect.width;
            float xPos = Random.Range(-xSize / 2, xSize / 2);
            float yPos = _rectTransform.rect.height / 2;
            var startPos = new Vector2(xPos, yPos);
            img.rectTransform.anchoredPosition = startPos;
        }
    }
}