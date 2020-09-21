using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Tests
{
    public class NewTest
    {
        [Test]
        public void NewTestSimplePasses()
        {
            Assert.IsTrue(true);
        }

        [UnityTest]
        public IEnumerator NewTestWithEnumeratorPasses()
        {
            yield return null;
            Assert.IsFalse(false);
        }
    }
}