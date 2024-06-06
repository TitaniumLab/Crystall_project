using CrystalProject.EventBus;
using CrystalProject.EventBus.Signals;
using CrystalProject.Score;
using CrystalProject.Units.Create;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace CrystalProject.Dropper
{
    /// <summary>
    /// Control and coordination of the game unit on dropping.
    /// </summary>
    [RequireComponent(typeof(DropModel))]
    [RequireComponent(typeof(DropAnimator))]
    public class DropController : MonoBehaviour
    {
        [SerializeField] private int _mouseButtonIndex; // Input mouse button index
        [SerializeField] private int _uILayerIndex = 5;
        [SerializeField] private float _checkDis = 100;
        private DropModel _dropModel;
        private DropAnimator _dropAnimator;
        private CustomEventBus _eventBus;
        private IUnitDispenser _unitDispenser;
        private IScore _score;

        #region MonoBeh
        // Getting instances of classes and subscriptions
        private void Awake()
        {
            if (!TryGetComponent(out _dropModel))
                throw new Exception($"Missing {typeof(DropModel).Name} component.");
            if (!TryGetComponent(out _dropAnimator))
                throw new Exception($"Missing {typeof(DropAnimator).Name} component.");

            _eventBus.Subscribe<GameStartSignal>(OnGameStart);
            _dropAnimator.OnDropEnd += GetNextUnit;
        }

        // Drop on button up
        private void Update()
        {
            //if (Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, _checkDis, 1 << _uILayerIndex))
            //    Debug.Log("rqewr" + (_uILayerIndex));
            if (EventSystem.current.RaycastAll()
            if (Input.GetMouseButtonUp(_mouseButtonIndex))
            {
                Debug.Log(Input.mousePosition);
                var dropPos = GetDropPosition();
                _dropAnimator.Drop(_dropModel.CurUnitTransform, dropPos);

            }
        }

        // Move to point while holding button
        private void FixedUpdate()
        {
            if (Input.GetMouseButton(_mouseButtonIndex))
            {
                var dropPos = GetDropPosition();
                _dropAnimator.MoveTo(_dropModel.CurUnitTransform, dropPos);
            }
        }

        // Unsubscriptions
        private void OnDestroy()
        {
            _eventBus.Unsubscribe<GameStartSignal>(OnGameStart);
            _dropAnimator.OnDropEnd -= GetNextUnit;
        }
        #endregion

        #region Methods
        [Inject] // Dependency injection
        private void Construct(CustomEventBus eventBus, IUnitDispenser unitDispenser, IScore score)
        {
            _eventBus = eventBus;
            _unitDispenser = unitDispenser;
            _score = score;
        }

        /// <summary>
        /// Game start action.
        /// </summary>
        /// <param name="signal"></param>
        private void OnGameStart(GameStartSignal signal)
        {
            GetNextUnit();
        }

        /// <summary>
        /// Release the current unit and get a new one.
        /// </summary>
        private void GetNextUnit()
        {
            _dropModel.CurUnitPreview?.DisablePreviewState();
            int tier = GetRandomUnitTier();
            _dropModel.SetNewUnit(_unitDispenser.GetUnit(tier).transform);
            _dropModel.CurUnitPreview.EnablePreviewState();
            _dropAnimator.AppearAnimation(_dropModel.CurUnitTransform, _dropModel.AppearPoint.position);
        }

        /// <summary>
        /// Calculate final position for game unit.
        /// </summary>
        /// <returns>Final position.</returns>
        private Vector3 GetDropPosition()
        {
            float xPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            if (xPos < _dropModel.MinXValue)
                xPos = _dropModel.MinXValue;
            else if (xPos > _dropModel.MaxXValue)
                xPos = _dropModel.MaxXValue;
            Vector3 dropPoint = new Vector3(xPos, _dropModel.DropHeight);
            return dropPoint;
        }

        /// <summary>
        /// Return available Tier.
        /// </summary>
        /// <returns>Tier</returns>
        /// <exception cref="Exception"></exception>
        private int GetRandomUnitTier()
        {
            List<int> dropUnitTiers = new List<int>();
            for (int i = 0; i < _dropModel.DropData.Length; i++)
            {
                var data = _dropModel.DropData[i];
                if (data.CanBeDropped && _score.Score >= data.ScoreToDrop)
                    dropUnitTiers.Add(i);
            }
            int index;
            if (dropUnitTiers.Count > 0)
                index = Random.Range(0, dropUnitTiers.Count);
            else
                throw new Exception("Can't get game unit tier.");
            return dropUnitTiers[index];
        }
        #endregion
    }
}