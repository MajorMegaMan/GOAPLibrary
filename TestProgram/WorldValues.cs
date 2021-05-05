using System;
using System.Collections.Generic;
using System.Text;

namespace TestProgram
{
    public static class WorldValues
    {
        // World values
        public static string storedWood { get; private set; } = "stored_wood";// int
        public static string storedFood { get; private set; } = "stored_food";// int


        // Agent Specific values
        public static string holdItemType { get; private set; } = "hold_item_type";// HoldItemType

        public enum HoldItemType
        {
            nothing,
            wood,
            food
        }
    }
}
