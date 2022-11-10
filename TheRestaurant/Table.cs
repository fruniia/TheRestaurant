using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Table : Restaurant
    {
        Random random = new();
        internal int MaxNumberOfGuestsAtTable { get; set; }
        internal bool Occupied { get; set; }
        internal int QualityLevel { get; set; }

        internal Group groupInTable = new();
        internal bool GroupHasOrderedFood { get; set; }
        internal bool GroupHasGotFood { get; set; }
        internal int TableID { get; set; }

        public Table()
        {
            MaxNumberOfGuestsAtTable = 0;
            Occupied = false;
            QualityLevel = random.Next(1, 6);
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
