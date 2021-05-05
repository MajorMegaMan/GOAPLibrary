using System;
using System.Collections.Generic;
using System.Text;
using GOAP;

namespace TestProgram
{
    class FakeGameObject
    {
        GOAPAgent<FakeGameObject> m_goapAgent;
        int someValue = 0;

        public FakeGameObject()
        {
            m_goapAgent = new GOAPAgent<FakeGameObject>(this);
        }
    }
}
