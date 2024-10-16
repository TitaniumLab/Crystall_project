using CrystalProject.EventBus;
using CrystalProject.EventBus.Signals;
using CrystalProject.Score;
using CrystalProject.Units;
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
        private DropModel _dropModel;
        private DropAnimator _dropAnimator;
        private CustomEventBus _eventBus;
        private IUnitDispenser _unitDispenser;
        private IScore _score;
        private bool _canDrop = true;
        public bool CanDrop { get { return _canDrop; } set { _canDrop = value; } }

        #region MONOBEH ////////////////////////////////////////////////
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
            if (Input.GetMouseButtonUp(_mouseButtonIndex) && _canDrop)
            {
                PointerEventData data = new(EventSystem.current) { position = Input.mousePosition };
                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(data, results);

                //  If not on UI -> Drop game unit
                if (results.Count == 0)
                {
                    var dropPos = GetDropPosition();
                    _dropAnimator.Drop(_dropModel.CurUnitTransform, dropPos);
                }
                else // Return to drop pos
                {
                    _dropAnimator.MoveTo(_dropModel.CurUnitTransform, _dropModel.AppearPoint.position);
                }
            }
        }

        // Move to point while holding button
        private void FixedUpdate()
        {
            // If button pressed and not on UI
            if (Input.GetMouseButton(_mouseButtonIndex) && _canDrop)
            {
                PointerEventData data = new(EventSystem.current) { position = Input.mousePosition };
                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(data, results);

                //  If not on UI
                if (results.Count == 0)
                {
                    var dropPos = GetDropPosition();
                    _dropAnimator.MoveTo(_dropModel.CurUnitTransform, dropPos);
                }
            }
        }

        // Unsubscriptions
        private void OnDestroy()
        {
            _eventBus.Unsubscribe<GameStartSignal>(OnGameStart);
            _dropAnimator.OnDropEnd -= GetNextUnit;
        }
        #endregion

        #region METHODS ////////////////////////////////////////////////
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
            int tier = GetRandomUnitTier();
            SetUnitOfTier(tier);
        }

        public void IncreaseUnitTier(int unitShift)
        {
            if (_dropAnimator.CanBeMoved)
            {

                int currentTier = GetCurrentTier();
                PoolCurrentUnit();

                int[] currentTiers = GetCurrentUnitTiers();
                int index = 0;
                for (int i = 0; i < currentTiers.Length; i++)
                {
                    if (currentTiers[i] == currentTier)
                    {
                        index = i;
                        i = currentTiers.Length;
                    }
                }
                int pseudoIndex = (index + unitShift) % currentTiers.Length;
                int nextIndex = pseudoIndex >= 0 ? pseudoIndex : currentTiers.Length + pseudoIndex;
                int nextTier = currentTiers[nextIndex];
                SetUnitOfTier(nextTier);
            }
        }

        public void SetUnitOfTier(int tier)
        {
            _dropModel.CurUnitPreview?.DisablePreviewState();
            _dropModel.SetNewUnit(_unitDispenser.GetUnit(tier).transform);
            _dropModel.CurUnitPreview.EnablePreviewState();
            _dropAnimator.AppearAnimation(_dropModel.CurUnitTransform, _dropModel.AppearPoint.position);
        }

        public void PoolCurrentUnit()
        {
            if (_dropModel.CurUnitTransform.TryGetComponent(out Units.IPoolable unit))
            {
                unit.PoolIt();
            }
        }

        public int GetCurrentTier()
        {
            int currentTier;
            if (_dropModel.CurUnitTransform.TryGetComponent(out Unit unit))
            {
                currentTier = unit.UnitTier;
            }
            else
            {
                throw new Exception($"Missing {typeof(Unit)} component.");
            }
            return currentTier;
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
            var dropUnitTiers = GetCurrentUnitTiers();
            int index;
            if (dropUnitTiers.Length > 0)
                index = Random.Range(0, dropUnitTiers.Length);
            else
                throw new Exception("Can't get game unit tier.");
            return dropUnitTiers[index];
        }

        public int[] GetCurrentUnitTiers()
        {
            List<int> dropUnitTiers = new List<int>();
            for (int i = 0; i < _dropModel.DropData.Length; i++)
            {
                var data = _dropModel.DropData[i];
                if (data.CanBeDropped && _score.Score >= data.ScoreToDrop)
                    dropUnitTiers.Add(i);
            }
            return dropUnitTiers.ToArray();
        }
        #endregion
    }
}