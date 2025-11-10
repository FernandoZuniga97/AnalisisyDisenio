namespace MyWinFormsApp.src.modulo4
{
    partial class ReporteMantenimiento
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && (components != null) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new System.Windows.Forms.Panel();
            label4 = new System.Windows.Forms.Label();
            lblFechas = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            btnExportarPDF = new System.Windows.Forms.Button();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            txtID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblDispostivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblTenico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblMateriales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblCosto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblObservaciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
            panel2 = new System.Windows.Forms.Panel();
            dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            label5 = new System.Windows.Forms.Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panel1.BackColor = System.Drawing.Color.FromArgb( 0, 32, 96 );
            panel1.Controls.Add( label4 );
            panel1.Controls.Add( lblFechas );
            panel1.Controls.Add( label2 );
            panel1.Controls.Add( label1 );
            panel1.Controls.Add( pictureBox1 );
            panel1.Location = new System.Drawing.Point( 12, 12 );
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size( 744, 100 );
            panel1.TabIndex = 0;
            // 
            // label4
            // 
            label4.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font( "Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0 );
            label4.ForeColor = System.Drawing.SystemColors.Control;
            label4.Location = new System.Drawing.Point( 292, 45 );
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size( 237, 25 );
            label4.TabIndex = 4;
            label4.Text = "Reporte de Mantenimiento";
            // 
            // lblFechas
            // 
            lblFechas.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblFechas.AutoSize = true;
            lblFechas.ForeColor = System.Drawing.SystemColors.Control;
            lblFechas.Location = new System.Drawing.Point( 367, 76 );
            lblFechas.Name = "lblFechas";
            lblFechas.Size = new System.Drawing.Size( 23, 15 );
            lblFechas.TabIndex = 3;
            lblFechas.Text = "{}{}";
            // 
            // label2
            // 
            label2.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label2.AutoSize = true;
            label2.ForeColor = System.Drawing.SystemColors.Control;
            label2.Location = new System.Drawing.Point( 315, 76 );
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size( 51, 15 );
            label2.TabIndex = 2;
            label2.Text = "Periodo:";
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font( "Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0 );
            label1.ForeColor = System.Drawing.SystemColors.Control;
            label1.Location = new System.Drawing.Point( 268, 3 );
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size( 293, 32 );
            label1.TabIndex = 1;
            label1.Text = "IMPORTACIONES GICELL";
            // 
            // pictureBox1
            // 
            pictureBox1.ImageLocation = "C:\\Users\\Anthony\\Source\\Repos\\AnalisisyDisenio\\MyWinFormsApp\\src\\login\\Image\\logo_g.jpg";
            pictureBox1.Location = new System.Drawing.Point( 3, 3 );
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size( 103, 97 );
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // btnExportarPDF
            // 
            btnExportarPDF.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnExportarPDF.Location = new System.Drawing.Point( 778, 16 );
            btnExportarPDF.Name = "btnExportarPDF";
            btnExportarPDF.Size = new System.Drawing.Size( 75, 43 );
            btnExportarPDF.TabIndex = 5;
            btnExportarPDF.Text = "Imprimir PDF";
            btnExportarPDF.UseVisualStyleBackColor = true;
            btnExportarPDF.Click += btnExportarPDF_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange( new System.Windows.Forms.DataGridViewColumn[] { txtID, lblFecha, lblDispostivo, lblTipo, lblDescripcion, lblTenico, lblMateriales, lblCosto, lblObservaciones } );
            dataGridView1.Location = new System.Drawing.Point( 0, 0 );
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new System.Drawing.Size( 746, 317 );
            dataGridView1.TabIndex = 0;
            // 
            // txtID
            // 
            txtID.Frozen = true;
            txtID.HeaderText = "ID";
            txtID.Name = "txtID";
            txtID.ReadOnly = true;
            txtID.Width = 43;
            // 
            // lblFecha
            // 
            lblFecha.Frozen = true;
            lblFecha.HeaderText = "Fecha";
            lblFecha.Name = "lblFecha";
            lblFecha.ReadOnly = true;
            lblFecha.Width = 63;
            // 
            // lblDispostivo
            // 
            lblDispostivo.Frozen = true;
            lblDispostivo.HeaderText = "Dispositivo";
            lblDispostivo.Name = "lblDispostivo";
            lblDispostivo.ReadOnly = true;
            lblDispostivo.Width = 110;
            // 
            // lblTipo
            // 
            lblTipo.Frozen = true;
            lblTipo.HeaderText = "Tipo de Mantenimiento";
            lblTipo.Name = "lblTipo";
            lblTipo.ReadOnly = true;
            lblTipo.Width = 143;
            // 
            // lblDescripcion
            // 
            lblDescripcion.Frozen = true;
            lblDescripcion.HeaderText = "Descripcion de la actividad";
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.ReadOnly = true;
            lblDescripcion.Width = 115;
            // 
            // lblTenico
            // 
            lblTenico.Frozen = true;
            lblTenico.HeaderText = "Tecnico responsable";
            lblTenico.Name = "lblTenico";
            lblTenico.ReadOnly = true;
            lblTenico.Width = 127;
            // 
            // lblMateriales
            // 
            lblMateriales.Frozen = true;
            lblMateriales.HeaderText = "Materiales usados";
            lblMateriales.Name = "lblMateriales";
            lblMateriales.ReadOnly = true;
            lblMateriales.Width = 115;
            // 
            // lblCosto
            // 
            lblCosto.Frozen = true;
            lblCosto.HeaderText = "Costo estimado (L.)";
            lblCosto.Name = "lblCosto";
            lblCosto.ReadOnly = true;
            lblCosto.Width = 108;
            // 
            // lblObservaciones
            // 
            lblObservaciones.Frozen = true;
            lblObservaciones.HeaderText = "Observaciones";
            lblObservaciones.Name = "lblObservaciones";
            lblObservaciones.ReadOnly = true;
            lblObservaciones.Width = 109;
            // 
            // panel2
            // 
            panel2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panel2.Controls.Add( dataGridView1 );
            panel2.Location = new System.Drawing.Point( 12, 118 );
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size( 762, 320 );
            panel2.TabIndex = 1;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            dateTimePicker1.Font = new System.Drawing.Font( "Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0 );
            dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new System.Drawing.Point( 780, 139 );
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new System.Drawing.Size( 85, 25 );
            dateTimePicker1.TabIndex = 6;
            dateTimePicker1.Value = new System.DateTime( 2025, 11, 10, 0, 0, 0, 0 );
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // label5
            // 
            label5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point( 780, 121 );
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size( 46, 15 );
            label5.TabIndex = 7;
            label5.Text = "Fechas:";
            // 
            // ReporteMantenimiento
            // 
            AutoScaleDimensions = new System.Drawing.SizeF( 7F, 15F );
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new System.Drawing.Size( 878, 450 );
            Controls.Add( label5 );
            Controls.Add( dateTimePicker1 );
            Controls.Add( btnExportarPDF );
            Controls.Add( panel2 );
            Controls.Add( panel1 );
            Name = "ReporteMantenimiento";
            Text = "ReporteMantenimiento";
            Load += ReporteMantenimiento_Load;
            panel1.ResumeLayout( false );
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel2.ResumeLayout( false );
            ResumeLayout( false );
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblFechas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnExportarPDF;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtID;
        private System.Windows.Forms.DataGridViewTextBoxColumn lblFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn lblDispostivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn lblTipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn lblDescripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn lblTenico;
        private System.Windows.Forms.DataGridViewTextBoxColumn lblMateriales;
        private System.Windows.Forms.DataGridViewTextBoxColumn lblCosto;
        private System.Windows.Forms.DataGridViewTextBoxColumn lblObservaciones;
    }
}