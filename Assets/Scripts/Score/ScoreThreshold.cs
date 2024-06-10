using CrystalProject.Dropper;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using static UnityEditor.Progress;

namespace CrystalProject.Score
{
    [RequireComponent(typeof(Slider))]
    public class ScoreThreshold : MonoBehaviour
    {
        private Slider _slider;
        [Header("Threshold options")]
        [SerializeField] private Image _thresholdImg;
        [SerializeField] private Vector2 _threMinAnchor, _threMaxAnchor;
        [SerializeField] private float _threImgYOffset;
        [Header("GameUnit options")]
        [SerializeField] private float _gameUnitSize;
        [SerializeField] private Vector2 _imgMinAnchor, _imgMaxAnchor;
        [Header("Animation options")]
        [SerializeField] private float _animDur;
        private RectTransform _rectTransform;
        private IDropData[] _data;
        private IScoreThresholdImages[] _unitImg;
        private int _maxScore;
        private int _minScore = 0;
        private int[] _scoreThresholds;
        private Image[] _gameUnitImgs;

        [Inject]
        private void Construct(IDropData[] data, IScoreThresholdImages[] unitImg)
        {
            _data = data;
            _unitImg = unitImg;
        }

        private void Awake()
        {
            // Get slider
            if (!TryGetComponent(out _slider))
                Debug.LogError($"Missing {typeof(Slider)} component.");
            if (!TryGetComponent(out _rectTransform))
                Debug.LogError($"Missing {typeof(RectTransform)} component.");

            // Get score data
            List<int> scores = new List<int>();
            List<Image> imgs = new List<Image>();
            int maxScore = 0;

            for (int i = 0; i < _data.Length; i++)
            {
                if (_data[i].CanBeDropped && _data[i].ScoreToDrop > 0)
                {
                    scores.Add(_data[i].ScoreToDrop);
                    imgs.Add(_unitImg[i].ThresholdUnitImg);
                    if (_data[i].ScoreToDrop > maxScore)
                        maxScore = _data[i].ScoreToDrop; // Get maximum value of score slider
                }
            }

            _scoreThresholds = scores.ToArray();
            _gameUnitImgs = imgs.ToArray();
            _maxScore = maxScore;

            // Set slider values
            _slider.maxValue = _maxScore;
            _slider.value = _minScore;

            IstantiateThresholdImages();
        }

        private void IstantiateThresholdImages()
        {
            for (int i = 0; i < _scoreThresholds.Length; i++)
            {
                // Create and set threshold image
                var threshold = Instantiate(_thresholdImg, _slider.transform);
                float relativeTresSize = _rectTransform.sizeDelta.y / threshold.rectTransform.sizeDelta.y;
                float position = _rectTransform.sizeDelta.x * ((float)_scoreThresholds[i] / (float)_maxScore);
                threshold.rectTransform.localScale = new Vector3(relativeTresSize, relativeTresSize, relativeTresSize);
                threshold.rectTransform.anchorMin = _threMinAnchor;
                threshold.rectTransform.anchorMax = _threMaxAnchor;
                threshold.rectTransform.anchoredPosition = new Vector2(position, _threImgYOffset);

                // Create and set Game Unit image
                var gameUnit = Instantiate(_gameUnitImgs[i], threshold.transform);
                float relativeGameUSize = _gameUnitSize * (threshold.rectTransform.sizeDelta.y / gameUnit.rectTransform.sizeDelta.y);
                gameUnit.rectTransform.localScale = new Vector3(relativeGameUSize, relativeGameUSize, relativeGameUSize);
                gameUnit.rectTransform.anchorMin = _imgMinAnchor;
                gameUnit.rectTransform.anchorMax = _imgMaxAnchor;
            }
        }

        public void SetSliderValue(float value)
        {
            _slider.DOValue(value, _animDur);
        }
    }
}
