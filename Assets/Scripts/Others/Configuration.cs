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
                public static string Insert => " insert into SCOREBOARD (USER, SCORE, MOMENT) values ('{0}', '{1}', {2}) ";
                public static string Update => " update SCOREBOARD set USER = '{0}', SCORE = '{1}', MOMENT = {2} where ID = {3} ";
                public static string Delete => " delete from SCOREBOARD where ID = {0} ";
                public static string ListAll => " select ID, USER, SCORE, MOMENT from SCOREBOARD order by SCORE desc ";
                public static string GetById => " select ID, USER, SCORE, MOMENT from SCOREBOARD where ID = {0} ";
            }
        }

        public class Scenes
        {
            public static string RegisterUpdate => "RegisterUpdate";
            public static string Table => "Table";
        }

        public class Properties
        {
            public static string DatabaseName => "database.s3db";
            public static string DatabasePath => string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);
            public static string DatabaseStreamingAssetsPath => string.Format("{0}/StreamingAssets/{1}", Application.dataPath, DatabaseName);

#if UNITY_ANDROID
            public static string MobileDatabasePath => string.Format("jar:file://{0}!/assets/{1}", Application.dataPath, DatabaseName);
#elif UNITY_IOS
            public static string MobileDatabasePath => string.Format("file://{0}/Raw/{1}", Application.dataPath, DatabaseName);
#else
            public static string MobileDatabasePath => string.Empty;
#endif
        }
    }
}