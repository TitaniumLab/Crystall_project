using CrystalProject.Dropper;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CrystalProject.Score
{
    [RequireComponent(typeof(Slider))]
    public class ScoreThreshold : MonoBehaviour
    {
        private Slider _slider;
        [SerializeField] private Image _thresholdImg;
        [SerializeField] private Vector2 _imgMinAnchor, _imgMaxAnchor;
        [SerializeField] private float _imgYOffset;
        private IDropData[] _data;
        private int _maxScore;
        private int _minScore = 0;
        private int[] _scoreThresholds;

        [Inject]
        private void Construct(IDropData[] data)
        {
            _data = data;
        }

        private void Awake()
        {
            if (!TryGetComponent(out _slider))
                throw new Exception($"Missing {typeof(Slider)} component.");

            Init();

            _slider.maxValue = _maxScore;
            _slider.value = _minScore;

            var threshold = Instantiate(_thresholdImg, _slider.transform);
            RectTransform sliderRectT;
            float relativeSize = 0;
            float position = 0;

            if (TryGetComponent(out sliderRectT))
            {
                relativeSize = sliderRectT.sizeDelta.y / threshold.rectTransform.sizeDelta.y;
                position = sliderRectT.sizeDelta.x * ((float)_scoreThresholds[0] / (float)_maxScore);

            }


            Debug.LogWarning(sliderRectT.sizeDelta.x);
            Debug.LogWarning(_scoreThresholds[0]);
            Debug.LogWarning(_maxScore);
            Debug.LogWarning(position);
            threshold.rectTransform.localScale = new Vector3(relativeSize, relativeSize, relativeSize);
            threshold.rectTransform.anchorMin = _imgMinAnchor;
            threshold.rectTransform.anchorMax = _imgMaxAnchor;
            threshold.rectTransform.anchoredPosition = new Vector2(position, _imgYOffset);


            //  = _slider.transform / _thresholdImg.rectTransform.sizeDelta.y;

            // threshold.rectTransform.localScale = new Vector3(relativeSize, relativeSize, relativeSize);

            // threshold.rect.position = position;
            // rectTransform.position = new Vector2(position, 0);

        }

        private void Init()
        {
            List<int> scores = new List<int>();
            int maxScore = 0;
            foreach (var item in _data)
            {
                if (item.CanBeDropped && item.ScoreToDrop > 0)
                {
                    scores.Add(item.ScoreToDrop);
                    if (item.ScoreToDrop > maxScore)
                        maxScore = item.ScoreToDrop;
                }
            }
            _scoreThresholds = scores.ToArray();
            _maxScore = maxScore;
        }
    }
}
