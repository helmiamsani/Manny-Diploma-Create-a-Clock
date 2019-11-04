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
        

        [UnityTest]
        public IEnumerator SecondsArmMoves()
        {
            GameObject prefab = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Clock"));
            clock = prefab.GetComponent<Clock>();
            float initialYRotation = clock.secondsTransform.localRotation.y;
            yield return new WaitForSeconds(0.1f);
            if(initialYRotation < clock.secondsTransform.localRotation.y)
            {
                Assert.Greater(clock.secondsTransform.localRotation.y, initialYRotation);
            }
            else
            {
                Assert.Less(clock.secondsTransform.localRotation.y, initialYRotation);
            }
            Object.Destroy(prefab.gameObject);
        }
    }
}
