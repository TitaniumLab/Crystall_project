using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace CrystalProject.Game
{
    public class DragToShake : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private float _distance;
        public Action<Vector2> OnDirectionChanged;


        public void OnBeginDrag(PointerEventData eventData)
        {
        }

        public void OnDrag(PointerEventData eventData)

        {
            OnDirectionChanged?.Invoke(eventData.position - eventData.pressPosition);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _distance = (eventData.position - eventData.pressPosition).magnitude;
            Debug.Log(_distance);
        }
    }
}
