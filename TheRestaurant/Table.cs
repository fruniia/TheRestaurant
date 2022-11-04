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
        public int MaxNumberOfGuestsAtTable { get; set; }
        public bool Occupied { get; set; }
        private int QualityLevel { get; set; }
        //private List<Food> Menu;
        public Group groupInTable = new Group();
        public bool GroupHasOrderedFood { get; set; }
        //List<Table> tables = new();
        public int TableID { get; set; }


        public Table()
        {
            GroupHasOrderedFood = false;
            MaxNumberOfGuestsAtTable = 0;
            Occupied = false;
            //Menu = new List<Food>();
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
