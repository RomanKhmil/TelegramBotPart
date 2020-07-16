using BadoonBotLogic_2._0_.objects.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BadoonBotLogic_2._0_.objects.Grid;
using BadoonBotLogic_2._0_.objects;
using System.Management.Instrumentation;

namespace TelegramBotPart.Menu
{
    class Search
    {
        public static string getUsersInRange(GridMap map,string userID,double dist)
        {
            string res = "";
            dist = dist / 111;
            Person tempUser = map.Find(userID);
            if (tempUser == null)
                return null;
            map.FindAtRadius(tempUser);
            List<Person> userList = map.TakeFromSearchBasket();
            foreach(var i in userList)
            {
                res += $"{i.GetName()}, {(float)(Point.Dist(i,tempUser) * 111)} км\n";
            }
            return res;
        }
    }
}
