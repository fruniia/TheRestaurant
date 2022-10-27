using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Table : Restaurant
    {
        private int NumberOfGuestsAtTable { get; set; }
        private bool Occupied { get; set; }
        private int QualityLevel { get; set; }
        private int TypeOfTable { get; set; }

        private List<Food> Menu;

        public Table(int typeOfTable)
        {
            TypeOfTable = typeOfTable;
             NumberOfGuestsAtTable = 0;
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
}
