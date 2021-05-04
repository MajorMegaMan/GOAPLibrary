using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GOAP;


namespace UnitTestProject1
{
    [TestClass]
    public class WorldStateTest
    {
        class Dummy
        {
            public int number = 0;
            public bool boolean = false;
        }

        // World State
        [TestMethod]
        public void CreateAndGetTest()
        {
            GOAPWorldState worldState = new GOAPWorldState();

            int intValue = 57;
            worldState.CreateElement("first_int", intValue);

            object objectIntResult = worldState.GetElementValue("first_int");
            int intResult = worldState.GetElementValue<int>("first_int");

            Assert.AreEqual(intValue, objectIntResult);
            Assert.AreEqual(57, objectIntResult);
            Assert.AreEqual<int>(intValue, intResult);
            Assert.AreEqual<int>(57, intResult);

            worldState.CreateElement("other_int", 23);
            worldState.CreateElement("first_bool", true);
            worldState.CreateElement("first_float", 43.0f);
            worldState.CreateElement("null_test", null);
            Dummy dummy = new Dummy();
            worldState.CreateElement("dummy", dummy);

            int other = worldState.GetElementValue<int>("other_int");
            bool boolValue = worldState.GetElementValue<bool>("first_bool");
            float floatValue = worldState.GetElementValue<float>("first_float");
            Dummy nullTest = worldState.GetElementValue<Dummy>("null_test");
            Dummy dummyResult = worldState.GetElementValue<Dummy>("dummy");

            Assert.AreEqual<int>(23, other);
            Assert.AreEqual<bool>(true, boolValue);
            Assert.AreEqual<float>(43.0f, floatValue);

            Assert.AreEqual<Dummy>(dummy, dummyResult);
            Assert.AreNotEqual<Dummy>(null, dummyResult);

            Assert.AreEqual<Dummy>(null, nullTest);

            var namesList = worldState.GetNames();

            Assert.AreEqual(6, namesList.Count);
        }

        [TestMethod]
        public void SetValueTest()
        {
            GOAPWorldState worldState = new GOAPWorldState();

            int intValue = 57;
            worldState.CreateElement("first_int", intValue);

            worldState.SetElementValue("first_int", 23);

            int result = worldState.GetElementValue<int>("first_int");

            Assert.AreEqual<int>(23, result);

            worldState.CreateElement("other_int", 23);
            worldState.CreateElement("first_bool", true);
            worldState.CreateElement("first_float", 43.0f);
            worldState.CreateElement("dummy", null);

            worldState.SetElementValue("first_float", 57.5f);
            Dummy dummyTest = new Dummy();
            worldState.SetElementValue("dummy", dummyTest);

            float floatResult = worldState.GetElementValue<float>("first_float");
            Dummy dummyResult = worldState.GetElementValue<Dummy>("dummy");

            Assert.AreEqual<float>(57.5f, floatResult);
            Assert.AreEqual<Dummy>(dummyTest, dummyResult);
            Assert.AreNotEqual<Dummy>(null, dummyResult);
        }

        [TestMethod]
        public void CompareValueTest()
        {
            GOAPWorldState worldState = new GOAPWorldState();

            // Create element
            worldState.CreateElement("testName1", 5);
            worldState.CreateElement("bool", true);
            worldState.CreateElement("float", 7.8f);
            Dummy dummy = new Dummy();
            dummy.number = 8;
            dummy.boolean = false;
            worldState.CreateElement("Dummy", dummy);

            // compare value to other world state
            GOAPWorldState otherState = new GOAPWorldState();
            otherState.CreateElement("bool", true);
            otherState.CreateElement("float", 7.8f);
            otherState.CreateElement("Dummy", null);

            bool equalVal = worldState.CompareValue(otherState, "float");
            otherState.SetElementValue("float", 8.9f);
            bool notEqualVal = worldState.CompareValue(otherState, "float");

            Assert.AreEqual(true , equalVal);
            Assert.AreEqual(false, notEqualVal);

            // compare class
            bool notEqualDummyVal = worldState.CompareValue(otherState, "Dummy");
            otherState.SetElementValue("Dummy", dummy);
            bool equalDummyVal = worldState.CompareValue(otherState, "Dummy");

            Assert.AreEqual(false, notEqualDummyVal);
            Assert.AreEqual(true ,equalDummyVal);

            // compare All values of other state
            bool notEqualState1 = worldState.CheckState(otherState);
            otherState.SetElementValue("bool", false);
            bool notEqualState2 = worldState.CheckState(otherState);

            otherState.SetElementValue("bool", true);
            otherState.SetElementValue("float", 7.8f);
            bool equalState = worldState.CheckState(otherState);

            Assert.AreEqual(false, notEqualState1);
            Assert.AreEqual(false, notEqualState2);
            Assert.AreEqual(true, equalState);
        }

        [TestMethod]
        public void CombineWithRefTest()
        {
            GOAPWorldState firstState = new GOAPWorldState();

            // Create element
            firstState.CreateElement("testName1", 5);
            Dummy dummy = new Dummy();
            dummy.number = 8;
            dummy.boolean = false;
            firstState.CreateElement("Dummy", dummy);

            // compare value to other world state
            GOAPWorldState secondState = new GOAPWorldState();
            secondState.CreateElement("bool", true);
            secondState.CreateElement("float", 7.8f);

            GOAPWorldState combined = GOAPWorldState.CombineWithReferences(firstState, secondState);

            var totalNames = combined.GetNames();
            Assert.AreEqual(4, totalNames.Count);

            // Change value in combined to change values in the original states
            combined.SetElementValue("testName1", 3);
            combined.SetElementValue("float", 5.6f);

            int firstInt = firstState.GetElementValue<int>("testName1");
            float secondFloat = secondState.GetElementValue<float>("float");

            Assert.AreEqual(3, firstInt);
            Assert.AreEqual(5.6f, secondFloat);

            Dummy dummyCheck1 = combined.GetElementValue<Dummy>("Dummy");

            Assert.AreEqual(dummy, dummyCheck1);

            // Change values in original to change the values in combined
            firstState.SetElementValue("Dummy", null);
            secondState.SetElementValue("bool", false);

            Dummy changedDummyCheck = combined.GetElementValue<Dummy>("Dummy");
            bool boolCheck = combined.GetElementValue<bool>("bool");

            Assert.AreEqual(null, changedDummyCheck);
            Assert.AreEqual(false, boolCheck);
        }
    }
}
