namespace Smart.Logging
{
    using System.Collections.Generic;

    /// <summary>
    ///
    /// </summary>
    public static class Log
    {
        private static int indentLevel;

        private static int indentSize;

        /// <summary>
        ///
        /// </summary>
        public static LogLevel LogLevel { get; set; }

        /// <summary>
        ///
        /// </summary>
        public static IList<LogListener> Listeners { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public static bool AutoFlush { get; set; }

        /// <summary>
        ///
        /// </summary>
        public static int IndentSize
        {
            get
            {
                return indentSize;
            }
            set
            {
                SetIndentSize(value);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public static int IndentLevel
        {
            get
            {
                return indentLevel;
            }
            set
            {
                SetIndentLevel(value);
            }
        }

        static Log()
        {
            LogLevel = LogLevel.Off;
            Listeners = new List<LogListener>();
            AutoFlush = true;
        }

        /// <summary>
        ///
        /// </summary>
        public static void Close()
        {
            foreach (var listener in Listeners)
            {
                listener.Close();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public static void Flush()
        {
            foreach (var listener in Listeners)
            {
                listener.Flush();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public static void Indent()
        {
            indentLevel++;
            foreach (var listener in Listeners)
            {
                listener.IndentLevel = indentLevel;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public static void Unindent()
        {
            if (indentLevel > 0)
            {
                indentLevel--;
            }

            foreach (var listener in Listeners)
            {
                listener.IndentLevel = indentLevel;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        private static void SetIndentLevel(int value)
        {
            if (value < 0)
            {
                value = 0;
            }
            indentLevel = value;

            foreach (var listener in Listeners)
            {
                listener.IndentLevel = indentLevel;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        private static void SetIndentSize(int value)
        {
            if (value < 0)
            {
                value = 0;
            }
            indentSize = value;

            foreach (var listener in Listeners)
            {
                listener.IndentSize = indentSize;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        public static void Write(string message)
        {
            foreach (var listener in Listeners)
            {
                listener.Write(message);
                if (AutoFlush)
                {
                    listener.Flush();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static void Write(object value)
        {
            foreach (var listener in Listeners)
            {
                listener.Write(value);
                if (AutoFlush)
                {
                    listener.Flush();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        public static void WriteLine(string message)
        {
            foreach (var listener in Listeners)
            {
                listener.WriteLine(message);
                if (AutoFlush)
                {
                    listener.Flush();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static void WriteLine(object value)
        {
            foreach (var listener in Listeners)
            {
                listener.WriteLine(value);
                if (AutoFlush)
                {
                    listener.Flush();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        public static void WriteIf(bool condition, string message)
        {
            if (condition)
            {
                Write(message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="value"></param>
        public static void WriteIf(bool condition, object value)
        {
            if (condition)
            {
                Write(value);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        public static void WriteLineIf(bool condition, string message)
        {
            if (condition)
            {
                WriteLine(message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="value"></param>
        public static void WriteLineIf(bool condition, object value)
        {
            if (condition)
            {
                WriteLine(value);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message)
        {
            if (LogLevel >= LogLevel.Error)
            {
                WriteLine(message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static void Error(object value)
        {
            if (LogLevel >= LogLevel.Error)
            {
                WriteLine(value);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        public static void Warning(string message)
        {
            if (LogLevel >= LogLevel.Warning)
            {
                WriteLine(message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static void Warning(object value)
        {
            if (LogLevel >= LogLevel.Warning)
            {
                WriteLine(value);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            if (LogLevel >= LogLevel.Info)
            {
                WriteLine(message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static void Info(object value)
        {
            if (LogLevel >= LogLevel.Info)
            {
                WriteLine(value);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        public static void Debug(string message)
        {
            if (LogLevel >= LogLevel.Debug)
            {
                WriteLine(message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static void Debug(object value)
        {
            if (LogLevel >= LogLevel.Debug)
            {
                WriteLine(value);
            }
        }
    }
}
