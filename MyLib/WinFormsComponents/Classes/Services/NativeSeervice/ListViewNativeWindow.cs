namespace WinFormsComponents.Classes.Services.NativeSeervice
{
    internal class ListViewNativeWindow : NativeWindow
    {
        public event EventHandler ScrollMessageReceived;

        private const int WM_VSCROLL = 0x0115;
        private const int WM_HSCROLL = 0x0114;
        private const int WM_MOUSEWHEEL = 0x020A;
        private const int WM_KEYDOWN = 0x0100;

        public ListViewNativeWindow(ListView listView)
        {
            if (listView != null && listView.IsHandleCreated)
            {
                AssignHandle(listView.Handle);
            }
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_VSCROLL:
                case WM_HSCROLL:
                case WM_MOUSEWHEEL:
                    ScrollMessageReceived?.Invoke(this, EventArgs.Empty);
                    break;

                case WM_KEYDOWN:

                    int keyCode = m.WParam.ToInt32();
                    if (keyCode == (int)Keys.Up || keyCode == (int)Keys.Down ||
                        keyCode == (int)Keys.PageUp || keyCode == (int)Keys.PageDown ||
                        keyCode == (int)Keys.Home || keyCode == (int)Keys.End)
                    {
                        ScrollMessageReceived?.Invoke(this, EventArgs.Empty);
                    }
                    break;
            }

            base.WndProc(ref m);
        }
    }
}
