using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
#if YG_TEXT_MESH_PRO
using TMPro;
#endif

namespace YG
{
    [HelpURL("https://www.notion.so/PluginYG-d457b23eee604b7aa6076116aab647ed#7f075606f6c24091926fa3ad7ab59d10")]
    public class LBPlayerDataYG : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _rank, _name, _score;
        [SerializeField] private Image _rankImg, _nameImg, _scoreImg;
        [SerializeField, Tooltip("Elements color of this player.")]
        private Color _thisPlayerColor;


        public void UpdateValues(int rank, string playerName, int score, bool thisPlayer)
        {

            _rank.text = rank.ToString();
            _name.text = playerName;
            _score.text = score.ToString();

            if (thisPlayer)
            {
                _rankImg.color = _thisPlayerColor;
                _nameImg.color = _thisPlayerColor;
                _scoreImg.color = _thisPlayerColor;
            }
        }
    }
}