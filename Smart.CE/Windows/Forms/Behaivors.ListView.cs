namespace Smart.Windows.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    ///
    /// </summary>
    public class SortListBewBeaivors : IBehaivor<ListView>
    {
        private readonly Func<ListViewItem, ListViewItem, bool, int>[] sortFunctions;

        private readonly bool[] ascendings;

        /// <summary>
        ///
        /// </summary>
        /// <param name="sortFunctions"></param>
        public SortListBewBeaivors(IEnumerable<Func<ListViewItem, ListViewItem, bool, int>> sortFunctions)
        {
            this.sortFunctions = sortFunctions.ToArray();
            ascendings = new bool[this.sortFunctions.Length];
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="control"></param>
        public void Attach(ListView control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            control.ColumnClick += ControlOnColumnClick;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="control"></param>
        public void Detach(ListView control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            control.ColumnClick -= ControlOnColumnClick;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ControlOnColumnClick(object sender, ColumnClickEventArgs args)
        {
            if ((args.Column > sortFunctions.Length) || (sortFunctions[args.Column] == null))
            {
                return;
            }

            var listView = (ListView)sender;
            var count = listView.Items.Count;
            var comparer = new SortFunctionComparer(sortFunctions[args.Column], ascendings[args.Column]);

            var cursorOrg = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            Cursor.Show();
            try
            {
                listView.BeginUpdate();

                var list = listView.Items.Cast<ListViewItem>().ToList();
                list.Sort(0, count, comparer);

                listView.Items.Clear();

                foreach (var item in list)
                {
                    listView.Items.Add(item);
                }

                listView.EndUpdate();
            }
            finally
            {
                Cursor.Current = cursorOrg;
                Cursor.Hide();
            }

            ascendings[args.Column] = !ascendings[args.Column];
        }

        /// <summary>
        ///
        /// </summary>
        private class SortFunctionComparer : IComparer<ListViewItem>
        {
            private readonly Func<ListViewItem, ListViewItem, bool, int> sortFunction;
            private readonly bool ascending;

            /// <summary>
            ///
            /// </summary>
            /// <param name="sortFunction"></param>
            /// <param name="ascending"></param>
            public SortFunctionComparer(Func<ListViewItem, ListViewItem, bool, int> sortFunction, bool ascending)
            {
                this.sortFunction = sortFunction;
                this.ascending = ascending;
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public int Compare(ListViewItem x, ListViewItem y)
            {
                return sortFunction(x, y, ascending);
            }
        }
    }
}
