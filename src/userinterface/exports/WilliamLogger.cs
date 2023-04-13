using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackNTFS.src.userinterface.exports
{
    // William
    internal class WilliamLogger
    {
        public class WPriority
        {
            public static readonly object[] NONE         = { "NONE",        int.MinValue };
            public static readonly object[] MINOR        = { "MINOR",       10000        };
            public static readonly object[] NORMAL       = { "NORMAL",      20000        };
            public static readonly object[] MAJOR        = { "MAJOR",       30000        };
            public static readonly object[] SERIOUS      = { "SERIOUS",     40000        };
            public static readonly object[] DANDEROUS    = { "DANDEROUS",   50000        };
            public static readonly object[] FATAL        = { "FATAL",       60000        };
            public static readonly object[] ALL          = { "ALL",         int.MaxValue };

            private WPriority() { }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="current"></param>
            /// <param name="other"></param>
            /// <returns>2 For incomparable;
            ///          1 For CURRENT is greater than OTHER;
            ///          0 For CURRENT is equal as OTHER;
            ///         -1 For CURRENT is less than OTHER; </returns>
            public static int Compare(object[]? current, object[]? other)
            {
                if (current == null || other == null)
                    return 2;

                try
                {
                    Int64 i64Current = Convert.ToInt64(current[1]);
                    Int64 i64Other = Convert.ToInt64(other[1]);
                    if (i64Current > i64Other)
                    { return 1; }
                    else if (i64Current == i64Other)
                    { return 0; }
                    else
                    { return -1;}

                } catch (IndexOutOfRangeException outOfRangeExcep)
                {
                    globalWilliamLogger
                        .Log(WilliamLogger.WPriority.SERIOUS,
                             WilliamLogger.WPurpose.LOGGING,
                             new object[]
                             {
                                 $"Illegal parameter {nameof(current)} and parameter {nameof(other)}\n" +
                                 $"had not have proper length which is used to satisfy {nameof(WPriority)}.\n" +
                                 $"In this particular case:\nParameter {nameof(current)} have length being as {current.Length}\n" +
                                 $"Parameter {nameof(other)} have length being as {current.Length}\n" +
                                 $"While members of {nameof(WPriority)} all require at least having length as much as " +
                                 $"{WPriority.NONE.Length}"
                             }
                        );
                    return 2;
                }
            }
        }

        public class WPurpose
        {
            public static readonly string NOTHING = "Nothing";
            public static readonly string LOGGING = "Logging";
            public static readonly string TESTING = "Testing";
        }

        public static readonly string WILLIAM_LOG_DECORATION = ">>> ";
        public static readonly string WILLIAM_SIGN = "William";
        public static readonly string WILLIAM_DEAFULT_PURPOSE = "Logging";

        private readonly object[] wPriority;
        private readonly string wPurpose;

        private static WilliamLogger globalWilliamLogger = new WilliamLogger(WilliamLogger.WPriority.NONE, WilliamLogger.WPurpose.NOTHING);

        public WilliamLogger(object[] priority, string wPurpose)
        {
            this.wPriority = priority;
            this.wPurpose = wPurpose;
        }

        public object[] Priority { get { return wPriority;} }

        public string Purpose { get { return wPurpose;} }

        public void Log(object[] priority, string purpose, object[] msg)
        {
            if (priority is null)
            {
                throw new ArgumentNullException(nameof(priority));
            }

            if (msg is null)
            {
                throw new ArgumentNullException(nameof(msg));
            }

            DoLog(CombineToWilliamPrecontent(priority, purpose), msg);
        }

        /// <summary>
        /// Make sure every line for outputing is covered with WilliamPrecontent ahead.
        /// </summary>
        /// <param name="targetStr"></param>
        public static void DoLog(string WilliamPrecontent, object[] targetStr)
        {
            if (WilliamPrecontent is null)
            {
                throw new ArgumentNullException(nameof(WilliamPrecontent));
            }

            if (targetStr is null)
            {
                return;
            }

            /* Don't forget the first WilliamPreContent~~ */
            Console.Write(WilliamPrecontent);

            for (int i = 0; i < targetStr.Length; i++)
            {
                string currStr = targetStr[i].ToString();
                char currStrChar = char.MaxValue;
                for (int j = 0; j < currStr.Length; j++)
                {
                    currStrChar = currStr[j];
                    if (currStrChar == '\n')
                    {
                        Console.Write('\n');
                        Console.Write(WilliamPrecontent);
                        continue;
                    }
                    Console.Write(currStrChar);
                }
            }

            /* Finish logging by printting a line breaker. */
            Console.Write('\n');
        }

        private static string CombineToWilliamPrecontent(object[] priority, string purpose)
        {
            return ($"{WILLIAM_LOG_DECORATION}[{priority[0]}]({WILLIAM_SIGN} - {purpose}): ");
        }

        public static WilliamLogger GetGlobal()
        {
            return globalWilliamLogger;
        }
    }
}
