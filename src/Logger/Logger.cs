namespace Logger
{
    public class Logger
    {
        private static Logger instance;
        private StreamWriter _writer;

        private Logger() { }

        ~Logger()
        {
            Logger.GetInstance()._writer?.Close();
        }

        public static Logger GetInstance()
        {
            if (instance == null)
            {
                instance = new Logger();
            }
            return instance;
        }

        public static void SetLogfile(string logfile)
        {
            Logger.GetInstance()._writer?.Close();
            instance._writer = new(logfile);
            instance._writer.AutoFlush = true;
        }

        public static void Info(string message)
        {
            string data = String.Format("[{0}][INFO] {1}\n", DateTime.Now, message);
            Logger.GetInstance()._writer?.Write(data);
        }
        public static void Warning(string message)
        {
            string data = String.Format("[{0}][WARNING] {1}\n", DateTime.Now, message);
            Logger.GetInstance()._writer?.Write(data);
        }
        public static void Error(string message)
        {
            string data = String.Format("[{0}][ERROR] {1}\n", DateTime.Now, message);
            Logger.GetInstance()._writer?.Write(data);
        }
    }
}