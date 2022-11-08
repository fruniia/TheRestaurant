using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Table : Restaurant
    {
        Random random = new Random();
        //enum Level
        //{ 
        //    Low = 0-5,
        //    Medium = 6-10,
        //    High = 11-15
        //}
        internal int MaxNumberOfGuestsAtTable { get; set; }
        internal bool Occupied { get; set; }
        private int QualityLevel { get; set; }

        internal Group groupInTable = new();
        internal bool GroupHasOrderedFood { get; set; }
        internal bool GroupHasGotFood { get; set; }
        internal int TableID { get; set; }

        public Table()
        {
            GroupHasGotFood = false;
            GroupHasOrderedFood = false;
            MaxNumberOfGuestsAtTable = 0;
            Occupied = false;
            QualityLevel = 0;
        }
    }

    internal class TableForTwo : Table
    {
        public TableForTwo() : base()
        {
            MaxNumberOfGuestsAtTable = 2;
        }
    }

    internal class TableForFour : Table
    {
        public TableForFour() : base()
        {
            MaxNumberOfGuestsAtTable = 4;
        }
    }
}
