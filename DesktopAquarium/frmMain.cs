using Newtonsoft.Json;

using DesktopAquarium.Fish;
using DesktopAquarium.Settings;
using System.Reflection;

namespace DesktopAquarium
{
    public partial class frmMain : Form
    {
        private AquariumSettings _settings;
        private BaseSettings? _newFish;
        private BaseSettings? _selectedFish;
        private ImageHelper _imageHelper;
        private ImageList _fishImages;
        private JsonSerializerSettings _serializerSettings;
        private NameHelper _nameHelper;

        private int _currentFishID;

        private const string SettingsFilePath = @"C:\ProgramData\AquariumSettings.json";

        public event EventHandler<KillFishEventArgs> KillFish;
        public event EventHandler<SettingsChangedEventArgs> SettingsChanged;

        public frmMain()
        {
            InitializeComponent();

            _imageHelper = new ImageHelper();
            _nameHelper = new NameHelper();
            _currentFishID = 0;

            lvFishList.Columns.Add(" ", 40, HorizontalAlignment.Left);
            lvFishList.Columns.Add("Fish Name", -2, HorizontalAlignment.Left);

            _fishImages = new ImageList();
            _fishImages.ImageSize = new Size(32, 32);

            _serializerSettings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto };

            var fishTypes = Enum.GetValues(typeof(FishType)).Cast<FishType>().ToList();
            fishTypes.Insert(0, (FishType)(-1));
            cmbFishType.DataSource = fishTypes;
            cmbFishType.SelectedIndex = -1;
            cmbFishType.Format += cmbFishType_Format;

            if (File.Exists(SettingsFilePath))
            {
                string raw = File.ReadAllText(SettingsFilePath);
                AquariumSettings? settings = JsonConvert.DeserializeObject<AquariumSettings>(raw, _serializerSettings);
                if (settings != null && settings.FishList.Count > 0)
                {
                    _settings = settings;
                    foreach (BaseSettings fish in settings.FishList)
                    {
                        _currentFishID = Math.Max(_currentFishID, fish.FishID + 1);
                        AddFishToList(fish);
                        OpenFishForm(fish);
                    }
                }
                else
                    _settings = new();
            }
            else
                _settings = new();

            FormClosing += frmMain_FormClosing;
        }

        private void AddFishToList(BaseSettings fish)
        {
            if (fish == null)
                return;

            _fishImages.Images.Add(fish.Name ?? fish.FishType.ToString(), GetIconForFish(fish.FishType));

            lvFishList.SmallImageList = _fishImages;
            var newItem = new ListViewItem(fish.Name)
            {
                Text = string.Empty,
                Tag = fish.FishID,
                ImageKey = fish.Name ?? fish.FishType.ToString()
            };
            newItem.SubItems.Add(fish.Name);
            lvFishList.Items.Add(newItem);
        }

        private Image GetIconForFish(FishType fish)
        {
            switch (fish)
            {
                case FishType.Shark:
                    return ImageHelper.LoadImageFromBytes(Properties.Resources.SharkIcon);
                default:
                    return ImageHelper.LoadImageFromBytes(Properties.Resources.NullIcon);
            }
        }


        public string AddSpacesBeforeCapitals(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var result = new System.Text.StringBuilder();
            char previousChar = '\0';

            foreach (char ch in input)
            {
                if (char.IsUpper(ch) && result.Length > 0 && !char.IsUpper(previousChar))
                {
                    result.Append(' ');
                }
                result.Append(ch);
                previousChar = ch;
            }

            return result.ToString();
        }

        private void CreateControlsForFish(BaseSettings settings, FlowLayoutPanel panel)
        {
            Type objType = settings.GetType();
            panel.Controls.Clear();

            foreach (PropertyInfo property in objType.GetProperties())
            {
                if (property.Name == "FishID")
                    continue;

                if (property.PropertyType == typeof(int))
                {
                    Label label = new Label
                    {
                        Text = AddSpacesBeforeCapitals(property.Name),
                        AutoSize = true,
                    };
                    panel.Controls.Add(label);
                    NumericUpDown numericUpDown = new NumericUpDown
                    {
                        Name = property.Name,
                        Minimum = 0,
                        Maximum = 10000,
                        Value = (int?)property.GetValue(settings, null) ?? 0
                    };
                    panel.Controls.Add(numericUpDown);
                }
                else if (property.PropertyType == typeof(bool))
                {
                    CheckBox checkBox = new()
                    {
                        Name = property.Name,
                        Text = AddSpacesBeforeCapitals(property.Name),
                        AutoSize = true,
                        Checked = (bool?)property.GetValue(settings, null) ?? false
                    };
                    panel.Controls.Add(checkBox);
                }
                else if (property.PropertyType != typeof(FishType))
                {
                    Label label = new Label
                    {
                        Text = AddSpacesBeforeCapitals(property.Name),
                        AutoSize = true,
                    };
                    panel.Controls.Add(label);
                    TextBox textBox = new()
                    {
                        Name = property.Name,
                        Text = (string?)property.GetValue(settings, null),
                    };
                    if (property.Name == "Name" && textBox.Text == string.Empty)
                    {
                        textBox.Text = _nameHelper.GetRandomName();
                    }
                    panel.Controls.Add(textBox);
                }
            }
        }

        private void CreateNewFish(BaseSettings settingsToUse)
        {
            if (settingsToUse == null)
                return;

            settingsToUse = GetSettingsFromControls(settingsToUse, flpNewSettings);

            AddFishToList(settingsToUse);

            _settings.FishList.Add(settingsToUse);

            OpenFishForm(settingsToUse);

            _newFish = null;
        }

        private void OpenFishForm(BaseSettings settingsToUse)
        {
            if (settingsToUse.GetType() == typeof(SharkSettings))
            {
                var frm = new Shark((SharkSettings)settingsToUse);
                KillFish += frm.KillFish_Raised;
                SettingsChanged += frm.SettingsChanged_Raised;
                frm.Show();
            }
        }

        public BaseSettings GetSettingsFromControls(BaseSettings settings, FlowLayoutPanel panel)
        {
            foreach (Control ctrl in panel.Controls)
            {
                if (ctrl is NumericUpDown numericUpDown)
                {
                    var property = settings.GetType().GetProperty(numericUpDown.Name);
                    if (property != null && property.PropertyType == typeof(int))
                    {
                        property.SetValue(settings, (int)numericUpDown.Value);
                    }
                }
                else if (ctrl is CheckBox checkBox)
                {
                    var property = settings.GetType().GetProperty(checkBox.Name);
                    if (property != null && property.PropertyType == typeof(bool))
                    {
                        property.SetValue(settings, checkBox.Checked);
                    }
                }
                else if (ctrl is TextBox textBox)
                {
                    var property = settings.GetType().GetProperty(textBox.Name);
                    if (property != null && property.PropertyType == typeof(string))
                    {
                        property.SetValue(settings, textBox.Text);
                    }
                }
            }

            return settings;
        }

        #region Events

        private void frmMain_FormClosing(object? sender, FormClosingEventArgs e)
        {
            var settingsString = JsonConvert.SerializeObject(_settings, _serializerSettings);
            File.WriteAllText(SettingsFilePath, settingsString);

            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            else
            {
                notifyIcon1.Dispose();
            }
        }

        private void llRemoveFish_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lvFishList.SelectedItems.Count == 0)
                return;

            if (!int.TryParse(lvFishList.SelectedItems[0].Tag?.ToString(), out int fishID))
                fishID = -1;

            KillFish?.Invoke(this, new KillFishEventArgs(fishID));

            lvFishList.Items.Remove(lvFishList.SelectedItems[0]);

            for (int i = 0; i < _settings.FishList.Count; i++)
            {
                if (_settings.FishList[i].FishID == fishID)
                {
                    _settings.FishList.RemoveAt(i);
                    break;
                }
            }
        }

        private void lvFishList_ItemSelectionChanged(object? sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lvFishList.SelectedItems.Count == 0)
                return;

            var selectedFish = e.Item;
            if (selectedFish == null)
                return;

            if (!int.TryParse(selectedFish.Tag?.ToString(), out int fishID))
                fishID = -1;

            foreach (BaseSettings fish in _settings.FishList)
            {
                if (fish.FishID == fishID)
                {
                    CreateControlsForFish(fish, flpSelectedSettings);
                    _selectedFish = fish;
                    break;
                }
            }
        }

        private void cmbFishType_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_newFish != null)
            {
                if (MessageBox.Show("This new fish has not been saved. Do you want to save your changes?", "Unsaved Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    CreateNewFish(_newFish);
                }
                else
                {
                    _newFish = null;
                    flpNewSettings.Controls.Clear();
                }
            }
            FishType? type = cmbFishType.SelectedItem as FishType?;
            if (type == null)
                return;

            switch (type)
            {
                case FishType.Shark:
                    _newFish = new SharkSettings();
                    _currentFishID++;
                    _newFish.FishID = _currentFishID;
                    CreateControlsForFish(_newFish, flpNewSettings);
                    break;
                default:
                    return;
            }
        }

        private void cmbFishType_Format(object? sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem != null && (FishType)e.ListItem == (FishType)(-1))
            {
                e.Value = "No Option Selected";
            }
            else
            {
                e.Value = e.ListItem?.ToString(); // Display the enum value as the default
            }
        }

        private void btnCreateFish_Click(object sender, EventArgs e)
        {
            FishType? type = cmbFishType.SelectedItem as FishType?;
            if (type == null)
                return;

            if (type == (FishType)(-1) || _newFish == null)
            {
                MessageBox.Show("There is no new fish loaded.");
                return;
            }

            CreateNewFish(_newFish);
            flpNewSettings.Controls.Clear();
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            if (flpSelectedSettings.Controls.Count == 0 || _selectedFish == null)
            {
                MessageBox.Show("No fish is selected.");
                return;
            }

            _selectedFish = GetSettingsFromControls(_selectedFish, flpSelectedSettings);

            for (int i = 0; i < _settings.FishList.Count; ++i)
            {
                if (_settings.FishList[i].FishID == _selectedFish.FishID)
                {
                    _settings.FishList[i] = _selectedFish;
                    break;
                }
            }
            SettingsChanged?.Invoke(this, new SettingsChangedEventArgs(_selectedFish, _selectedFish.FishID));
            _selectedFish = null;
            flpSelectedSettings.Controls.Clear();
            lvFishList.SelectedItems.Clear();
            Application.DoEvents();
        }

        private void llCredits_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new frmCredit();
            frm.Show();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
        }

        private void btnManage_Click(object sender, EventArgs e)
        {
            Show();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion
    }
}
