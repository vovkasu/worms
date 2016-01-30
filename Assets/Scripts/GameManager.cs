using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            ActivPlayer = ActivTeam.GetActivPlayer();
            StartCoroutine(HoldOnPlayers());
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
        }

        private void HoldOnPlayer(PlayerControl player)
        {
            player.Rigidbody2D.isKinematic = true;
            player.Arrow.gameObject.SetActive(false);
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
            ActivPlayer.Arrow.gameObject.SetActive(false);
            ActivPlayer.Gun.Fire();
        }
    }


    public class TeamPlayers
    {
        public PlayerControl Player1;
        public PlayerControl Player2;
        public Color Color;
        private bool _isFirstPlayerActiv;

        public void Init(GameObject root, PlayerControl controlPrefab, Transform spawnPosition1, Transform spawnPosition2)
        {
            if (Player1 != null)
            {
                Object.Destroy(Player1);
            }

            Player1 = Object.Instantiate(controlPrefab);
            Player1.transform.position = spawnPosition1.position;
            Player1.transform.parent = root.transform;
            Player1.Body.color = Color;

            if (Player2 != null)
            {
                Object.Destroy(Player2);
            }

            Player2 = Object.Instantiate(controlPrefab);
            Player2.transform.position = spawnPosition2.position;
            Player2.transform.parent = root.transform;
            Player2.Body.color = Color;
        }

        public PlayerControl GetActivPlayer()
        {
            _isFirstPlayerActiv = !_isFirstPlayerActiv;
            return _isFirstPlayerActiv ? Player1 : Player2;
        }
    }
}