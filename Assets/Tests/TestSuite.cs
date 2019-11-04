using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestSuite
    {
        private Clock clock;
        private Player player;
        private GameObject game;
        private DeathZone deathZone;

        [SetUp]
        public void Setup()
        {
            GameObject gamePrefab = Resources.Load<GameObject>("Prefabs/Game");
            game = Object.Instantiate(gamePrefab);
            clock = game.GetComponentInChildren<Clock>();
            player = game.GetComponentInChildren<Player>();
            deathZone = game.GetComponentInChildren<DeathZone>();
        }

        [TearDown]
        public void Teardown()
        {
            Object.Destroy(game);
        }

        [UnityTest]
        public IEnumerator GravityisOn()
        {
            Vector3 initialYPosition = Vector3.zero;
            initialYPosition.y = 10;
            player.transform.localPosition = initialYPosition;
            player.gravity = player.initialGravity;
            yield return new WaitForSeconds(0.1f);
            Assert.Less(player.transform.localPosition.y, initialYPosition.y);
        }

        [UnityTest]
        public IEnumerator PlayerJump()
        {
            float initialYPosition = player.transform.localPosition.y;
            player.Jump(player.jumpHeight);
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            Assert.Greater(player.transform.localPosition.y, initialYPosition);
        }
                
        [UnityTest]
        public IEnumerator PlayerKilled()
        {
            Vector3 initialPosition = new Vector3(0, 2f, 20f);
            player.transform.localPosition = initialPosition;
            player.gravity = player.initialGravity;
            deathZone.transform.position = player.transform.position;
            yield return new WaitForFixedUpdate();
            Assert.IsTrue(player.isDead);
        }

        [UnityTest]
        public IEnumerator PlayerExists()
        {
            yield return new WaitForEndOfFrame();

            Assert.NotNull(player, "Where is da player?? You gotta assign da player ");
        }


    }
}
