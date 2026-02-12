namespace DesktopAquarium
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            lvFishList = new ListView();
            label1 = new Label();
            label2 = new Label();
            llRemoveFish = new LinkLabel();
            groupBox1 = new GroupBox();
            btnCreateFish = new Button();
            label4 = new Label();
            flpNewSettings = new FlowLayoutPanel();
            label3 = new Label();
            cmbFishType = new ComboBox();
            groupBox2 = new GroupBox();
            btnSaveSettings = new Button();
            label5 = new Label();
            flpSelectedSettings = new FlowLayoutPanel();
            llCredits = new LinkLabel();
            notifyIcon1 = new NotifyIcon(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            btnManage = new ToolStripMenuItem();
            btnQuit = new ToolStripMenuItem();
            btnIdentifyFish = new Button();
            llExit = new LinkLabel();
            tcMain = new TabControl();
            tabFish = new TabPage();
            tabPlants = new TabPage();
            label7 = new Label();
            lvPlantList = new ListView();
            llRemovePlant = new LinkLabel();
            gbNewPlant = new GroupBox();
            btnCreatePlant = new Button();
            button1 = new Button();
            label8 = new Label();
            flpNewPlantSettings = new FlowLayoutPanel();
            label9 = new Label();
            cmbPlantType = new ComboBox();
            gbEditPlant = new GroupBox();
            btnSavePlant = new Button();
            button2 = new Button();
            label10 = new Label();
            flpSelectedPlantSettings = new FlowLayoutPanel();
            btnFishTab = new Button();
            btnPlantTab = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            tcMain.SuspendLayout();
            tabFish.SuspendLayout();
            tabPlants.SuspendLayout();
            gbNewPlant.SuspendLayout();
            gbEditPlant.SuspendLayout();
            SuspendLayout();
            // 
            // lvFishList
            // 
            lvFishList.BackColor = Color.FromArgb(0, 79, 111);
            lvFishList.BorderStyle = BorderStyle.None;
            lvFishList.ForeColor = SystemColors.Window;
            lvFishList.FullRowSelect = true;
            lvFishList.Location = new Point(0, 30);
            lvFishList.Margin = new Padding(5, 6, 5, 6);
            lvFishList.MultiSelect = false;
            lvFishList.Name = "lvFishList";
            lvFishList.Size = new Size(296, 685);
            lvFishList.TabIndex = 0;
            lvFishList.UseCompatibleStateImageBehavior = false;
            lvFishList.View = View.Details;
            lvFishList.ItemSelectionChanged += lvFishList_ItemSelectionChanged;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.Font = new Font("Segoe UI", 30F);
            label1.ForeColor = Color.WhiteSmoke;
            label1.Location = new Point(19, 10);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(1221, 82);
            label1.TabIndex = 1;
            label1.Text = "Aquarium Manager";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15F);
            label2.ForeColor = Color.WhiteSmoke;
            label2.Location = new Point(0, 0);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(80, 28);
            label2.TabIndex = 2;
            label2.Text = "Fish List";
            // 
            // llRemoveFish
            // 
            llRemoveFish.AutoSize = true;
            llRemoveFish.ForeColor = Color.WhiteSmoke;
            llRemoveFish.LinkColor = Color.WhiteSmoke;
            llRemoveFish.Location = new Point(220, 0);
            llRemoveFish.Name = "llRemoveFish";
            llRemoveFish.Size = new Size(78, 28);
            llRemoveFish.TabIndex = 3;
            llRemoveFish.TabStop = true;
            llRemoveFish.Text = "Kill Fish";
            llRemoveFish.VisitedLinkColor = Color.Black;
            llRemoveFish.LinkClicked += llRemoveFish_LinkClicked;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(btnCreateFish);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(flpNewSettings);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(cmbFishType);
            groupBox1.ForeColor = Color.WhiteSmoke;
            groupBox1.Location = new Point(304, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(917, 344);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Create New Fish";
            // 
            // btnCreateFish
            // 
            btnCreateFish.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCreateFish.BackColor = Color.Teal;
            btnCreateFish.FlatStyle = FlatStyle.Flat;
            btnCreateFish.ForeColor = Color.White;
            btnCreateFish.Location = new Point(6, 268);
            btnCreateFish.Name = "btnCreateFish";
            btnCreateFish.Size = new Size(203, 70);
            btnCreateFish.TabIndex = 4;
            btnCreateFish.Text = "Create Fish";
            btnCreateFish.UseVisualStyleBackColor = false;
            btnCreateFish.Click += btnCreateFish_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(215, 30);
            label4.Name = "label4";
            label4.Size = new Size(166, 28);
            label4.TabIndex = 3;
            label4.Text = "New Fish Settings";
            // 
            // flpNewSettings
            // 
            flpNewSettings.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flpNewSettings.FlowDirection = FlowDirection.TopDown;
            flpNewSettings.Location = new Point(215, 61);
            flpNewSettings.Name = "flpNewSettings";
            flpNewSettings.Size = new Size(696, 277);
            flpNewSettings.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 30);
            label3.Name = "label3";
            label3.Size = new Size(92, 28);
            label3.TabIndex = 1;
            label3.Text = "Fish Type";
            // 
            // cmbFishType
            // 
            cmbFishType.BackColor = Color.Teal;
            cmbFishType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFishType.FlatStyle = FlatStyle.Flat;
            cmbFishType.ForeColor = Color.White;
            cmbFishType.FormattingEnabled = true;
            cmbFishType.Location = new Point(6, 61);
            cmbFishType.Name = "cmbFishType";
            cmbFishType.Size = new Size(203, 36);
            cmbFishType.TabIndex = 0;
            cmbFishType.SelectedIndexChanged += cmbFishType_SelectedIndexChanged;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(btnSaveSettings);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(flpSelectedSettings);
            groupBox2.ForeColor = Color.White;
            groupBox2.Location = new Point(304, 350);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(917, 365);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "Edit Selected Fish";
            // 
            // btnSaveSettings
            // 
            btnSaveSettings.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSaveSettings.BackColor = Color.Teal;
            btnSaveSettings.FlatStyle = FlatStyle.Flat;
            btnSaveSettings.ForeColor = Color.White;
            btnSaveSettings.Location = new Point(6, 289);
            btnSaveSettings.Name = "btnSaveSettings";
            btnSaveSettings.Size = new Size(203, 70);
            btnSaveSettings.TabIndex = 4;
            btnSaveSettings.Text = "Save New Settings";
            btnSaveSettings.UseVisualStyleBackColor = false;
            btnSaveSettings.Click += btnSaveSettings_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(215, 51);
            label5.Name = "label5";
            label5.Size = new Size(201, 28);
            label5.TabIndex = 3;
            label5.Text = "Selected Fish Settings";
            // 
            // flpSelectedSettings
            // 
            flpSelectedSettings.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flpSelectedSettings.FlowDirection = FlowDirection.TopDown;
            flpSelectedSettings.Location = new Point(215, 82);
            flpSelectedSettings.Name = "flpSelectedSettings";
            flpSelectedSettings.Size = new Size(696, 277);
            flpSelectedSettings.TabIndex = 2;
            // 
            // llCredits
            // 
            llCredits.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            llCredits.AutoSize = true;
            llCredits.Font = new Font("Segoe UI", 12F);
            llCredits.LinkColor = Color.WhiteSmoke;
            llCredits.Location = new Point(12, 7);
            llCredits.Name = "llCredits";
            llCredits.Size = new Size(59, 21);
            llCredits.TabIndex = 0;
            llCredits.TabStop = true;
            llCredits.Text = "Credits";
            llCredits.LinkClicked += llCredits_LinkClicked;
            // 
            // notifyIcon1
            // 
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "Desktop Aquarium";
            notifyIcon1.Visible = true;
            notifyIcon1.DoubleClick += notifyIcon1_DoubleClick;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { btnManage, btnQuit });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(118, 48);
            // 
            // btnManage
            // 
            btnManage.Name = "btnManage";
            btnManage.Size = new Size(117, 22);
            btnManage.Text = "Manage";
            btnManage.Click += btnManage_Click;
            // 
            // btnQuit
            // 
            btnQuit.Name = "btnQuit";
            btnQuit.Size = new Size(117, 22);
            btnQuit.Text = "Exit";
            btnQuit.Click += btnQuit_Click;
            // 
            // btnIdentifyFish
            // 
            btnIdentifyFish.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnIdentifyFish.BackColor = Color.Teal;
            btnIdentifyFish.FlatStyle = FlatStyle.Flat;
            btnIdentifyFish.ForeColor = Color.White;
            btnIdentifyFish.Location = new Point(1044, 29);
            btnIdentifyFish.Name = "btnIdentifyFish";
            btnIdentifyFish.Size = new Size(203, 74);
            btnIdentifyFish.TabIndex = 5;
            btnIdentifyFish.Text = "Identify";
            btnIdentifyFish.UseVisualStyleBackColor = false;
            btnIdentifyFish.Click += btnIdentifyFish_Click;
            // 
            // llExit
            // 
            llExit.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            llExit.AutoSize = true;
            llExit.Font = new Font("Segoe UI", 8.25F);
            llExit.LinkColor = Color.WhiteSmoke;
            llExit.Location = new Point(1157, 7);
            llExit.Name = "llExit";
            llExit.Size = new Size(87, 13);
            llExit.TabIndex = 6;
            llExit.TabStop = true;
            llExit.Text = "Exit Application";
            llExit.LinkClicked += llExit_LinkClicked;
            // 
            // tcMain
            // 
            tcMain.Appearance = TabAppearance.FlatButtons;
            tcMain.Controls.Add(tabFish);
            tcMain.Controls.Add(tabPlants);
            tcMain.DrawMode = TabDrawMode.OwnerDrawFixed;
            tcMain.ItemSize = new Size(0, 1);
            tcMain.Location = new Point(19, 109);
            tcMain.Name = "tcMain";
            tcMain.SelectedIndex = 0;
            tcMain.Size = new Size(1228, 727);
            tcMain.SizeMode = TabSizeMode.Fixed;
            tcMain.TabIndex = 9;
            tcMain.DrawItem += tcMain_DrawItem;
            // 
            // tabFish
            // 
            tabFish.BackColor = Color.FromArgb(0, 105, 148);
            tabFish.Controls.Add(label2);
            tabFish.Controls.Add(lvFishList);
            tabFish.Controls.Add(llRemoveFish);
            tabFish.Controls.Add(groupBox1);
            tabFish.Controls.Add(groupBox2);
            tabFish.Location = new Point(4, 5);
            tabFish.Name = "tabFish";
            tabFish.Padding = new Padding(3);
            tabFish.Size = new Size(1220, 718);
            tabFish.TabIndex = 0;
            tabFish.Text = "Fish";
            // 
            // tabPlants
            // 
            tabPlants.BackColor = Color.FromArgb(0, 105, 148);
            tabPlants.Controls.Add(label7);
            tabPlants.Controls.Add(lvPlantList);
            tabPlants.Controls.Add(llRemovePlant);
            tabPlants.Controls.Add(gbNewPlant);
            tabPlants.Controls.Add(gbEditPlant);
            tabPlants.Location = new Point(4, 5);
            tabPlants.Name = "tabPlants";
            tabPlants.Padding = new Padding(3);
            tabPlants.Size = new Size(1220, 718);
            tabPlants.TabIndex = 1;
            tabPlants.Text = "Plants";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 15F);
            label7.ForeColor = Color.WhiteSmoke;
            label7.Location = new Point(0, 2);
            label7.Margin = new Padding(5, 0, 5, 0);
            label7.Name = "label7";
            label7.Size = new Size(90, 28);
            label7.TabIndex = 13;
            label7.Text = "Plant List";
            // 
            // lvPlantList
            // 
            lvPlantList.BackColor = Color.FromArgb(0, 79, 111);
            lvPlantList.BorderStyle = BorderStyle.None;
            lvPlantList.ForeColor = SystemColors.Window;
            lvPlantList.FullRowSelect = true;
            lvPlantList.Location = new Point(0, 36);
            lvPlantList.Margin = new Padding(5, 6, 5, 6);
            lvPlantList.MultiSelect = false;
            lvPlantList.Name = "lvPlantList";
            lvPlantList.Size = new Size(296, 681);
            lvPlantList.TabIndex = 12;
            lvPlantList.UseCompatibleStateImageBehavior = false;
            lvPlantList.View = View.Details;
            lvPlantList.ItemSelectionChanged += lvPlantList_ItemSelectionChanged;
            // 
            // llRemovePlant
            // 
            llRemovePlant.AutoSize = true;
            llRemovePlant.ForeColor = Color.WhiteSmoke;
            llRemovePlant.LinkColor = Color.WhiteSmoke;
            llRemovePlant.Location = new Point(208, 3);
            llRemovePlant.Name = "llRemovePlant";
            llRemovePlant.Size = new Size(88, 28);
            llRemovePlant.TabIndex = 14;
            llRemovePlant.TabStop = true;
            llRemovePlant.Text = "Kill Plant";
            llRemovePlant.VisitedLinkColor = Color.Black;
            llRemovePlant.LinkClicked += llRemovePlant_LinkClicked;
            // 
            // gbNewPlant
            // 
            gbNewPlant.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbNewPlant.Controls.Add(btnCreatePlant);
            gbNewPlant.Controls.Add(button1);
            gbNewPlant.Controls.Add(label8);
            gbNewPlant.Controls.Add(flpNewPlantSettings);
            gbNewPlant.Controls.Add(label9);
            gbNewPlant.Controls.Add(cmbPlantType);
            gbNewPlant.ForeColor = Color.WhiteSmoke;
            gbNewPlant.Location = new Point(304, 2);
            gbNewPlant.Name = "gbNewPlant";
            gbNewPlant.Size = new Size(917, 344);
            gbNewPlant.TabIndex = 15;
            gbNewPlant.TabStop = false;
            gbNewPlant.Text = "Create New Plant";
            // 
            // btnCreatePlant
            // 
            btnCreatePlant.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCreatePlant.BackColor = Color.Teal;
            btnCreatePlant.FlatStyle = FlatStyle.Flat;
            btnCreatePlant.ForeColor = Color.White;
            btnCreatePlant.Location = new Point(6, 268);
            btnCreatePlant.Name = "btnCreatePlant";
            btnCreatePlant.Size = new Size(203, 70);
            btnCreatePlant.TabIndex = 13;
            btnCreatePlant.Text = "Create Plant";
            btnCreatePlant.UseVisualStyleBackColor = false;
            btnCreatePlant.Click += btnCreatePlant_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button1.BackColor = Color.Teal;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.White;
            button1.Location = new Point(6, 512);
            button1.Name = "button1";
            button1.Size = new Size(203, 70);
            button1.TabIndex = 4;
            button1.Text = "Create Fish";
            button1.UseVisualStyleBackColor = false;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(215, 30);
            label8.Name = "label8";
            label8.Size = new Size(176, 28);
            label8.TabIndex = 3;
            label8.Text = "New Plant Settings";
            // 
            // flpNewPlantSettings
            // 
            flpNewPlantSettings.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flpNewPlantSettings.FlowDirection = FlowDirection.TopDown;
            flpNewPlantSettings.Location = new Point(215, 61);
            flpNewPlantSettings.Name = "flpNewPlantSettings";
            flpNewPlantSettings.Size = new Size(695, 277);
            flpNewPlantSettings.TabIndex = 2;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(6, 30);
            label9.Name = "label9";
            label9.Size = new Size(102, 28);
            label9.TabIndex = 1;
            label9.Text = "Plant Type";
            // 
            // cmbPlantType
            // 
            cmbPlantType.BackColor = Color.Teal;
            cmbPlantType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPlantType.FlatStyle = FlatStyle.Flat;
            cmbPlantType.ForeColor = Color.White;
            cmbPlantType.FormattingEnabled = true;
            cmbPlantType.Location = new Point(6, 61);
            cmbPlantType.Name = "cmbPlantType";
            cmbPlantType.Size = new Size(203, 36);
            cmbPlantType.TabIndex = 0;
            cmbPlantType.SelectedIndexChanged += cmbPlantType_SelectedIndexChanged;
            cmbPlantType.Format += cmbPlantType_Format;
            // 
            // gbEditPlant
            // 
            gbEditPlant.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbEditPlant.Controls.Add(btnSavePlant);
            gbEditPlant.Controls.Add(button2);
            gbEditPlant.Controls.Add(label10);
            gbEditPlant.Controls.Add(flpSelectedPlantSettings);
            gbEditPlant.ForeColor = Color.White;
            gbEditPlant.Location = new Point(304, 352);
            gbEditPlant.Name = "gbEditPlant";
            gbEditPlant.Size = new Size(917, 365);
            gbEditPlant.TabIndex = 16;
            gbEditPlant.TabStop = false;
            gbEditPlant.Text = "Edit Selected Plant";
            // 
            // btnSavePlant
            // 
            btnSavePlant.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSavePlant.BackColor = Color.Teal;
            btnSavePlant.FlatStyle = FlatStyle.Flat;
            btnSavePlant.ForeColor = Color.White;
            btnSavePlant.Location = new Point(6, 289);
            btnSavePlant.Name = "btnSavePlant";
            btnSavePlant.Size = new Size(203, 70);
            btnSavePlant.TabIndex = 12;
            btnSavePlant.Text = "Save New Settings";
            btnSavePlant.UseVisualStyleBackColor = false;
            btnSavePlant.Click += btnSavePlant_Click;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button2.BackColor = Color.Teal;
            button2.FlatStyle = FlatStyle.Flat;
            button2.ForeColor = Color.White;
            button2.Location = new Point(723, 554);
            button2.Name = "button2";
            button2.Size = new Size(203, 70);
            button2.TabIndex = 4;
            button2.Text = "Save New Settings";
            button2.UseVisualStyleBackColor = false;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(215, 51);
            label10.Name = "label10";
            label10.Size = new Size(211, 28);
            label10.TabIndex = 3;
            label10.Text = "Selected Plant Settings";
            // 
            // flpSelectedPlantSettings
            // 
            flpSelectedPlantSettings.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flpSelectedPlantSettings.FlowDirection = FlowDirection.TopDown;
            flpSelectedPlantSettings.Location = new Point(215, 82);
            flpSelectedPlantSettings.Name = "flpSelectedPlantSettings";
            flpSelectedPlantSettings.Size = new Size(696, 277);
            flpSelectedPlantSettings.TabIndex = 2;
            // 
            // btnFishTab
            // 
            btnFishTab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnFishTab.BackColor = Color.FromArgb(0, 105, 148);
            btnFishTab.FlatStyle = FlatStyle.Flat;
            btnFishTab.ForeColor = Color.White;
            btnFishTab.Location = new Point(19, 70);
            btnFishTab.Name = "btnFishTab";
            btnFishTab.Size = new Size(125, 40);
            btnFishTab.TabIndex = 10;
            btnFishTab.Text = "Fish";
            btnFishTab.UseVisualStyleBackColor = false;
            btnFishTab.Click += btnFishTab_Click;
            // 
            // btnPlantTab
            // 
            btnPlantTab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnPlantTab.BackColor = Color.FromArgb(0, 79, 111);
            btnPlantTab.FlatStyle = FlatStyle.Flat;
            btnPlantTab.ForeColor = Color.White;
            btnPlantTab.Location = new Point(145, 73);
            btnPlantTab.Name = "btnPlantTab";
            btnPlantTab.Size = new Size(125, 40);
            btnPlantTab.TabIndex = 11;
            btnPlantTab.Text = "Plants";
            btnPlantTab.UseVisualStyleBackColor = false;
            btnPlantTab.Click += btnPlantTab_Click;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 105, 148);
            ClientSize = new Size(1259, 848);
            Controls.Add(tcMain);
            Controls.Add(llExit);
            Controls.Add(btnIdentifyFish);
            Controls.Add(llCredits);
            Controls.Add(btnPlantTab);
            Controls.Add(btnFishTab);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 15F);
            ForeColor = Color.Black;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 6, 5, 6);
            Name = "frmMain";
            Text = "Aquarium Manager";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            contextMenuStrip1.ResumeLayout(false);
            tcMain.ResumeLayout(false);
            tabFish.ResumeLayout(false);
            tabFish.PerformLayout();
            tabPlants.ResumeLayout(false);
            tabPlants.PerformLayout();
            gbNewPlant.ResumeLayout(false);
            gbNewPlant.PerformLayout();
            gbEditPlant.ResumeLayout(false);
            gbEditPlant.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView lvFishList;
        private Label label1;
        private Label label2;
        private LinkLabel llRemoveFish;
        private GroupBox groupBox1;
        private Label label3;
        private ComboBox cmbFishType;
        private Button btnCreateFish;
        private Label label4;
        private FlowLayoutPanel flpNewSettings;
        private Button btnSaveSettings;
        private GroupBox groupBox2;
        private Label label5;
        private FlowLayoutPanel flpSelectedSettings;
        private LinkLabel llCredits;
        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem btnManage;
        private ToolStripMenuItem btnQuit;
        private Button btnIdentifyFish;
        private LinkLabel llExit;
        private TabControl tcMain;
        private TabPage tabFish;
        private TabPage tabPlants;
        private Button btnFishTab;
        private Button btnPlantTab;
        private Label label7;
        private ListView lvPlantList;
        private LinkLabel llRemovePlant;
        private GroupBox gbNewPlant;
        private Button button1;
        private Label label8;
        private FlowLayoutPanel flpNewPlantSettings;
        private Label label9;
        private ComboBox cmbPlantType;
        private GroupBox gbEditPlant;
        private Button button2;
        private Label label10;
        private FlowLayoutPanel flpSelectedPlantSettings;
        private Button btnCreatePlant;
        private Button btnSavePlant;
    }
}
