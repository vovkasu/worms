using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public TeamPlayers Team1 = new TeamPlayers();
        public TeamPlayers Team2 = new TeamPlayers();

        public TeamPlayers ActivTeam;
        public PlayerControl ActivPlayer;
        public TextState TextState;
        public PlayerControl PlayerControlPrefab;
        public List<Transform> SpawnPoints = new List<Transform>();
        private bool _isFiredThisTurn;
        private Coroutine _timerCoroutine;
        private int _timer;
        private int _endTimer;

        public void Awake()
        {
            InitGame();
        }

        private void InitGame()
        {
            var posibleSpawnPositions = new List<Transform>(SpawnPoints);
            var targetSpawnPositions = new List<Transform>();

            for (int i = 0; i < 4; i++)
            {
                var spawnPosition = posibleSpawnPositions[Random.Range(0, posibleSpawnPositions.Count)];
                targetSpawnPositions.Add(spawnPosition);
                posibleSpawnPositions.Remove(spawnPosition);
            }

            Team1.Color = Color.green;
            Team2.Color = Color.red;

            Team1.Init(gameObject, PlayerControlPrefab, targetSpawnPositions[0], targetSpawnPositions[1]);
            Team2.Init(gameObject, PlayerControlPrefab, targetSpawnPositions[2], targetSpawnPositions[3]);

            SetActivPlayer();
        }

        private void SetActivPlayer()
        {
            if (ActivTeam == null)
            {
                ActivTeam = Team1;
            }
            else
            {
                ActivTeam = ActivTeam == Team1 ? Team2 : Team1;
            }

            if (ActivPlayer != null)
            {
                ActivPlayer.PlayerHealth.OnDie -= OnActivPlayerDie;
            }
            ActivPlayer = ActivTeam.GetActivPlayer();
            ActivPlayer.PlayerHealth.OnDie += OnActivPlayerDie;
            StartCoroutine(HoldOnPlayers());
        }

        private void OnActivPlayerDie(object sender, EventArgs e)
        {
            Debug.Log("OnActivPlayerDie");
            StopCoroutine(_timerCoroutine);
            EndTurne();
        }

        private IEnumerator HoldOnPlayers()
        {
            yield return new WaitForSeconds(1);
            HoldOnPlayer(Team1.Player1);
            HoldOnPlayer(Team1.Player2);
            HoldOnPlayer(Team2.Player1);
            HoldOnPlayer(Team2.Player2);
            ActivPlayer.Rigidbody2D.isKinematic = false;
            ActivPlayer.Arrow.gameObject.SetActive(true);
            _isFiredThisTurn = false;

            StartTimer();
        }

        private void HoldOnPlayer(PlayerControl player)
        {
            if(player==null) return;
            player.Rigidbody2D.isKinematic = true;
            player.Arrow.gameObject.SetActive(false);
        }

        private void StartTimer()
        {
            TextState.TextView.color = ActivTeam.Color;
            _timerCoroutine = StartCoroutine(UpdateTimer(60));
        }

        private void StartFinalTimer()
        {
            _timerCoroutine = StartCoroutine(UpdateTimer(2));
        }

        private IEnumerator UpdateTimer(int time)
        {
            var timer = time;
            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
            }

            do
            {
                TextState.SetText(timer.ToString());
                yield return new WaitForSeconds(1);
                timer--;
            } while (timer >= 0);

            EndTurne();
        }

        private void EndTurne()
        {
            Debug.Log("EndTurne#########");
            ActivPlayer.PlayerHealth.OnDie -= OnActivPlayerDie;
            SetActivPlayer();
        }

        public void LeftButtonDown()
        {
            ActivPlayer.Arrow.gameObject.SetActive(false);
            ActivPlayer.LeftButtonDown();
        }

        public void LeftButtonUp()
        {
            ActivPlayer.Arrow.gameObject.SetActive(false);
            ActivPlayer.LeftButtonUp();
        }

        public void RightButtonDown()
        {
            ActivPlayer.Arrow.gameObject.SetActive(false);
            ActivPlayer.RightButtonDown();
        }

        public void RightButtonUp()
        {
            ActivPlayer.Arrow.gameObject.SetActive(false);
            ActivPlayer.RightButtonUp();
        }

        public void Jump()
        {
            ActivPlayer.Arrow.gameObject.SetActive(false);
            ActivPlayer.Jump();
        }

        public void UpButtonDown()
        {
            ActivPlayer.Arrow.gameObject.SetActive(false);
            ActivPlayer.Gun.StartUpGun();
        }

        public void UpButtonUp()
        {
            ActivPlayer.Arrow.gameObject.SetActive(false);
            ActivPlayer.Gun.StopUpGun();
        }

        public void DownButtonDown()
        {
            ActivPlayer.Arrow.gameObject.SetActive(false);
            ActivPlayer.Gun.StartDownGun();
        }

        public void DownButtonUp()
        {
            ActivPlayer.Arrow.gameObject.SetActive(false);
            ActivPlayer.Gun.StopDownGun();
        }

        public void Fire()
        {
            if(_isFiredThisTurn) return;
            ActivPlayer.Arrow.gameObject.SetActive(false);
            ActivPlayer.Gun.Fire();
            StartFinalTimer();
            _isFiredThisTurn = true;
        }
    }
}