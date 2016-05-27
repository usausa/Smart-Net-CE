namespace Smart.Windows.Forms
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    ///
    /// </summary>
    public sealed class CursorChanger : IDisposable
    {
        private static int refCount;

        /// <summary>
        ///
        /// </summary>
        public CursorChanger() :
            this(Cursors.WaitCursor)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cursor"></param>
        public CursorChanger(Cursor cursor)
        {
            if (refCount == 0)
            {
                Cursor.Current = cursor;
                Cursor.Show();
            }

            refCount++;
        }

        /// <summary>
        ///
        /// </summary>
        public void Dispose()
        {
            refCount--;

            if (refCount < 0)
            {
                refCount = 0;
            }

            if (refCount == 0)
            {
                Cursor.Current = Cursors.Default;
                Cursor.Hide();
            }
        }
    }
}