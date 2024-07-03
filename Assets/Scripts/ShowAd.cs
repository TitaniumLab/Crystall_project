using UnityEngine;
using YG;

namespace CrystalProject.UI
{
    public static class ShowAd
    {
        public static void ShowAdWithChance(float chance = 1)
        {
            float randNum = Random.Range(0, 1);
            if (randNum <= chance)
            {
                YandexGame.FullscreenShow();
            }
        }
    }
}