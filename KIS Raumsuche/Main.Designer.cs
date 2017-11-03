namespace TUK_Raumsuche
{
    partial class Main
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.dtp_date = new System.Windows.Forms.DateTimePicker();
            this.dtp_startTime = new System.Windows.Forms.DateTimePicker();
            this.dtp_endTime = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_search = new System.Windows.Forms.Button();
            this.dgv_results = new System.Windows.Forms.DataGridView();
            this.pb_search = new System.Windows.Forms.ProgressBar();
            this.lbl_status = new System.Windows.Forms.Label();
            this.timer_status = new System.Windows.Forms.Timer(this.components);
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.room_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.room_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.places = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clb_roomGroups = new System.Windows.Forms.CheckedListBox();
            this.btn_setup = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_results)).BeginInit();
            this.SuspendLayout();
            // 
            // dtp_date
            // 
            this.dtp_date.Location = new System.Drawing.Point(12, 25);
            this.dtp_date.Name = "dtp_date";
            this.dtp_date.Size = new System.Drawing.Size(212, 20);
            this.dtp_date.TabIndex = 0;
            // 
            // dtp_startTime
            // 
            this.dtp_startTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtp_startTime.Location = new System.Drawing.Point(12, 64);
            this.dtp_startTime.Name = "dtp_startTime";
            this.dtp_startTime.ShowUpDown = true;
            this.dtp_startTime.Size = new System.Drawing.Size(103, 20);
            this.dtp_startTime.TabIndex = 1;
            // 
            // dtp_endTime
            // 
            this.dtp_endTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtp_endTime.Location = new System.Drawing.Point(121, 64);
            this.dtp_endTime.Name = "dtp_endTime";
            this.dtp_endTime.ShowUpDown = true;
            this.dtp_endTime.Size = new System.Drawing.Size(103, 20);
            this.dtp_endTime.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Datum";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Von";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(118, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Bis";
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(12, 90);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(103, 23);
            this.btn_search.TabIndex = 6;
            this.btn_search.Text = "Suchen";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // dgv_results
            // 
            this.dgv_results.AllowUserToAddRows = false;
            this.dgv_results.AllowUserToDeleteRows = false;
            this.dgv_results.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_results.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_results.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.room_id,
            this.room_name,
            this.places});
            this.dgv_results.Location = new System.Drawing.Point(12, 148);
            this.dgv_results.Name = "dgv_results";
            this.dgv_results.ReadOnly = true;
            this.dgv_results.Size = new System.Drawing.Size(619, 486);
            this.dgv_results.TabIndex = 7;
            this.dgv_results.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_results_CellContentClick);
            // 
            // pb_search
            // 
            this.pb_search.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pb_search.Location = new System.Drawing.Point(12, 119);
            this.pb_search.Name = "pb_search";
            this.pb_search.Size = new System.Drawing.Size(619, 23);
            this.pb_search.Step = 1;
            this.pb_search.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pb_search.TabIndex = 8;
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.Location = new System.Drawing.Point(121, 95);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(24, 13);
            this.lbl_status.TabIndex = 9;
            this.lbl_status.Text = "Idle";
            // 
            // timer_status
            // 
            this.timer_status.Enabled = true;
            this.timer_status.Tick += new System.EventHandler(this.timer_status_Tick);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(596, 9);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(35, 13);
            this.linkLabel1.TabIndex = 10;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "About";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // room_id
            // 
            this.room_id.HeaderText = "ID";
            this.room_id.Name = "room_id";
            this.room_id.ReadOnly = true;
            this.room_id.Visible = false;
            // 
            // room_name
            // 
            this.room_name.HeaderText = "Name";
            this.room_name.Name = "room_name";
            this.room_name.ReadOnly = true;
            // 
            // places
            // 
            this.places.HeaderText = "Sitzplätze";
            this.places.Name = "places";
            this.places.ReadOnly = true;
            // 
            // clb_roomGroups
            // 
            this.clb_roomGroups.FormattingEnabled = true;
            this.clb_roomGroups.Location = new System.Drawing.Point(230, 20);
            this.clb_roomGroups.Name = "clb_roomGroups";
            this.clb_roomGroups.Size = new System.Drawing.Size(288, 64);
            this.clb_roomGroups.TabIndex = 11;
            // 
            // btn_setup
            // 
            this.btn_setup.Location = new System.Drawing.Point(230, 90);
            this.btn_setup.Name = "btn_setup";
            this.btn_setup.Size = new System.Drawing.Size(120, 23);
            this.btn_setup.TabIndex = 12;
            this.btn_setup.Text = "Setup";
            this.btn_setup.UseVisualStyleBackColor = true;
            this.btn_setup.Click += new System.EventHandler(this.btn_setup_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 646);
            this.Controls.Add(this.btn_setup);
            this.Controls.Add(this.clb_roomGroups);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.lbl_status);
            this.Controls.Add(this.pb_search);
            this.Controls.Add(this.dgv_results);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtp_endTime);
            this.Controls.Add(this.dtp_startTime);
            this.Controls.Add(this.dtp_date);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "TUK Raumsuche";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_results)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtp_date;
        private System.Windows.Forms.DateTimePicker dtp_startTime;
        private System.Windows.Forms.DateTimePicker dtp_endTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.DataGridView dgv_results;
        private System.Windows.Forms.ProgressBar pb_search;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.Timer timer_status;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn room_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn room_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn places;
        private System.Windows.Forms.CheckedListBox clb_roomGroups;
        private System.Windows.Forms.Button btn_setup;
    }
}

