namespace Smart.Logging
{
    using System;
    using System.IO;

    /// <summary>
    ///
    /// </summary>
    public class TextWriterLogListener : LogListener
    {
        /// <summary>
        ///
        /// </summary>
        public TextWriter Writer { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileName"></param>
        public TextWriterLogListener(string fileName)
        {
            Writer = new StreamWriter(fileName, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        public TextWriterLogListener(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            Writer = new StreamWriter(stream);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="writer"></param>
        public TextWriterLogListener(TextWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            Writer = writer;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Close();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        ///
        /// </summary>
        public override void Close()
        {
            if (Writer != null)
            {
                Writer.Close();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public override void Flush()
        {
            if (Writer != null)
            {
                Writer.Flush();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        public override void Write(string message)
        {
            if (Writer == null)
            {
                return;
            }

            if (NeedIndent)
            {
                WriteIndent();
            }
            Writer.Write(message);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        public override void WriteLine(string message)
        {
            if (Writer == null)
            {
                return;
            }

            if (NeedIndent)
            {
                WriteIndent();
            }
            Writer.WriteLine(message);

            NeedIndent = true;
        }
    }
}
