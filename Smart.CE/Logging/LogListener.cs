namespace Smart.Logging
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public abstract class LogListener : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        protected bool NeedIndent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int IndentLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int IndentSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected LogListener()
        {
            NeedIndent = true;
            IndentSize = 4;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Close()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Flush()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public abstract void Write(string message);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public abstract void WriteLine(string message);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public virtual void Write(object value)
        {
            if (value != null)
            {
                Write(value.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public virtual void WriteLine(object value)
        {
            WriteLine(value == null ? string.Empty : value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void WriteIndent()
        {
            NeedIndent = false;

            for (var i = 0; i < IndentLevel; i++)
            {
                if (IndentSize == 4)
                {
                    Write("    ");
                }
                else
                {
                    for (var j = 0; j < IndentSize; j++)
                    {
                        Write(" ");
                    }
                }
            }
        }
    }
}
