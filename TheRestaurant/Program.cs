namespace TheRestaurant
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Restaurant restaurant = new();

        }

        public void CheckForEmptyTable(List<Table> tables, List<Group> waitingList)
        {
            for (int i = 0; i < tables.Count; i++)
            {
                if (tables[i].Occupied == false && waitingList is not null)
                {
                    for (int j = 0; j < waitingList.Count; j++)
                    {
                        if (tables[i] is TableForTwo && waitingList[j].guests.Count <= 2)
                        {
                            HandleWaitingList(tables, waitingList, i, j);
                            RemoveFromWaitingList(waitingList, j);
                        }
                        else if (tables[i] is TableForFour && waitingList[j].guests.Count <= 4)
                        {
                            HandleWaitingList(tables, waitingList, i, j);
                            RemoveFromWaitingList(waitingList, j);
                        }
                    }
                }
            }
        }
        public void HandleWaitingList(List<Table> tables, List<Group> waitingList, int tableIndex, int wlIndex)
        {
            tables[tableIndex].Occupied = true;
            tables[tableIndex].groupInTable.guests = waitingList[wlIndex].guests;
        }
        public void RemoveFromWaitingList(List<Group> waitingList, int index)
        {
            waitingList.Remove(waitingList[index]);
        }
    }
}