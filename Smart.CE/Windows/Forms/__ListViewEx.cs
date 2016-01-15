---------------------------------------------------
        public struct ListViewHitTestInfo
        {
            public int X;
            public int Y;
            public int Flags;
            public int ItemIndex;
            public int SubItemIndex;
        }

public ListViewHitTestInfo HitTest(int x, int y)
        {
            ListViewHitTestInfo hti = new ListViewHitTestInfo();
            hti.X = x;
            hti.Y = y;
            int ret = NativeMethods.SendMessage( Handle, (int)LVM.HITTEST, 0, ref hti );

            return hti;
        }

        public ListViewHitTestInfo SubItemHitTest(int x, int y)
        {
            ListViewHitTestInfo hti = new ListViewHitTestInfo();
            hti.X = x;
            hti.Y = y;
            int ret = NativeMethods.SendMessage( Handle, (int)LVM.SUBITEMHITTEST, 0, ref hti );

            return hti;
        }

----------------------------------------------------
----------------------------------------------------
        private const int NM_CUSTOMDRAW = -12;

        private const int CDRF_DODEFAULT = 0x00000000;
        private const int CDRF_NEWFONT = 0x00000002;
        private const int CDRF_SKIPDEFAULT = 0x00000004;
        private const int CDRF_NOTIFYITEMDRAW = 0x00000020;

        private const int CDDS_PREPAINT = 0x00000001;
        private const int CDDS_ITEM = 0x00010000;
        private const int CDDS_SUBITEM = 0x00020000;

        private const int CDDS_ITEMPREPAINT = ( CDDS_ITEM | CDDS_PREPAINT );
        private const int CDRF_NOTIFYSUBITEMDRAW = CDRF_NOTIFYITEMDRAW;

        public event EventHandler<DrawListViewItemEventArgs> DrawItem;
        public event EventHandler<DrawListViewSubItemEventArgs> DrawSubItem;


       protected virtual bool WndProc(ref Message m)
        {
            if( m.Msg == (int)WM.NOTIFY )
            {
                NMHDR hmndr = (NMHDR)Marshal.PtrToStructure( m.LParam, typeof(NMHDR) );

                if( m.HWnd == Handle )
                {
                    if( ( hmndr.code == NM_CUSTOMDRAW ) )
                    {
                        CustomDrawHeader( ref m );
                        return true;
                    }
                }
                else
                {
                    if( hmndr.code == NM_CUSTOMDRAW )
                    {
                        CustomDrawList( ref m );
                        return true;
                    }
                }

----------------------------------------------------
        private void CustomDrawHeader(ref Message m)
        {
            m.Result = (IntPtr)CDRF_DODEFAULT;

            NMLVCUSTOMDRAW nmcd = (NMLVCUSTOMDRAW)Marshal.PtrToStructure( m.LParam, typeof(NMLVCUSTOMDRAW) );
            switch( nmcd.nmcd.dwDrawStage )
            {

            case CDDS_PREPAINT:
                m.Result = (IntPtr)CDRF_NOTIFYITEMDRAW;

            case CDDS_ITEMPREPAINT:
                Color foreColor = SystemColors.WindowText;
                Color backColor = SystemColors.Control;

                int index = (int)nmcd.nmcd.dwItemSpec;
                if( index < this.extenders.Count )
                {
                    foreColor = this.extenders[ index ].ForeColor;
                    backColor = this.extenders[ index ].BackColor;
                }

                NativeMethods.SetTextColor( nmcd.nmcd.hdc, ( foreColor.B << 16 ) | ( foreColor.G << 8 ) | foreColor.R );
                NativeMethods.SetBkColor( nmcd.nmcd.hdc, ( backColor.B << 16 ) | ( backColor.G << 8 ) | backColor.R );
                m.Result = (IntPtr)CDRF_NEWFONT;


    [DesignTimeVisible( false )]
    public class ColumnHeaderExtender : Component
    {
        private ListViewEx parent;

        private Color foreColor = SystemColors.WindowText;
        private Color backColor = SystemColors.Control;
----------------------------------------------------
    public enum ItemBoundsPortion
    {
        Entire = 0,
        Icon = 1,
        Label = 2,
        ItemOnly = 3,
    }

    [Flags]
    public enum ListViewItemState
    {
        Selected = 1,
        Grayed = 2,
        Checked = 8,
        Focused = 16,
        Default = 32,
        Hot = 64,
        Marked = 128,
        Indeterminate = 256,
        ShowKeyboardCues = 512,
    }

    public class DrawListViewItemEventArgs : EventArgs
        public Graphics Graphics { get; private set; }
        public Rectangle Bounds { get; private set; }
        public ListViewItem Item { get; private set; }
        public int ItemIndex { get; private set; }
        public ListViewItemState ItemState { get; private set; }
        public bool DrawDefault { get; set; }

    public class DrawListViewSubItemEventArgs : EventArgs
        public Graphics Graphics { get; private set; }
        public Rectangle Bounds { get; private set; }
        public ListViewItem Item { get; private set; }
        public ListViewItem.ListViewSubItem SubItem { get; private set; }
        public int ItemIndex { get; private set; }
        public int ColumnIndex { get; private set; }
        public ColumnHeader Header { get; private set; }
        public ListViewItemState ItemState { get; private set; }
        public bool DrawDefault { get; set; }

            private void CustomDrawList(ref Message m)
        {
            m.Result = (IntPtr)CDRF_DODEFAULT;

            bool itemDrawDefault = false;

            NMLVCUSTOMDRAW nmcd = (NMLVCUSTOMDRAW)Marshal.PtrToStructure( m.LParam, typeof( NMLVCUSTOMDRAW ) );
            switch( nmcd.nmcd.dwDrawStage )
            {
            case CDDS_PREPAINT:
                if( this.ownerDraw )
                {
                    m.Result = (IntPtr)CDRF_NOTIFYSUBITEMDRAW;
                }
                break;

            case CDDS_ITEMPREPAINT:
                {
                    int itemIndex = (int)nmcd.nmcd.dwItemSpec;

                    Rectangle bounds = GetItemRectOrEmpty( itemIndex );
                    if( !ClientRectangle.IntersectsWith( bounds ) )
                    {
                        return;
                    }

                    if( this.ownerDraw )
                    {
                        using( Graphics g = Graphics.FromHdc(nmcd.nmcd.hdc) )
                        {
                            DrawListViewItemEventArgs args =
                                new DrawListViewItemEventArgs( g, bounds, Items[ itemIndex ],
                                                               itemIndex, (ListViewItemState)( nmcd.nmcd.uItemState ) );

                            OnDrawItem( args );

                            itemDrawDefault = args.DrawDefault;
                        }

                        if( this.View == View.Details )
                        {
                            m.Result = (IntPtr)CDRF_NOTIFYSUBITEMDRAW;
                        }
                        else
                        {
                            if( !itemDrawDefault )
                            {
                                m.Result = (IntPtr)CDRF_SKIPDEFAULT;
                            }
                        }

                        if( !itemDrawDefault )
                        {
                            return;
                        }
                    }

                    if( this.View == View.Details )
                    {
                        m.Result = (IntPtr)( CDRF_NOTIFYSUBITEMDRAW | CDRF_NEWFONT );
                    }

                    goto case ( CDDS_ITEMPREPAINT | CDDS_SUBITEM );
                }

            case ( CDDS_ITEMPREPAINT | CDDS_SUBITEM ):
                {
                    int itemIndex = (int)nmcd.nmcd.dwItemSpec;

                    Rectangle bounds = GetItemRectOrEmpty( itemIndex );
                    if( !ClientRectangle.IntersectsWith( bounds ) )
                    {
                        return;
                    }

                    if( this.ownerDraw && !itemDrawDefault )
                    {
                        bool skipDefault = true;

                        using( Graphics g = Graphics.FromHdc( nmcd.nmcd.hdc ) )
                        {
                            if( nmcd.iSubItem < Items[ itemIndex ].SubItems.Count )
                            {
                                Rectangle subItemBounds = GetSubItemRect( itemIndex, nmcd.iSubItem );

                                if( nmcd.iSubItem == 0 && Items[ itemIndex ].SubItems.Count > 1 )
                                {
                                    subItemBounds.Width = this.Columns[ 0 ].Width;
                                }

                                if( ClientRectangle.IntersectsWith( subItemBounds ) )
                                {
                                    DrawListViewSubItemEventArgs args =
                                        new DrawListViewSubItemEventArgs( g, subItemBounds, Items[ itemIndex ], Items[ itemIndex ].SubItems[ nmcd.iSubItem ],
                                                                          itemIndex, nmcd.iSubItem, Columns[ nmcd.iSubItem ], (ListViewItemState)( nmcd.nmcd.uItemState ) );

                                    OnDrawSubItem( args );

                                    skipDefault = !args.DrawDefault;
                                }
                            }
                        }

                        if( skipDefault )
                        {
                            m.Result = (IntPtr)CDRF_SKIPDEFAULT;
                            return;
                        }
                    }
                }
                break;
            }
        }

------------------------------------
        internal Rectangle GetSubItemRect(int itemIndex, int subItemIndex)
        {
            return GetSubItemRect( itemIndex, subItemIndex, 0 );
        }

------------------------------------
        internal Rectangle GetSubItemRect(int itemIndex, int subItemIndex, ItemBoundsPortion portion)
        {
            if( this.View != View.Details )
            {
                return Rectangle.Empty;
            }

            if( itemIndex < 0 || itemIndex >= this.Items.Count )
            {
                throw new ArgumentOutOfRangeException( "itemIndex", itemIndex.ToString() );
            }

            int subItemCount = Items[ itemIndex ].SubItems.Count;

            if( subItemIndex < 0 || subItemIndex >= subItemCount )
            {
                throw new ArgumentOutOfRangeException( "subItemIndex", subItemIndex.ToString() );
            }

            if( this.Columns.Count == 0 )
            {
                return Rectangle.Empty;
            }

            RECT rect = new RECT();
            rect.left = (int)portion;
            rect.top = subItemIndex;

            if( NativeMethods.SendMessage( this.Handle, (int)LVM.GETSUBITEMRECT, itemIndex, ref rect ) == 0 )
            {
                throw new ArgumentException( "itemIndex", itemIndex.ToString() );
            }

            return Rectangle.FromLTRB( rect.left, rect.top, rect.right, rect.bottom );
        }

        private Rectangle GetItemRectOrEmpty(int index)
        {
            if( index < 0 || index >= this.Items.Count )
            {
                return Rectangle.Empty;
            }

            if( ( this.View == View.Details ) && ( this.Columns.Count == 0 ) )
            {
                return Rectangle.Empty;
            }

            RECT rect = new RECT();
            if( NativeMethods.SendMessage( Handle, (int)LVM.GETITEMRECT, index, ref rect ) == 0 )
            {
                return Rectangle.Empty;
            }

            return Rectangle.FromLTRB( rect.left, rect.top, rect.right, rect.bottom );
        }
