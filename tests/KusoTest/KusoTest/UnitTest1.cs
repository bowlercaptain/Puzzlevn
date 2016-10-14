using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace KusoTest {
	[TestClass]
	public class UnitTest1 {
		[TestMethod]
		public void TestMethod1() {


		Assert.IsTrue(Equals(JapaneseNato.Encode("hi"), "ホテルインド"));

        }
	}
}
