using System;
using System.Xml;
using Objects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Core
{
    public class Game : MonoBehaviour
    {
        public UnityEvent OnRestart;
        public UnityEvent OnMenu;
        public UnityEvent OnLeaderboard;
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject gameUI;
        [SerializeField] private GameObject leaderboards;
        [SerializeField] private Text pointsLabel;
        [SerializeField] private Transform playerRoot;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject gameRoot;
        [SerializeField] private Animator gameLadder1;
        [SerializeField] private Animator gameLadder2;
        [SerializeField] private GameObject menuLadder;
        [SerializeField] private Text playerNameTextField;
        private Ball ballComponent;
        private string playerName;
        private int pointsCount;

        void Start()
        {
            Application.targetFrameRate = 60;
            ResetUI();
            mainMenu.SetActive(true);
        }

        void OnPointEarned()
        {
            pointsCount++;
            Leaderboard.Leaderboard.Record(playerName,pointsCount);

            pointsLabel.text = pointsCount.ToString();
        }

        public void OnShowMenu()
        {
            ResetUI();
            menuLadder.SetActive(true);
            mainMenu.SetActive(true);
            CleanGameState();
        }

        public void OnRestartGame()
        {
            playerName = playerNameTextField.text == String.Empty ? "playerName" : playerNameTextField.text;
            gameRoot.SetActive(true);
            ballComponent = Instantiate(playerPrefab, playerRoot).GetComponent<Ball>();
            ballComponent.OnBounce.AddListener(OnPointEarned);
            ballComponent.OnCollidedWithEnemy.AddListener(OnLost);
            pointsCount = 0;
            pointsLabel.text = "0";
            ResetUI();
            gameUI.SetActive(true);
            menuLadder.SetActive(false);
            Time.timeScale = 1f;
            OnRestart?.Invoke();
        }

        public void OnShowLeaderboard()
        {
            ResetUI();
            leaderboards.SetActive(true);
        }

        void OnLost()
        {
            CleanGameState();
            ResetUI();
            OnRestartGame();
        }

        void CleanGameState()
        {
            ballComponent.OnBounce.RemoveListener(OnPointEarned);
            ballComponent.OnCollidedWithEnemy.RemoveListener(OnLost);
            if(playerRoot.childCount>0)
                Destroy(playerRoot.GetChild(0).gameObject);
            pointsCount = 0;
            gameRoot.SetActive(false);
        }
        

        void ResetUI()
        {
            mainMenu.SetActive(false);
            gameUI.SetActive(false);
            leaderboards.SetActive(false);
        }
    }
}