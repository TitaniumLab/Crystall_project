using DG.Tweening;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        private List<Tween> _rTweens = new List<Tween>();
        private List<Tween> _mTweens = new List<Tween>();

        void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            float time = 1 / _spawnFrequency;
            InvokeRepeating(nameof(CreateRandomAndAnimate), 0, time);
        }

        private async void CreateRandomAndAnimate()
        {
            int index = Random.Range(0, _images.Length);
            var img = Instantiate(_images[index], transform);
            float horizontalSize = (img.rectTransform.sizeDelta.x / img.rectTransform.sizeDelta.y) * _imgSize;
            var size = new Vector2(horizontalSize, _imgSize);
            img.rectTransform.sizeDelta = size;
            float xSize = _rectTransform.rect.width;
            float xPos = Random.Range(-xSize / 2, xSize / 2);
            float yPos = _rectTransform.rect.height / 2;
            var startPos = new Vector2(xPos, yPos);
            img.rectTransform.anchoredPosition = startPos;
            float distance = 2 * yPos;
            float minMoveDur = distance / _fallSpeed.x;
            float maxMoveDur = distance / _fallSpeed.y;
            float moveDur = Random.Range(minMoveDur, maxMoveDur);

            float rotSpeed = Random.Range(_rotationSpeed.x, _rotationSpeed.y);
            float rotAngle = 360 * (rotSpeed / Mathf.Abs(rotSpeed)); // direction
            float rotDur = 360 / Mathf.Abs(rotSpeed);


            Tween rTween = img.rectTransform.DOLocalRotate(new Vector3(0, 0, rotAngle), rotDur, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
            Tween mTween = img.rectTransform.DOMoveY(-yPos, moveDur).SetEase(Ease.Linear);
            _rTweens.Add(rTween);
            _mTweens.Add(mTween);

            await (mTween).AsyncWaitForCompletion();
            _mTweens.Remove(mTween);

            if (img)
                Destroy(img);
        }

        private void OnDestroy()
        {
            foreach (var item in _mTweens)
            {
                item?.Kill();
            }
            foreach (var item in _rTweens)
            {
                item?.Kill();
            }
        }
    }
}