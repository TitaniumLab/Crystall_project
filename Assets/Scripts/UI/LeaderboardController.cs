using UnityEngine;
using YG;

namespace CrystalProject.UI
{
    public class LeaderboardController : MonoBehaviour
    {
        private void Leader()
        {
            YandexGame.GetLeaderboard("Score", 10, 10, 10, "asdf");
            lbpla


        }
    }
}