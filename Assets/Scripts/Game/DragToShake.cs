using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CrystalProject.SpecialActions
{
    [RequireComponent(typeof(RectTransform))]
    public class DragToShake : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private RectTransform _rectTransform;
        public Action<Vector2> OnDirectionChanged;
        public Action<Vector2> OnMoveStart;
        public Action<Vector2> OnMoveEnd;
        public Action OnCencel;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            var pos = eventData.position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, pos, Camera.main, out Vector2 localPoint);
            OnMoveStart?.Invoke(localPoint);
        }

        public void OnDrag(PointerEventData eventData)
        {
            var direction = eventData.position - eventData.pressPosition;
            OnDirectionChanged?.Invoke(direction);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject != gameObject)
            {
                OnCencel?.Invoke();
            }
            else
            {
                OnMoveEnd?.Invoke(eventData.position - eventData.pressPosition);
            }
        }
    }
}
