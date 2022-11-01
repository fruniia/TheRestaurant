using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Table : Restaurant
    {
        protected int MaxNumberOfGuestsAtTable { get; set; }
        public bool Occupied { get; set; }
        private int QualityLevel { get; set; }

        private List<Food> Menu;

        public Group groupInTable = new Group();

        public Table()
        {
            MaxNumberOfGuestsAtTable = 0;
            Occupied = false;
            Menu = new List<Food>();
            QualityLevel = 0;
        }

        public void SetTable()
        {

        }

        private void DrawTable()
        {

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
