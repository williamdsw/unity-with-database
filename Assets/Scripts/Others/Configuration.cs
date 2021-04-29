using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Others
{
    public class Configuration
    {
        public class Messages
        {
            public static string ItemsFound => "{0} items found!";
            public static string NothingWasFound => "Nothing was found!";
        }

        public class Queries
        {
            public class Scoreboard
            {
                public static string Insert => " insert into SCOREBOARD (USER, SCORE, MOMENT) values (@USER, @SCORE, @MOMENT) ";
                public static string Update => " update SCOREBOARD set USER = @USER, SCORE = @SCORE, MOMENT = @MOMENT where ID = @ID ";
                public static string Delete => " delete from SCOREBOARD where ID = @ID ";
                public static string ListAll => " select ID, USER, SCORE, MOMENT from SCOREBOARD order by SCORE desc ";
                public static string GetById => " select ID, USER, SCORE, MOMENT from SCOREBOARD where ID = @ID ";
            }
        }

        public class Scenes
        {
            public static string RegisterUpdate => "RegisterUpdate";
            public static string Table => "Table";
        }
    }
}