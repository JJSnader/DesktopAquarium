using DesktopAquarium.Enums;
using DesktopAquarium.Fish;
using DesktopAquarium.Plants;
using DesktopAquarium.Settings;
using Newtonsoft.Json;
using System.Reflection;

namespace DesktopAquarium
{
    public partial class frmMain : Form
    {
        private AquariumSettings _settings;
        private ImageHelper _imageHelper;
        private JsonSerializerSettings _serializerSettings;
        private NameHelper _nameHelper;

        private int _currentFishID;
        private BaseFishSettings? _newFish;
        private BaseFishSettings? _selectedFish;
        private ImageList _fishImages;

        private int _currentPlantID;
        private BasePlantSettings? _newPlant;
        private BasePlantSettings? _selectedPlant;
        private ImageList _plantImages;

        private const string SettingsFilePath = @"C:\ProgramData\AquariumSettings.json";

        public event EventHandler<EventArgs> Identify;

        public event EventHandler<KillFishEventArgs> KillFish;
        public event EventHandler<FishSettingsChangedEventArgs> FishSettingsChanged;

        public event EventHandler<KillPlantEventArgs> KillPlant;
        public event EventHandler<PlantSettingsChangedEventArgs> PlantSettingsChanged;

        public frmMain()
        {
            InitializeComponent();

            _imageHelper = new ImageHelper();
            _nameHelper = new NameHelper();
            _serializerSettings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto };

            _currentFishID = 0;

            lvFishList.Columns.Add(" ", 40, HorizontalAlignment.Left);
            lvFishList.Columns.Add("Fish Name", -2, HorizontalAlignment.Left);

            _fishImages = new();
            _fishImages.ImageSize = new Size(32, 32);

            var fishTypes = Enum.GetValues(typeof(FishType)).Cast<FishType>().ToList();
            fishTypes.Insert(0, (FishType)(-1));
            cmbFishType.DataSource = fishTypes;
            cmbFishType.SelectedIndex = -1;
            cmbFishType.Format += cmbFishType_Format;

            _currentPlantID = 0;

            lvPlantList.Columns.Add(" ", 40, HorizontalAlignment.Left);
            lvPlantList.Columns.Add("Plant ID", -2, HorizontalAlignment.Left);

            _plantImages = new();
            _plantImages.ImageSize = new Size(32, 32);

            var plantTypes = Enum.GetValues(typeof(PlantType)).Cast<PlantType>().ToList();
            plantTypes.Insert(0, (PlantType)(-1));
            cmbPlantType.DataSource = plantTypes;
            cmbPlantType.SelectedIndex = -1;
            cmbPlantType.Format += cmbPlantType_Format;

            if (File.Exists(SettingsFilePath))
            {
                string raw = File.ReadAllText(SettingsFilePath);
                AquariumSettings? settings = JsonConvert.DeserializeObject<AquariumSettings>(raw, _serializerSettings);
                if (settings != null && settings.FishList.Count > 0)
                {
                    _settings = settings;

                    if (settings.FishList.Count > 0)
                    {
                        var orderedList = settings.FishList
                            .OrderBy(bs => bs.FishType)
                            .ThenBy(bs => bs.Name)
                            .ToList();
                        foreach (BaseFishSettings fish in orderedList)
                        {
                            _currentFishID = Math.Max(_currentFishID, fish.FishID + 1);
                            AddFishToList(fish);
                            OpenFishForm(fish);
                        }
                    }

                    if (settings.PlantList.Count > 0)
                    {
                        var orderedList = settings.PlantList
                            .OrderBy(bs => bs.PlantType)
                            .ThenBy(bs => bs.PlantID)
                            .ToList();
                        foreach (BasePlantSettings plant in orderedList)
                        {
                            _currentPlantID = Math.Max(_currentPlantID, plant.PlantID + 1);
                            AddPlantToList(plant);
                            OpenPlantForm(plant);
                        }
                    }
                }
                else
                    _settings = new();
            }
            else
                _settings = new();

            FormClosing += frmMain_FormClosing;
        }

        #region Fish Code

        private void AddFishToList(BaseFishSettings fish)
        {
            if (fish == null)
                return;

            _fishImages.Images.Add(fish.FishID.ToString(), GetIconForFish(fish.FishType));

            lvFishList.SmallImageList = _fishImages;
            var newItem = new ListViewItem(fish.Name)
            {
                Text = string.Empty,
                Tag = fish.FishID,
                ImageKey = fish.FishID.ToString()
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
                case FishType.Goldfish:
                    return ImageHelper.LoadImageFromBytes(Properties.Resources.GoldfishIcon);
                case FishType.Jellyfish:
                    return ImageHelper.LoadImageFromBytes(Properties.Resources.JellyfishIcon);
                case FishType.Pufferfish:
                    return ImageHelper.LoadImageFromBytes(Properties.Resources.PufferIcon);
                case FishType.Submarine:
                    return ImageHelper.LoadImageFromBytes(Properties.Resources.SubmarineIcon);
                case FishType.SpermWhale:
                    return ImageHelper.LoadImageFromBytes(Properties.Resources.SpermWhaleIcon);
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

        private void CreateControlsForFish(BaseFishSettings settings, FlowLayoutPanel panel)
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
                else if (property.PropertyType == typeof(Scale))
                {
                    Label label = new Label
                    {
                        Text = AddSpacesBeforeCapitals(property.Name),
                        AutoSize = true,
                    };
                    panel.Controls.Add(label);
                    var scales = Enum.GetValues(typeof(Scale)).Cast<Scale>().ToList();
                    ComboBox cb = new()
                    {
                        Name = property.Name,
                        DataSource = scales,
                    };

                    panel.Controls.Add(cb);
                    if (cb.DataSource != null && cb.Items.Count > 0)
                        cb.SelectedIndex = (int)(property.GetValue(settings, null) ?? 0);
                }
                else if (property.PropertyType == typeof(Frequency))
                {
                    Label label = new Label
                    {
                        Text = AddSpacesBeforeCapitals(property.Name),
                        AutoSize = true,
                    };
                    panel.Controls.Add(label);
                    var frequencies = Enum.GetValues(typeof(Frequency)).Cast<Frequency>().ToList();
                    ComboBox cb = new()
                    {
                        Name = property.Name,
                        DataSource = frequencies,
                    };

                    panel.Controls.Add(cb);
                    if (cb.DataSource != null && cb.Items.Count > 0)
                        cb.SelectedIndex = (int)(property.GetValue(settings, null) ?? 0);
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

        private void CreateNewFish(BaseFishSettings settingsToUse)
        {
            if (settingsToUse == null)
                return;

            settingsToUse = GetSettingsFromControls(settingsToUse, flpNewSettings);

            AddFishToList(settingsToUse);

            _settings.FishList.Add(settingsToUse);

            SaveSettings();

            OpenFishForm(settingsToUse);

            _newFish = null;
        }

        private void OpenFishForm(BaseFishSettings settingsToUse)
        {
            BaseFish? frm = null;

            if (settingsToUse.GetType() == typeof(SharkSettings))
            {
                frm = new Shark((SharkSettings)settingsToUse);
            }
            else if (settingsToUse.GetType() == typeof(GoldfishSettings))
            {
                frm = new Goldfish((GoldfishSettings)settingsToUse);
            }
            else if (settingsToUse.GetType() == typeof(JellyfishSettings))
            {
                frm = new Jellyfish((JellyfishSettings)settingsToUse);
            }
            else if (settingsToUse.GetType() == typeof(PufferfishSettings))
            {
                frm = new Pufferfish((PufferfishSettings)settingsToUse);
            }
            else if (settingsToUse.GetType() == typeof(SubmarineSettings))
            {
                frm = new Submarine((SubmarineSettings)settingsToUse);
            }
            else if (settingsToUse.GetType() == typeof(SpermWhaleSettings))
            {
                frm = new SpermWhale((SpermWhaleSettings)settingsToUse);
            }

            if (frm is not null)
            {
                KillFish += frm.KillFish_Raised;
                FishSettingsChanged += frm.SettingsChanged_Raised;
                Identify += frm.IdentifyFish_Raised;
                frm.Show();
            }
        }

        public BaseFishSettings GetSettingsFromControls(BaseFishSettings settings, FlowLayoutPanel panel)
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
                else if (ctrl is ComboBox comboBox)
                {
                    var property = settings.GetType().GetProperty(comboBox.Name);
                    if (property != null && property.PropertyType == typeof(Frequency))
                    {
                        property.SetValue(settings, comboBox.SelectedIndex);
                    }
                    else if (property != null && property.PropertyType == typeof(Scale))
                    {
                        property.SetValue(settings, comboBox.SelectedIndex);
                    }
                }
            }

            return settings;
        }

        #endregion
        #region Plant Code 

        private void AddPlantToList(BasePlantSettings plant)
        {
            if (plant == null)
                return;

            _plantImages.Images.Add(plant.PlantID.ToString(), GetIconForPlant(plant.PlantType));

            lvPlantList.SmallImageList = _plantImages;
            var newItem = new ListViewItem(plant.PlantID.ToString())
            {
                Text = string.Empty,
                Tag = plant.PlantID,
                ImageKey = plant.PlantID.ToString()
            };
            newItem.SubItems.Add(plant.PlantID.ToString());
            lvPlantList.Items.Add(newItem);
        }

        private Image GetIconForPlant(PlantType plant)
        {
            switch (plant)
            {
                case PlantType.Seaweed:
                    return ImageHelper.LoadImageFromBytes(Properties.Resources.SeaweedIcon);
                default:
                    return ImageHelper.LoadImageFromBytes(Properties.Resources.NullIcon);
            }
        }

        private void CreateControlsForPlant(BasePlantSettings settings, FlowLayoutPanel panel)
        {
            Type objType = settings.GetType();
            panel.Controls.Clear();

            foreach (PropertyInfo property in objType.GetProperties())
            {
                if (property.Name == "PlantID" || property.Name == "Location")
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
                else if (property.PropertyType == typeof(Scale))
                {
                    Label label = new Label
                    {
                        Text = AddSpacesBeforeCapitals(property.Name),
                        AutoSize = true,
                    };
                    panel.Controls.Add(label);
                    var scales = Enum.GetValues(typeof(Scale)).Cast<Scale>().ToList();
                    ComboBox cb = new()
                    {
                        Name = property.Name,
                        DataSource = scales,
                    };

                    panel.Controls.Add(cb);
                    if (cb.DataSource != null && cb.Items.Count > 0)
                        cb.SelectedIndex = (int)(property.GetValue(settings, null) ?? 0);
                }
                else if (property.PropertyType == typeof(Frequency))
                {
                    Label label = new Label
                    {
                        Text = AddSpacesBeforeCapitals(property.Name),
                        AutoSize = true,
                    };
                    panel.Controls.Add(label);
                    var frequencies = Enum.GetValues(typeof(Frequency)).Cast<Frequency>().ToList();
                    ComboBox cb = new()
                    {
                        Name = property.Name,
                        DataSource = frequencies,
                    };

                    panel.Controls.Add(cb);
                    if (cb.DataSource != null && cb.Items.Count > 0)
                        cb.SelectedIndex = (int)(property.GetValue(settings, null) ?? 0);
                }
                else if (property.PropertyType != typeof(PlantType))
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
                        Text = property.GetValue(settings, null)?.ToString(),
                    };
                    if (property.Name == "Name" && textBox.Text == string.Empty)
                    {
                        textBox.Text = _nameHelper.GetRandomName();
                    }
                    panel.Controls.Add(textBox);
                }
            }
        }

        private void CreateNewPlant(BasePlantSettings settingsToUse)
        {
            if (settingsToUse == null)
                return;

            settingsToUse = GetSettingsFromControls(settingsToUse, flpNewPlantSettings);

            AddPlantToList(settingsToUse);

            _settings.PlantList.Add(settingsToUse);

            SaveSettings();

            OpenPlantForm(settingsToUse);

            _newPlant = null;
        }

        private void OpenPlantForm(BasePlantSettings settingsToUse)
        {
            BasePlant? frm = null;

            if (settingsToUse.GetType() == typeof(SeaweedSettings))
            {
                frm = new Seaweed((SeaweedSettings)settingsToUse);
            }

            if (frm is not null)
            {
                KillPlant += frm.KillPlant_Raised;
                PlantSettingsChanged += frm.SettingsChanged_Raised;
                Identify += frm.IdentifyPlant_Raised;
                frm.LocationChanged += LocationChanged_Raised;
                frm.Show();
            }
        }

        public BasePlantSettings GetSettingsFromControls(BasePlantSettings settings, FlowLayoutPanel panel)
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
                else if (ctrl is ComboBox comboBox)
                {
                    var property = settings.GetType().GetProperty(comboBox.Name);
                    if (property != null && property.PropertyType == typeof(Frequency))
                    {
                        property.SetValue(settings, comboBox.SelectedIndex);
                    }
                    else if (property != null && property.PropertyType == typeof(Scale))
                    {
                        property.SetValue(settings, comboBox.SelectedIndex);
                    }
                }
            }

            return settings;
        }
        #endregion

        private void SaveSettings()
        {
            var settingsString = JsonConvert.SerializeObject(_settings, _serializerSettings);
            File.WriteAllText(SettingsFilePath, settingsString);
        }

        #region Events

        private void frmMain_FormClosing(object? sender, FormClosingEventArgs e)
        {
            SaveSettings();

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

        #region Fish Tab

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

            foreach (BaseFishSettings fish in _settings.FishList)
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
                    break;
                case FishType.Goldfish:
                    _newFish = new GoldfishSettings();
                    break;
                case FishType.Jellyfish:
                    _newFish = new JellyfishSettings();
                    break;
                case FishType.Pufferfish:
                    _newFish = new PufferfishSettings();
                    break;
                case FishType.Submarine:
                    _newFish = new SubmarineSettings();
                    break;
                case FishType.SpermWhale:
                    _newFish = new SpermWhaleSettings();
                    break;
                default:
                    return;
            }

            _currentFishID++;
            _newFish.FishID = _currentFishID;
            _newFish.FishType = (FishType)type;
            CreateControlsForFish(_newFish, flpNewSettings);
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

            // change fish name in list if it changed
            for (int i = 0; i < lvFishList.Items.Count; ++i)
            {
                if (((int?)lvFishList.Items[i].Tag ?? 0) == _selectedFish.FishID)
                {
                    //var imageKey = lvFishList.Items[i].ImageKey;
                    if (_selectedFish.Name != null)
                    {
                        lvFishList.Items[i].SubItems.Clear();
                        lvFishList.Items[i].SubItems.Add(_selectedFish.Name);
                    }

                    break;
                }

                var orderedList = _settings.FishList
                .OrderBy(bs => bs.FishType)
                .ThenBy(bs => bs.Name)
                .ToList();
                lvFishList.Items.Clear();
                _fishImages.Images.Clear();
                foreach (BaseFishSettings fish in orderedList)
                {
                    _currentFishID = Math.Max(_currentFishID, fish.FishID + 1);
                    AddFishToList(fish);
                }
            }
            FishSettingsChanged?.Invoke(this, new FishSettingsChangedEventArgs(_selectedFish, _selectedFish.FishID));
            SaveSettings();
            _selectedFish = null;
            flpSelectedSettings.Controls.Clear();
            lvFishList.SelectedItems.Clear();
            Application.DoEvents();
        }

        #endregion
        #region Plant Tab 

        private void llRemovePlant_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lvPlantList.SelectedItems.Count == 0)
                return;

            if (!int.TryParse(lvPlantList.SelectedItems[0].Tag?.ToString(), out int PlantID))
                PlantID = -1;

            KillPlant?.Invoke(this, new KillPlantEventArgs(PlantID));

            lvPlantList.Items.Remove(lvPlantList.SelectedItems[0]);

            for (int i = 0; i < _settings.PlantList.Count; i++)
            {
                if (_settings.PlantList[i].PlantID == PlantID)
                {
                    _settings.PlantList.RemoveAt(i);
                    break;
                }
            }
        }

        private void lvPlantList_ItemSelectionChanged(object? sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lvPlantList.SelectedItems.Count == 0)
                return;

            var selectedPlant = e.Item;
            if (selectedPlant == null)
                return;

            if (!int.TryParse(selectedPlant.Tag?.ToString(), out int PlantID))
                PlantID = -1;

            foreach (BasePlantSettings Plant in _settings.PlantList)
            {
                if (Plant.PlantID == PlantID)
                {
                    CreateControlsForPlant(Plant, flpSelectedPlantSettings);
                    _selectedPlant = Plant;
                    break;
                }
            }
        }

        private void cmbPlantType_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_newPlant != null)
            {
                if (MessageBox.Show("This new plant has not been saved. Do you want to save your changes?", "Unsaved Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    CreateNewPlant(_newPlant);
                }
                else
                {
                    _newPlant = null;
                    flpNewPlantSettings.Controls.Clear();
                }
            }
            PlantType? type = cmbPlantType.SelectedItem as PlantType?;
            if (type == null)
                return;

            switch (type)
            {
                case PlantType.Seaweed:
                    _newPlant = new SeaweedSettings();
                    break;
                default:
                    return;
            }

            _currentPlantID++;
            _newPlant.PlantID = _currentPlantID;
            _newPlant.PlantType = (PlantType)type;
            CreateControlsForPlant(_newPlant, flpNewPlantSettings);
        }

        private void cmbPlantType_Format(object? sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem != null && (PlantType)e.ListItem == (PlantType)(-1))
            {
                e.Value = "No Option Selected";
            }
            else
            {
                e.Value = e.ListItem?.ToString(); // Display the enum value as the default
            }
        }

        private void btnCreatePlant_Click(object sender, EventArgs e)
        {
            PlantType? type = cmbPlantType.SelectedItem as PlantType?;
            if (type == null)
                return;

            if (type == (PlantType)(-1) || _newPlant == null)
            {
                MessageBox.Show("There is no new Plant loaded.");
                return;
            }

            CreateNewPlant(_newPlant);
            flpNewPlantSettings.Controls.Clear();
        }

        private void btnSavePlant_Click(object sender, EventArgs e)
        {
            if (flpSelectedPlantSettings.Controls.Count == 0 || _selectedPlant == null)
            {
                MessageBox.Show("No plant is selected.");
                return;
            }

            _selectedPlant = GetSettingsFromControls(_selectedPlant, flpSelectedPlantSettings);

            for (int i = 0; i < _settings.PlantList.Count; ++i)
            {
                if (_settings.PlantList[i].PlantID == _selectedPlant.PlantID)
                {
                    _settings.PlantList[i] = _selectedPlant;
                    break;
                }
            }

            // change Plant name in list if it changed
            for (int i = 0; i < lvPlantList.Items.Count; ++i)
            {
                if (((int?)lvPlantList.Items[i].Tag ?? 0) == _selectedPlant.PlantID)
                {
                    if (_selectedPlant.PlantID > 0)
                    {
                        lvPlantList.Items[i].SubItems.Clear();
                        lvPlantList.Items[i].SubItems.Add(_selectedPlant.PlantID.ToString());
                    }

                    break;
                }

                var orderedList = _settings.PlantList
                .OrderBy(bs => bs.PlantType)
                .ThenBy(bs => bs.PlantID)
                .ToList();
                lvPlantList.Items.Clear();
                _plantImages.Images.Clear();
                foreach (BasePlantSettings Plant in orderedList)
                {
                    _currentPlantID = Math.Max(_currentPlantID, Plant.PlantID + 1);
                    AddPlantToList(Plant);
                }
            }

            PlantSettingsChanged?.Invoke(this, new PlantSettingsChangedEventArgs(_selectedPlant, _selectedPlant.PlantID));
            SaveSettings();
            _selectedPlant = null;
            flpSelectedPlantSettings.Controls.Clear();
            lvPlantList.SelectedItems.Clear();
            Application.DoEvents();
        }

        private void LocationChanged_Raised(object? sender, int plantID)
        {
            SaveSettings();
        }

        #endregion

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

        private void btnIdentifyFish_Click(object sender, EventArgs e)
        {
            Identify?.Invoke(this, e);
        }

        private void llExit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Application.Exit();
        }

        private void tcMain_DrawItem(object sender, DrawItemEventArgs e)
        {
            var tabControl = (TabControl)sender;
            var tabPage = tabControl.TabPages[e.Index];
            var bounds = tabControl.GetTabRect(e.Index);
            bool selected = e.Index == tabControl.SelectedIndex;

            Color baseColor = Color.FromArgb(0, 79, 111);
            Color backColor = selected
                ? baseColor
                : ControlPaint.Light(baseColor, 0.2f);

            using var backBrush = new SolidBrush(backColor);
            using var textBrush = new SolidBrush(Color.White);

            e.Graphics.FillRectangle(backBrush, bounds);

            TextRenderer.DrawText(
                e.Graphics,
                tabPage.Text,
                tabControl.Font,
                bounds,
                Color.White,
                TextFormatFlags.HorizontalCenter |
                TextFormatFlags.VerticalCenter |
                TextFormatFlags.SingleLine
            );
        }

        private void btnFishTab_Click(object sender, EventArgs e)
        {
            if (tcMain.SelectedTab == tabPlants)
            {
                btnFishTab.BackColor = Color.FromArgb(255, 0, 105, 148);
                btnPlantTab.BackColor = Color.FromArgb(255, 0, 79, 111);

                btnFishTab.Location = new Point(btnFishTab.Location.X, btnFishTab.Location.Y - 3);
                btnPlantTab.Location = new Point(btnPlantTab.Location.X, btnPlantTab.Location.Y + 3);
                tcMain.SelectedTab = tabFish;
            }
        }

        private void btnPlantTab_Click(object sender, EventArgs e)
        {
            if (tcMain.SelectedTab == tabFish)
            {
                btnFishTab.BackColor = Color.FromArgb(255, 0, 79, 111);
                btnPlantTab.BackColor = Color.FromArgb(255, 0, 105, 148);

                btnFishTab.Location = new Point(btnFishTab.Location.X, btnFishTab.Location.Y + 3);
                btnPlantTab.Location = new Point(btnPlantTab.Location.X, btnPlantTab.Location.Y - 3);
                tcMain.SelectedTab = tabPlants;
            }

        }

        #endregion
    }
}
