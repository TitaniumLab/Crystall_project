using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CrystalProject.Audio
{
    public class SoundCheck : MonoBehaviour, ISoundChecker, IPointerUpHandler
    {
        public ISoundCheckable SoundChecker { get; set; }

        private void OnDestroy()
        {
            SoundChecker = null;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            SoundChecker?.SoundCheck();
        }
    }
}