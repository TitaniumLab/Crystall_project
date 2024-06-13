using CrystalProject.Dropper;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CrystalProject.Score
{
    [RequireComponent(typeof(Slider))]
    public class ScoreThreshold : MonoBehaviour
    {
        [Header("Threshold options")]
        [SerializeField] private Image _thresholdImg;
        [SerializeField] private Vector2 _threMinAnchor, _threMaxAnchor;
        [SerializeField] private float _threImgYOffset;
        [Header("GameUnit options")]
        [SerializeField] private float _gameUnitSize;
        [SerializeField] private Vector2 _imgMinAnchor, _imgMaxAnchor;
        [Header("Animation options")]
        [SerializeField] private float _sliderAnimDur;
        [SerializeField] private float _imgShakeDur;
        [SerializeField] private float _shakeSize;
        [SerializeField] private ParticleSystem _particleSystem;
        private Slider _slider;
        private RectTransform _rectTransform;
        private IDropData[] _data;
        private IScoreThresholdImages[] _unitImg;
        private int _maxScore = 0;
        private int _minScore = 0;
        private List<ScoreThresholdForAnimation> _scoreThresAnims;

        /// <summary>
        /// Contains info about each threshold point
        /// </summary>
        private class ScoreThresholdForAnimation
        {
            public readonly int ScoreThreshold;
            public readonly Image ThresholdImg;
            public readonly Image GameUnitImg;
            public bool AnimPlayed { get; set; }

            public ScoreThresholdForAnimation(int scoreThreshold, Image threshold, Image gameUnit, bool animPlayed = false)
            {
                ScoreThreshold = scoreThreshold;
                ThresholdImg = threshold;
                GameUnitImg = gameUnit;
                AnimPlayed = animPlayed;
            }
        }

        [Inject] // Dependency injection
        private void Construct(IDropData[] data, IScoreThresholdImages[] unitImg)
        {
            _data = data;
            _unitImg = unitImg;
        }

        private void Awake()
        {
            if (!TryGetComponent(out _slider))
                Debug.LogError($"Missing {typeof(Slider)} component.");
            if (!TryGetComponent(out _rectTransform))
                Debug.LogError($"Missing {typeof(RectTransform)} component.");

            // Get score data
            List<int> scores = new List<int>();
            List<Image> imgs = new List<Image>();
            _scoreThresAnims = new List<ScoreThresholdForAnimation>();
            for (int i = 0; i < _data.Length; i++)
            {
                // If can be dropped and score to drop more then _minScore
                if (_data[i].CanBeDropped && _data[i].ScoreToDrop > _minScore)
                {
                    scores.Add(_data[i].ScoreToDrop);
                    imgs.Add(_unitImg[i].ThresholdUnitImg);
                    // Get maximum value of score slider
                    if (_data[i].ScoreToDrop > _maxScore)
                        _maxScore = _data[i].ScoreToDrop;
                }
            }

            for (int i = 0; i < scores.Count; i++)
            {
                // Create and set threshold image
                var threshold = Instantiate(_thresholdImg, _slider.transform);
                float relativeTresSize = _rectTransform.sizeDelta.y / threshold.rectTransform.sizeDelta.y;
                float position = _rectTransform.rect.width * ((float)scores[i] / (float)_maxScore);
                threshold.rectTransform.localScale = new Vector3(relativeTresSize, relativeTresSize, relativeTresSize);
                threshold.rectTransform.anchorMin = _threMinAnchor;
                threshold.rectTransform.anchorMax = _threMaxAnchor;
                threshold.rectTransform.anchoredPosition = new Vector2(position, _threImgYOffset);

                // Create and set Game Unit image
                var gameUnit = Instantiate(imgs[i], threshold.transform);
                float relativeGameUSize = _gameUnitSize * (threshold.rectTransform.sizeDelta.y / gameUnit.rectTransform.sizeDelta.y);
                gameUnit.rectTransform.localScale = new Vector3(relativeGameUSize, relativeGameUSize, relativeGameUSize);
                gameUnit.rectTransform.anchorMin = _imgMinAnchor;
                gameUnit.rectTransform.anchorMax = _imgMaxAnchor;

                // Create threshold info container
                var scoreThresAnim = new ScoreThresholdForAnimation(scores[i], threshold, gameUnit);
                _scoreThresAnims.Add(scoreThresAnim);
            }

            // Set slider values
            _slider.maxValue = _maxScore;
            _slider.minValue = _minScore;
            _slider.value = _minScore;

            _slider.onValueChanged.AddListener(delegate { OnScoreChange(); });
        }

        private void OnDestroy()
        {
            _slider.onValueChanged.RemoveListener(delegate { OnScoreChange(); });
        }

        public void SetSliderValue(float value)
        {
            _slider.DOValue(value, _sliderAnimDur);
        }

        private void OnScoreChange()
        {
            for (int i = 0; i < _scoreThresAnims.Count; i++)
            {
                if (!_scoreThresAnims[i].AnimPlayed && _slider.value >= _scoreThresAnims[i].ScoreThreshold)
                {
                    _particleSystem.transform.position = _scoreThresAnims[i].GameUnitImg.rectTransform.position;
                    _particleSystem.Play();
                    var seq = DOTween.Sequence();
                    var rT = _scoreThresAnims[i].GameUnitImg.rectTransform;
                    Vector3 size = rT.localScale;
                    seq.Append(rT.DOScale(size * _shakeSize, _imgShakeDur / 2).SetEase(Ease.OutBounce));
                    seq.Append(rT.DOScale(size, _imgShakeDur / 2).SetEase(Ease.InBounce));
                    _scoreThresAnims[i].AnimPlayed = true;
                }
            }
        }
    }
}
