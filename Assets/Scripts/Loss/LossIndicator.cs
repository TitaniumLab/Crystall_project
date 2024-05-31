using System;
using UnityEngine;

namespace CrystalProject.Loss
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(ParticleSystem))]
    public class LossIndicator : MonoBehaviour
    {
        [SerializeField] private Gradient _emissionGradient; // Emission gradiend effect
        [SerializeField] private Gradient _colorGradient; // Color gradient
        [SerializeField] private float _awakeGradValue; // Value of colors on awake
        [SerializeField] private float _pSActiveValue = 0.9f; // Value after which the particle system is activated
        private string _emissionPropertyName = "_EmissionColor";
        private ParticleSystem _particleSystem;
        private MeshRenderer _meshRend;

        private void Awake()
        {
            if (!TryGetComponent(out _meshRend))
                throw new Exception($"Missing {typeof(MeshRenderer).Name} component.");
            if (!TryGetComponent(out _particleSystem))
                throw new Exception($"Missing {typeof(ParticleSystem).Name} component.");

            SetIndicatorValue(_awakeGradValue);
        }

        private void OnDisable()
        {
            _particleSystem.Play();
        }

        /// <summary>
        /// Set color and emission gradient value.
        /// </summary>
        /// <param name="value">Value of gradient.</param>
        public void SetIndicatorValue(float value)
        {
            _meshRend.material.color = _colorGradient.Evaluate(value);
            _meshRend.material.SetColor(_emissionPropertyName, _emissionGradient.Evaluate(value));
            if (value > _pSActiveValue && !_particleSystem.isPlaying)
                _particleSystem.Play();
            else if (value <= _pSActiveValue)
                _particleSystem.Stop();
        }
    }
}




