using WinFormsComponents.Classes.Model;
using WinFormsComponents.Classes;

namespace WinFormsComponents
{
    public partial class ConnectingSettingsControl : UserControl
    {
        private List<ConnectionElement> Connections { get; set; }
        private Form ThisForm { get; set; }

        public ConnectingSettingsControl()
        {
            InitializeComponent();
        }

        private void SettingsConnectControlOnLoad(object sender, EventArgs e)
        {
            ThisForm = this.GetForm();

            if (!File.Exists("connection_list.json"))
            {
                Connections = new List<ConnectionElement>();
                Connections.Add(new(true, ConnectionInfo.DefaultConnection, true));
                Connections.ToArray().Save();
            }

            LoadConnectionList();
        }

        /// <summary>
        /// Загрузка списка соединений
        /// </summary>
        private void LoadConnectionList()
        {
            lvConnections.Items.Clear();
            Connections = ConnectionInfo.Conections.ToList();

            foreach (ConnectionElement conection in Connections)
            {
                ListViewItem item = new(conection.Name);
                item.Tag = conection;
                item.ImageIndex = 1;

                if (conection.IsNotDelete)
                {
                    item.BackColor = Color.LightBlue;
                    item.ImageIndex = 2;
                }
                if (conection.IsActive)
                {
                    item.BackColor = Color.LightGreen;
                    item.ImageIndex = 0;
                    LoadConectionInfo(conection);
                    item.Selected = true;
                }
                if (conection.IsDiconnect)
                {
                    item.BackColor = Color.MistyRose;
                    item.ImageIndex = 3;
                }

                lvConnections.Items.Add(item);
            }
        }

        /// <summary>
        /// Загрузка информации о соединении
        /// </summary>
        /// <param name="connectionElement"></param>
        private void LoadConectionInfo(ConnectionElement connectionElement)
        {
            tbHost.Text = connectionElement.ConnectionBuilder.Host;
            tbPort.Text = connectionElement.ConnectionBuilder.Port.ToString();
            tbLogin.Text = connectionElement.ConnectionBuilder.Username;
            tbPassword.Text = connectionElement.ConnectionBuilder.Password;
            tbDataBase.Text = connectionElement.ConnectionBuilder.Database;
        }

        private void btLockPasswordOnClick(object sender, EventArgs e)
        {
            if (tbPassword.PasswordChar.Equals('\0'))
            {
                tbPassword.PasswordChar = '*';
                btLockPassword.BackgroundImage = Properties.Resources.lock_text;
            }
            else
            {
                tbPassword.PasswordChar = '\0';
                btLockPassword.BackgroundImage = Properties.Resources.unlock_text;
            }
        }

        private void tsbSaveOnClick(object sender, EventArgs e) => SaveConnections();

        /// <summary>
        /// Обработка события сохранения
        /// </summary>
        /// <param name="actionSave">Действие при сохранении: 0 - Изменение, 1 - Добавление, 2 - Удаление</param>
        private void SaveConnections(int actionSave = 0)
        {


            ThisForm.InterfaceLock(tspbProgress);
            tslProgress.Text = "Проверка соединения...";

            ConnectionElement connection = new(true, tbHost.Text, Convert.ToInt32(tbPort.Text), tbLogin.Text, tbPassword.Text, tbDataBase.Text);

            if (actionSave != 2 && !connection.ConnectionBuilder.IsCheckConection())
            {
                tslProgress.Text = "Соединение не установлено!";
                DialogResult result = InfoViewer.QuestionMessege("Соединение не установлено!\nСохранить соединение?", MessageBoxButtons.YesNoCancel);

                switch (result)
                {
                    case DialogResult.Yes:
                        connection.IsDiconnect = true;
                        connection.IsActive = false;
                        break;
                    case DialogResult.Cancel:
                    case DialogResult.No:
                        ThisForm.InterfaceUnlock(tspbProgress);
                        return;
                }
            }

            tslProgress.Text = "Сохранение...";

            Connections.ForEach(i => i.IsActive = false);

            switch (actionSave)
            {
                case 0:
                    Connections[Connections.IndexOf((ConnectionElement)lvConnections.SelectedItems[0].Tag)] = connection;
                    break;
                case 1:
                    Connections.Add(connection);
                    break;
                case 2:
                    foreach (ListViewItem item in lvConnections.SelectedItems)
                    {
                        Connections.RemoveAt(Connections.IndexOf((ConnectionElement)item.Tag));
                    }
                    break;
            }

            if (!Connections.Any(i => i.IsActive))
            {
                Connections.FirstOrDefault(i => i.IsNotDelete).IsActive = true;
            }

            Connections.ToArray().Save();
            LoadConnectionList();

            ThisForm.InterfaceUnlock(tspbProgress);
        }

        private void tbPortOnKeyPress(object sender, KeyPressEventArgs e) => e.NumRestrictionTextBox();

        private void tspbProgressOnVisibleChanged(object sender, EventArgs e)
        {
            tslProgress.Enabled = tslProgress.Visible = tspbProgress.Visible;
        }

        private void btLockPasswordOnEnabledChanged(object sender, EventArgs e)
        {
            btLockPassword.BackgroundImage = btLockPassword.Enabled ?
                                                tbPassword.PasswordChar.Equals('\0') ?
                                                    Properties.Resources.unlock_text :
                                                    Properties.Resources.lock_text :
                                                Properties.Resources.uneneble_lock_text;
        }

        private void tsmiAddConectionOnClick(object sender, EventArgs e) => SaveConnections(1);

        private void tsmiDeleteConectionOnClick(object sender, EventArgs e) => SaveConnections(2);

        private void cmsConectionListOnOpening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            IEnumerable<ListViewItem> items = lvConnections.SelectedItems.Cast<ListViewItem>();
            ConnectionElement firstConnect = (ConnectionElement)items.FirstOrDefault()?.Tag;

            tsmiAddConection.Enabled = lvConnections.SelectedItems.Count == 0;
            tsmiDeleteConection.Enabled = lvConnections.SelectedItems.Count > 0 && items.All(i => !((ConnectionElement)i.Tag).IsNotDelete);
            tsmiConnect.Enabled = lvConnections.SelectedItems.Count == 1 && !firstConnect.IsDiconnect && !firstConnect.IsActive;
        }

        private void lvConnectionsOnSelectedIndexChanged(object sender, EventArgs e)
        {
            IEnumerable<ListViewItem> items = lvConnections.SelectedItems.Cast<ListViewItem>();
            ConnectionElement firstConnect = (ConnectionElement)items.FirstOrDefault()?.Tag;

            tsbSave.Enabled = lvConnections.SelectedItems.Count == 1 && !firstConnect.IsNotDelete;
            tsbConnect.Enabled = lvConnections.SelectedItems.Count == 1 && !firstConnect.IsDiconnect && !firstConnect.IsActive;
            tsbDelete.Enabled = lvConnections.SelectedItems.Count > 0 && items.All(i => !((ConnectionElement)i.Tag).IsNotDelete);
            tbHost.Enabled =
            tbPort.Enabled =
            tbLogin.Enabled =
            tbPassword.Enabled =
            tbDataBase.Enabled =
            tsbAdd.Enabled = lvConnections.SelectedItems.Count <= 1;

            if (lvConnections.SelectedItems.Count == 1)
            {
                tbHost.Text = firstConnect.ConnectionBuilder.Host;
                tbPort.Text = firstConnect.ConnectionBuilder.Port.ToString();
                tbLogin.Text = firstConnect.ConnectionBuilder.Username;
                tbPassword.Text = firstConnect.ConnectionBuilder.Password;
                tbDataBase.Text = firstConnect.ConnectionBuilder.Database;
            }
            else
            {
                tbHost.Clear();
                tbPort.Clear();
                tbLogin.Clear();
                tbPassword.Clear();
                tbDataBase.Clear();
            }
        }

        async private void tbPortOnTextChanged(object sender, EventArgs e)
        {
            if (!await tbPort.TextEmptyTextBox())
            {
                tbPort.Text = Properties.Settings.Default.Port.ToString();
            }
        }

        async private void tbHostOnTextChanged(object sender, EventArgs e)
        {
            if (!await tbHost.TextEmptyTextBox())
            {
                tbHost.Text = Properties.Settings.Default.Host;
            }
        }

        private void tsbConnectOnClick(object sender, EventArgs e)
        {
            ConnectionElement connection = (ConnectionElement)lvConnections.SelectedItems[0].Tag;

            Connections.ForEach(i => i.IsActive = false);
            Connections[Connections.IndexOf(connection)].IsActive = true;

            Connections.ToArray().Save();
            LoadConnectionList();
        }

        private void lvConnectionsOnKeyDown(object sender, KeyEventArgs e)
        {
            IEnumerable<ListViewItem> items = lvConnections.SelectedItems.Cast<ListViewItem>();
            ConnectionElement firstConnect = (ConnectionElement)items.FirstOrDefault()?.Tag;

            switch (e.KeyCode)
            {
                case Keys.Delete
                when lvConnections.SelectedItems.Count > 0 && items.All(i => !((ConnectionElement)i.Tag).IsNotDelete):
                    SaveConnections(2);
                    break;
                case Keys.Enter
                when lvConnections.SelectedItems.Count == 1 && !firstConnect.IsDiconnect && !firstConnect.IsActive:
                    tsbConnectOnClick(null, null);
                    break;
                case Keys.Insert
                when lvConnections.SelectedItems.Count <= 1:
                    SaveConnections(1);
                    break;
                case Keys.S when e.Control && lvConnections.SelectedItems.Count == 1 && !firstConnect.IsNotDelete:
                    SaveConnections();
                    e.SuppressKeyPress = true;
                    break;
            }
        }
    }
}
