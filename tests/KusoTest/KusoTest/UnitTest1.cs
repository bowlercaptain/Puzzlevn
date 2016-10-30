using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
//using UnityEngine;

namespace KusoTest {
	[TestClass]
	public class UnitTest1 {
		[TestMethod]
		public void TestMethod1() {


		Assert.IsTrue(Equals(JapaneseNato.Encode("hi"), "ホテルインド"));

        }

		[TestMethod]
		public void TestAnimationCosntsss() {
			//Assert.IsTrue(typeof(CharacterRend) == typeof(CharacterRend));
			var typee = typeof(CharacterRend.Hop);
			var fieldd = typee.GetField("HOPDURATION",BindingFlags.NonPublic|BindingFlags.Static);
			var vall = fieldd.GetValue(null);
            Assert.IsTrue((float)(vall) != 0f);

			Assert.AreNotEqual<float>(0f, (float)(typeof(CharacterRend.Hop).GetField("HOPHEIGHT",BindingFlags.NonPublic | BindingFlags.Static).GetValue(null)));

			Assert.AreNotEqual<float>(0f, (float)(typeof(CharacterRend.Hide).GetField("HIDETIME",BindingFlags.NonPublic | BindingFlags.Static).GetValue(null)));

            if((float)(typeof(CharacterRend.Enter).GetField("OFFSETMAGNITUDE", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null))< 1f){
                Console.WriteLine("Warning: 'Enter' magnitude is definitely too small to look good. Are you sure it's correct?");
            }

        }
    }
}
