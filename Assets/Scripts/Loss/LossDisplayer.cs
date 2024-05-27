using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

namespace CrystalProject.Loss
{
    [RequireComponent(typeof(MeshRenderer))]
    public class LossDisplayer : MonoBehaviour
    {
        [SerializeField] private float _minValue;
        [SerializeField] private float _maxValue = 1;
        [SerializeField] private float _sensitivity;
        [SerializeField] private float _value;
        [SerializeField] private MeshRenderer _material;
        [SerializeField] private Gradient _emissionGradient;
        [SerializeField] private Gradient _colorGradient;
        [SerializeField] private float gradient;
        private Color _defaultColor;
        private string _colorPropertyName = "_EmissionColor";
        [SerializeField] private Color _finalColor;

        private void OnValidate()
        {

        }

        private void Awake()
        {
            _material = GetComponent<MeshRenderer>();


        }
        private void Update()
        {
            _material.material.color = _colorGradient.Evaluate(_value);
            _material.material.SetColor("_EmissionColor", _emissionGradient.Evaluate(gradient) * _value);
            _value += Time.deltaTime;
            if (_value > 1)
                _value = 0;
        }
    }
}

