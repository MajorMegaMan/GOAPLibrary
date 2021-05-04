using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GOAP;

namespace UnitTestProject1
{
    [TestClass]
    public class WorldDataTest
    {
        // World Data
        [TestMethod]
        public void ConvertValueTest()
        {
            // int
            {
                int intValue = 8;
                string intName = "int";
                WorldData intWorld = new WorldData(intName, intValue);

                var intConvertResult = intWorld.ConvertValue<int>();

                Assert.AreEqual(intValue, intConvertResult);
                Assert.AreEqual<int>(8, intConvertResult);

                Assert.AreNotEqual<int>(5, intConvertResult);
            }

            // bool
            {
                bool boolValue = true;
                string boolName = "bool";
                WorldData boolWorld = new WorldData(boolName, boolValue);

                var boolConvertResult = boolWorld.ConvertValue<bool>();

                Assert.AreEqual(boolValue, boolConvertResult);
                Assert.AreEqual<bool>(true, boolConvertResult);

                Assert.AreNotEqual<bool>(false, boolConvertResult);
            }
        }

        [TestMethod]
        public void ConvertTypeTest()
        {
            // int
            {
                int intValue = 8;
                string intName = "int";
                WorldData intWorld = new WorldData(intName, intValue);

                var convertResult = intWorld.ConvertValue<int>();

                Assert.AreEqual(typeof(int), convertResult.GetType());
                Assert.AreEqual(intValue.GetType(), convertResult.GetType());
            }

            // bool
            {
                bool boolValue = true;
                string boolName = "bool";
                WorldData boolWorld = new WorldData(boolName, boolValue);

                var convertResult = boolWorld.ConvertValue<bool>();

                Assert.AreEqual(typeof(bool), convertResult.GetType());
                Assert.AreEqual(boolValue.GetType(), convertResult.GetType());
            }
        }

        [TestMethod]
        public void DefaultValueTest()
        {
            // int
            {
                int intValue = 8;
                string intName = "int";
                WorldData intWorld = new WorldData(intName, intValue);

                intWorld.SetDefaultValue<int>();

                var convertResult = intWorld.ConvertValue<int>();

                Assert.AreEqual(0, convertResult);
            }

            // bool
            {
                int boolValue = 8;
                string boolName = "bool";
                WorldData boolWorld = new WorldData(boolName, boolValue);

                boolWorld.SetDefaultValue<bool>();

                var convertResult = boolWorld.ConvertValue<bool>();

                Assert.AreEqual(false, convertResult);
            }
        }
    }
}
