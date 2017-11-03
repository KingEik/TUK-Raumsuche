using System;
using System.IO;
using System.Threading;

namespace TUK_Raumsuche
{
    public class Logger
    {
        public static readonly int LOGLEVEL_NONE = 5;
        public static readonly int LOGLEVEL_ERROR = 4;
        public static readonly int LOGLEVEL_WARNING = 3;
        public static readonly int LOGLEVEL_INFO = 2;
        public static readonly int LOGLEVEL_DEBUG = 1;
        public static readonly int LOGLEVEL_VERBOSE = 0;

        public static readonly int[] LOGLEVELS = {
                                                     LOGLEVEL_NONE,
                                                     LOGLEVEL_ERROR,
                                                     LOGLEVEL_WARNING,
                                                     LOGLEVEL_INFO,
                                                     LOGLEVEL_DEBUG,
                                                     LOGLEVEL_VERBOSE
                                                 };

        private static object locker = new object();
        public static String path = "log.txt";
        public static int logLevel = LOGLEVEL_VERBOSE;

        public static int parseLogLevel(String text)
        {
            switch (text.ToUpper())
            {
                case "VERBOSE":
                    return LOGLEVEL_VERBOSE;
                case "DEBUG":
                    return LOGLEVEL_DEBUG;
                case "INFO":
                    return LOGLEVEL_INFO;
                case "WARNING":
                    return LOGLEVEL_WARNING;
                case "ERROR":
                    return LOGLEVEL_ERROR;
            }

            return LOGLEVEL_NONE;
        }

        public static void force(params String[] text)
        {
            foreach (String line in text)
                log("[FORCE  ] " + line);
        }

        public static void error(params String[] text)
        {
            if (LOGLEVEL_ERROR >= logLevel)
            {
                foreach (String line in text)
                    log("[ERROR  ] " + line);
            }
        }
        public static void warning(params String[] text)
        {
            if (LOGLEVEL_WARNING >= logLevel)
            {
                foreach (String line in text)
                    log("[WARNING] " + line);
            }
        }
        public static void info(params String[] text)
        {
            if (LOGLEVEL_INFO >= logLevel)
            {
                foreach (String line in text)
                    log("[INFO   ] " + line);
            }
        }
        public static void debug(params String[] text)
        {
            if (LOGLEVEL_DEBUG >= logLevel)
            {
                foreach (String line in text)
                    log("[DEBUG  ] " + line);
            }
        }
        public static void verbose(params String[] text)
        {
            if (LOGLEVEL_VERBOSE >= logLevel)
            {
                foreach (String line in text)
                    log("[VERBOSE] " + line);
            }
        }

        private static void log(String line)
        {
            lock (locker)
            {
                using (StreamWriter file = new StreamWriter(path, true))
                {
                    file.WriteLine("[" + DateTime.Now.ToString() + "]" + line);
                }
            }
        }
    }
}
