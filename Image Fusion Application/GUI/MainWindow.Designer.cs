namespace Image_Fusion_Application
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.VisibleImage = new System.Windows.Forms.PictureBox();
            this.ThermalImage = new System.Windows.Forms.PictureBox();
            this.FusedImage = new System.Windows.Forms.PictureBox();
            this.openIRB = new System.Windows.Forms.Label();
            this.filePath = new System.Windows.Forms.TextBox();
            this.loadIRB = new System.Windows.Forms.Button();
            this.visbibleImageLabel = new System.Windows.Forms.Label();
            this.thermalImageLabel = new System.Windows.Forms.Label();
            this.fuseImages = new System.Windows.Forms.Button();
            this.thermalType = new System.Windows.Forms.ComboBox();
            this.getFilePath = new System.Windows.Forms.Button();
            this.repaintThermalImage = new System.Windows.Forms.Button();
            this.processOngoing = new System.Windows.Forms.Label();
            this.maskImage = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fusionMethods = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.thresholdScroll = new System.Windows.Forms.HScrollBar();
            this.ratioScroll = new System.Windows.Forms.HScrollBar();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.thresholdText = new System.Windows.Forms.TextBox();
            this.ratioText = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.visibleFolderButton = new System.Windows.Forms.Button();
            this.thermalFolderButton = new System.Windows.Forms.Button();
            this.fusedFolderButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.VisibleImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThermalImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FusedImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maskImage)).BeginInit();
            this.SuspendLayout();
            // 
            // VisibleImage
            // 
            this.VisibleImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VisibleImage.Location = new System.Drawing.Point(21, 126);
            this.VisibleImage.Name = "VisibleImage";
            this.VisibleImage.Size = new System.Drawing.Size(480, 320);
            this.VisibleImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.VisibleImage.TabIndex = 0;
            this.VisibleImage.TabStop = false;
            this.VisibleImage.Click += new System.EventHandler(this.VisibleImage_Click);
            // 
            // ThermalImage
            // 
            this.ThermalImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ThermalImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ThermalImage.Location = new System.Drawing.Point(877, 126);
            this.ThermalImage.Name = "ThermalImage";
            this.ThermalImage.Size = new System.Drawing.Size(480, 320);
            this.ThermalImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ThermalImage.TabIndex = 1;
            this.ThermalImage.TabStop = false;
            this.ThermalImage.Click += new System.EventHandler(this.ThermalImage_Click);
            // 
            // FusedImage
            // 
            this.FusedImage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.FusedImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FusedImage.Location = new System.Drawing.Point(437, 529);
            this.FusedImage.Name = "FusedImage";
            this.FusedImage.Size = new System.Drawing.Size(480, 320);
            this.FusedImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.FusedImage.TabIndex = 2;
            this.FusedImage.TabStop = false;
            this.FusedImage.Click += new System.EventHandler(this.FusedImage_Click);
            // 
            // openIRB
            // 
            this.openIRB.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.openIRB.AutoSize = true;
            this.openIRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openIRB.Location = new System.Drawing.Point(16, 35);
            this.openIRB.Name = "openIRB";
            this.openIRB.Size = new System.Drawing.Size(165, 25);
            this.openIRB.TabIndex = 3;
            this.openIRB.Text = "Open IRB File:";
            // 
            // filePath
            // 
            this.filePath.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.filePath.Location = new System.Drawing.Point(187, 35);
            this.filePath.Multiline = true;
            this.filePath.Name = "filePath";
            this.filePath.Size = new System.Drawing.Size(614, 25);
            this.filePath.TabIndex = 4;
            // 
            // loadIRB
            // 
            this.loadIRB.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.loadIRB.Location = new System.Drawing.Point(868, 35);
            this.loadIRB.Name = "loadIRB";
            this.loadIRB.Size = new System.Drawing.Size(119, 25);
            this.loadIRB.TabIndex = 5;
            this.loadIRB.Text = "Load IRB";
            this.loadIRB.UseVisualStyleBackColor = true;
            this.loadIRB.Click += new System.EventHandler(this.loadIRB_Click);
            // 
            // visbibleImageLabel
            // 
            this.visbibleImageLabel.AutoSize = true;
            this.visbibleImageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.visbibleImageLabel.Location = new System.Drawing.Point(182, 98);
            this.visbibleImageLabel.Name = "visbibleImageLabel";
            this.visbibleImageLabel.Size = new System.Drawing.Size(153, 25);
            this.visbibleImageLabel.TabIndex = 6;
            this.visbibleImageLabel.Text = "Visible Image";
            // 
            // thermalImageLabel
            // 
            this.thermalImageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.thermalImageLabel.AutoSize = true;
            this.thermalImageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.thermalImageLabel.Location = new System.Drawing.Point(936, 98);
            this.thermalImageLabel.Name = "thermalImageLabel";
            this.thermalImageLabel.Size = new System.Drawing.Size(167, 25);
            this.thermalImageLabel.TabIndex = 7;
            this.thermalImageLabel.Text = "Thermal Image";
            // 
            // fuseImages
            // 
            this.fuseImages.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.fuseImages.Location = new System.Drawing.Point(619, 498);
            this.fuseImages.Name = "fuseImages";
            this.fuseImages.Size = new System.Drawing.Size(119, 25);
            this.fuseImages.TabIndex = 8;
            this.fuseImages.Text = "Fuse Images";
            this.fuseImages.UseVisualStyleBackColor = true;
            this.fuseImages.Click += new System.EventHandler(this.fuseImages_Click);
            // 
            // thermalType
            // 
            this.thermalType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.thermalType.FormattingEnabled = true;
            this.thermalType.Items.AddRange(new object[] {
            "Rainbow",
            "RainbowSeven",
            "BlueToRed",
            "Fire",
            "Grayscale"});
            this.thermalType.Location = new System.Drawing.Point(1109, 102);
            this.thermalType.Name = "thermalType";
            this.thermalType.Size = new System.Drawing.Size(121, 21);
            this.thermalType.TabIndex = 9;
            // 
            // getFilePath
            // 
            this.getFilePath.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.getFilePath.Location = new System.Drawing.Point(807, 35);
            this.getFilePath.Name = "getFilePath";
            this.getFilePath.Size = new System.Drawing.Size(38, 25);
            this.getFilePath.TabIndex = 10;
            this.getFilePath.Text = "...";
            this.getFilePath.UseVisualStyleBackColor = true;
            this.getFilePath.Click += new System.EventHandler(this.getFilePath_Click);
            // 
            // repaintThermalImage
            // 
            this.repaintThermalImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.repaintThermalImage.Location = new System.Drawing.Point(1238, 102);
            this.repaintThermalImage.Name = "repaintThermalImage";
            this.repaintThermalImage.Size = new System.Drawing.Size(119, 21);
            this.repaintThermalImage.TabIndex = 11;
            this.repaintThermalImage.Text = "Repaint";
            this.repaintThermalImage.UseVisualStyleBackColor = true;
            this.repaintThermalImage.Click += new System.EventHandler(this.repaintThermalImage_Click);
            // 
            // processOngoing
            // 
            this.processOngoing.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.processOngoing.AutoSize = true;
            this.processOngoing.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.processOngoing.Location = new System.Drawing.Point(757, 498);
            this.processOngoing.Name = "processOngoing";
            this.processOngoing.Size = new System.Drawing.Size(0, 25);
            this.processOngoing.TabIndex = 13;
            // 
            // maskImage
            // 
            this.maskImage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.maskImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maskImage.Location = new System.Drawing.Point(437, 529);
            this.maskImage.Name = "maskImage";
            this.maskImage.Size = new System.Drawing.Size(480, 320);
            this.maskImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.maskImage.TabIndex = 14;
            this.maskImage.TabStop = false;
            this.maskImage.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 529);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 25);
            this.label1.TabIndex = 15;
            this.label1.Text = "Fusion methods";
            // 
            // fusionMethods
            // 
            this.fusionMethods.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.fusionMethods.BackColor = System.Drawing.SystemColors.Menu;
            this.fusionMethods.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fusionMethods.CheckOnClick = true;
            this.fusionMethods.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fusionMethods.FormattingEnabled = true;
            this.fusionMethods.Items.AddRange(new object[] {
            "Replace",
            "Saliency"});
            this.fusionMethods.Location = new System.Drawing.Point(21, 573);
            this.fusionMethods.Name = "fusionMethods";
            this.fusionMethods.Size = new System.Drawing.Size(120, 42);
            this.fusionMethods.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(970, 529);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 25);
            this.label2.TabIndex = 17;
            this.label2.Text = "Fusion settings";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(971, 573);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(236, 20);
            this.label3.TabIndex = 18;
            this.label3.Text = "Threshold (Replace method)";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(971, 653);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(201, 20);
            this.label4.TabIndex = 19;
            this.label4.Text = "Ratio (Saliency method)";
            // 
            // thresholdScroll
            // 
            this.thresholdScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.thresholdScroll.Location = new System.Drawing.Point(975, 614);
            this.thresholdScroll.Maximum = 109;
            this.thresholdScroll.Name = "thresholdScroll";
            this.thresholdScroll.Size = new System.Drawing.Size(255, 20);
            this.thresholdScroll.TabIndex = 20;
            this.thresholdScroll.Value = 10;
            this.thresholdScroll.ValueChanged += new System.EventHandler(this.thresholdScroll_ValueChanged);
            // 
            // ratioScroll
            // 
            this.ratioScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ratioScroll.Location = new System.Drawing.Point(990, 697);
            this.ratioScroll.Maximum = 109;
            this.ratioScroll.Name = "ratioScroll";
            this.ratioScroll.Size = new System.Drawing.Size(259, 22);
            this.ratioScroll.TabIndex = 21;
            this.ratioScroll.Value = 50;
            this.ratioScroll.ValueChanged += new System.EventHandler(this.ratioScroll_ValueChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(938, 697);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 16);
            this.label5.TabIndex = 22;
            this.label5.Text = "Visible";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1261, 697);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 16);
            this.label6.TabIndex = 23;
            this.label6.Text = "Thermal";
            // 
            // thresholdText
            // 
            this.thresholdText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.thresholdText.Location = new System.Drawing.Point(1340, 614);
            this.thresholdText.Name = "thresholdText";
            this.thresholdText.Size = new System.Drawing.Size(72, 20);
            this.thresholdText.TabIndex = 24;
            // 
            // ratioText
            // 
            this.ratioText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ratioText.Location = new System.Drawing.Point(1340, 697);
            this.ratioText.Name = "ratioText";
            this.ratioText.Size = new System.Drawing.Size(72, 20);
            this.ratioText.TabIndex = 25;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // visibleFolderButton
            // 
            this.visibleFolderButton.Image = global::Image_Fusion_Application.Properties.Resources.folder_icon1;
            this.visibleFolderButton.Location = new System.Drawing.Point(21, 84);
            this.visibleFolderButton.Name = "visibleFolderButton";
            this.visibleFolderButton.Size = new System.Drawing.Size(50, 39);
            this.visibleFolderButton.TabIndex = 26;
            this.visibleFolderButton.UseVisualStyleBackColor = true;
            this.visibleFolderButton.Click += new System.EventHandler(this.visibleFolderButton_Click);
            // 
            // thermalFolderButton
            // 
            this.thermalFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.thermalFolderButton.Image = global::Image_Fusion_Application.Properties.Resources.folder_icon1;
            this.thermalFolderButton.Location = new System.Drawing.Point(877, 84);
            this.thermalFolderButton.Name = "thermalFolderButton";
            this.thermalFolderButton.Size = new System.Drawing.Size(50, 39);
            this.thermalFolderButton.TabIndex = 27;
            this.thermalFolderButton.UseVisualStyleBackColor = true;
            this.thermalFolderButton.Click += new System.EventHandler(this.thermalFolderButton_Click);
            // 
            // fusedFolderButton
            // 
            this.fusedFolderButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.fusedFolderButton.Image = global::Image_Fusion_Application.Properties.Resources.folder_icon1;
            this.fusedFolderButton.Location = new System.Drawing.Point(437, 484);
            this.fusedFolderButton.Name = "fusedFolderButton";
            this.fusedFolderButton.Size = new System.Drawing.Size(50, 39);
            this.fusedFolderButton.TabIndex = 28;
            this.fusedFolderButton.UseVisualStyleBackColor = true;
            this.fusedFolderButton.Click += new System.EventHandler(this.fusedFolderButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(1400, 850);
            this.ClientSize = new System.Drawing.Size(1424, 873);
            this.Controls.Add(this.fusedFolderButton);
            this.Controls.Add(this.thermalFolderButton);
            this.Controls.Add(this.visibleFolderButton);
            this.Controls.Add(this.ratioText);
            this.Controls.Add(this.thresholdText);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ratioScroll);
            this.Controls.Add(this.thresholdScroll);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.fusionMethods);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.maskImage);
            this.Controls.Add(this.processOngoing);
            this.Controls.Add(this.repaintThermalImage);
            this.Controls.Add(this.getFilePath);
            this.Controls.Add(this.thermalType);
            this.Controls.Add(this.fuseImages);
            this.Controls.Add(this.thermalImageLabel);
            this.Controls.Add(this.visbibleImageLabel);
            this.Controls.Add(this.loadIRB);
            this.Controls.Add(this.filePath);
            this.Controls.Add(this.openIRB);
            this.Controls.Add(this.FusedImage);
            this.Controls.Add(this.ThermalImage);
            this.Controls.Add(this.VisibleImage);
            this.Name = "MainWindow";
            this.Text = "Image Fusion Application";
            ((System.ComponentModel.ISupportInitialize)(this.VisibleImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThermalImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FusedImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maskImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox VisibleImage;
        private System.Windows.Forms.PictureBox ThermalImage;
        private System.Windows.Forms.PictureBox FusedImage;
        private System.Windows.Forms.Label openIRB;
        private System.Windows.Forms.TextBox filePath;
        private System.Windows.Forms.Button loadIRB;
        private System.Windows.Forms.Label visbibleImageLabel;
        private System.Windows.Forms.Label thermalImageLabel;
        private System.Windows.Forms.Button fuseImages;
        private System.Windows.Forms.ComboBox thermalType;
        private System.Windows.Forms.Button getFilePath;
        private System.Windows.Forms.Button repaintThermalImage;
        private System.Windows.Forms.Label processOngoing;
        private System.Windows.Forms.PictureBox maskImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox fusionMethods;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.HScrollBar thresholdScroll;
        private System.Windows.Forms.HScrollBar ratioScroll;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox thresholdText;
        private System.Windows.Forms.TextBox ratioText;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button visibleFolderButton;
        private System.Windows.Forms.Button thermalFolderButton;
        private System.Windows.Forms.Button fusedFolderButton;
    }
}