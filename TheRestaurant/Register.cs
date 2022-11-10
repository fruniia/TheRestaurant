using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Register : Restaurant
    {
        internal int TonightsRevenue { get; set; }
        internal int Tip { get; set; }

        internal int RevenuePerGroup { get; set; }
        public Register()
        {
            TonightsRevenue += RevenuePerGroup;
            Tip = 0;
            RevenuePerGroup = 0;
        }
        internal void CalculateRevenue(Table table)
        {
            switch (table.groupInTable.GroupExperience)
            {
                case 2:
                case 3:
                    Tip = 0;
                    break;
                case 4:
                case 5:
                case 6:
                    Tip = table.groupInTable.TotalPrice / 20;
                    break;
                case 7:
                case 8:
                case 9:
                    Tip = table.groupInTable.TotalPrice / 10;
                    break;
                case 10:
                    Tip = table.groupInTable.TotalPrice / 5;
                    break;
            }
            RevenuePerGroup = table.groupInTable.TotalPrice + Tip;
            TonightsRevenue += RevenuePerGroup;
            Console.WriteLine((table.groupInTable.GroupExperience < 4) ?
            $"The customers were unhappy with the service and gives no tip. Just pays {table.groupInTable.TotalPrice}" :
            $"Table {table.TableID} tips {Tip} SEK for a good service, and pays a total of {RevenuePerGroup} SEK");
        }
    }
}
