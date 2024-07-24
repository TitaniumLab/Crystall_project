using System;
using UnityEngine.Events;
using UnityEngine;
using UnityToolbag;
using YG.Utils.LB;
using System.Collections.Generic;

namespace YG
{
    [DefaultExecutionOrder(-101), HelpURL("https://www.notion.so/PluginYG-d457b23eee604b7aa6076116aab647ed#7f075606f6c24091926fa3ad7ab59d10")]
    public class LeaderboardYG : MonoBehaviour
    {
        [SerializeField, Tooltip("Техническое название соревновательной таблицы")]
        private string nameLB;
        public string NameLb { get; set; }

        [SerializeField, Tooltip("Максимальное кол-во получаемых игроков")]
        private int maxQuantityPlayers = 10;

        [SerializeField, Tooltip("Кол-во получения верхних топ игроков")]
        [Range(1, 20)]
        private int quantityTop = 3;

        [SerializeField, Tooltip("Кол-во получаемых записей возле пользователя")]
        [Range(1, 10)]
        private int quantityAround = 6;

        private enum UpdateLBMethod { Start, OnEnable, DoNotUpdate };
        [SerializeField, Tooltip("Когда следует обновлять лидерборд?\nStart - Обновлять в методе Start.\nOnEnable - Обновлять при каждой активации объекта (в методе OnEnable)\nDoNotUpdate - Не обновлять лидерборд с помощью данного скрипта (подразоумивается, что метод обновления 'UpdateLB' вы будете запускать сами, когда вам потребуется.")]
        private UpdateLBMethod updateLBMethod = UpdateLBMethod.OnEnable;

        [SerializeField, Tooltip("Родительский объект для спавна в нём объектов 'playerDataPrefab'")]
        private Transform rootSpawnPlayersData;
        [SerializeField, Tooltip("Префаб отображаемых данных игрока (объект со компонентом LBPlayerDataYG)")]
        private LBPlayerDataYG playerDataPrefab;

        private enum PlayerPhoto { NonePhoto, Small, Medium, Large };
        [SerializeField, Tooltip("Размер подгружаемых изображений игроков. NonePhoto = не подгружать изображение")]
        private PlayerPhoto playerPhoto = PlayerPhoto.Small;

        private string photoSize;
        private LBPlayerDataYG[] players;

        void Awake()
        {
            if (playerPhoto == PlayerPhoto.NonePhoto)
                photoSize = "nonePhoto";
            if (playerPhoto == PlayerPhoto.Small)
                photoSize = "small";
            else if (playerPhoto == PlayerPhoto.Medium)
                photoSize = "medium";
            else if (playerPhoto == PlayerPhoto.Large)
                photoSize = "large";

            if (updateLBMethod == UpdateLBMethod.Start && YandexGame.initializedLB)
            {
                UpdateLB();
            }

            YandexGame.onGetLeaderboard += OnUpdateLB;
        }


        private void OnEnable()
        {
            if (updateLBMethod == UpdateLBMethod.OnEnable && YandexGame.initializedLB)
            {
                UpdateLB();
            }
        }


        private void OnDestroy()
        {
            YandexGame.onGetLeaderboard -= OnUpdateLB;
        }


        void OnUpdateLB(LBData lb)
        {
            if (lb.technoName == nameLB)
            {
                DestroyLBList();
                SpawnPlayersList(lb);
            }
        }


        private void DestroyLBList()
        {
            if (players is not null && players.Length > 0)
            {
                for (int i = players.Length - 1; i >= 0; i--)
                {
                    Destroy(players[i].gameObject);
                }
            }
        }

        private void SpawnPlayersList(LBData lb)
        {

            int length = lb.players.Length < maxQuantityPlayers ? lb.players.Length : maxQuantityPlayers;

            int[] ranks = new int[length];
            string[] names = new string[length];
            int[] scores = new int[length];
            bool[] isThisPlayer = new bool[length];
            players = new LBPlayerDataYG[length];

            for (int i = 0; i < length; i++)
            {
                players[i] = Instantiate(playerDataPrefab, rootSpawnPlayersData);
                ranks[i] = lb.players[i].rank;
                names[i] = LBMethods.AnonimName(lb.players[i].name);
                scores[i] = lb.players[i].score;
                isThisPlayer[i] = lb.players[i].uniqueID == YandexGame.playerId ? true : false;
            }

            if (lb.thisPlayer.rank > length)
            {
                ranks[length - 1] = lb.thisPlayer.rank;
                names[length - 1] = LBMethods.AnonimName(YandexGame.playerName);
                scores[length - 1] = lb.thisPlayer.score;
                isThisPlayer[length - 1] = true;
            }

            for (int i = 0; i < length; i++)
            {
                players[i].UpdateValues(ranks[i], names[i], scores[i], isThisPlayer[i]);
            }
        }


        public void UpdateLB()
        {
            YandexGame.GetLeaderboard(nameLB, maxQuantityPlayers, quantityTop, quantityAround, photoSize);
        }
    }
}

