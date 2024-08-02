using UnityEngine;
using UnityEngine.UI;

namespace CrystalProject.Game
{
    public class SpecialActionsController : MonoBehaviour
    {
        [SerializeField] private GameObject _crystalsParent;

        [Header("Shake actions")]
        [SerializeField] private Button _shakeCrystallsButton;
        [SerializeField] private DragToShake _shakeBlockScreen;
        [SerializeField] private Image _shakeDirectionImg;
        private Vector3 _pos;

        [Header("Other actions")]
        [SerializeField] private Button _okButton;
        [SerializeField] private Button _cancelButton;

        private void Awake()
        {
            _shakeBlockScreen.OnDirectionChanged += DragDisplay;
        }

        public void StartShake()
        {
            _shakeCrystallsButton.gameObject.SetActive(false);
            _cancelButton.gameObject.SetActive(true);
            _shakeBlockScreen.gameObject.SetActive(true);
        }

        public void StopShake()
        {
            _shakeCrystallsButton.gameObject.SetActive(true);
            _cancelButton.gameObject.SetActive(false);
            _shakeBlockScreen.gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawCube(_pos, Vector3.one);
        }

        private void DragDisplay(Vector2 direction)
        {
            Vector3 relativeDirection = (Vector3)direction + new Vector3(0, 0, _shakeDirectionImg.transform.position.z);
            _shakeDirectionImg.rectTransform.up = direction;
        }
    }
}

