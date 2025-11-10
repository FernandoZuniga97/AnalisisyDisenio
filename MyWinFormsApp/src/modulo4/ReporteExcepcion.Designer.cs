namespace MyWinFormsApp.src.modulo4
{
    partial class ReporteExcepcion
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
            panel2 = new System.Windows.Forms.Panel();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            panel1 = new System.Windows.Forms.Panel();
            lblFechaG = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            lblPeriodo = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            btnExportarPDF = new System.Windows.Forms.Button();
            label3 = new System.Windows.Forms.Label();
            dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            txtID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblDispostivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblDiasSinRetiro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblMontoPendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panel2.Controls.Add( dataGridView1 );
            panel2.Location = new System.Drawing.Point( 12, 150 );
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size( 704, 457 );
            panel2.TabIndex = 3;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange( new System.Windows.Forms.DataGridViewColumn[] { txtID, lblFecha, lblCliente, lblDispostivo, lblEstado, lblDiasSinRetiro, lblMontoPendiente } );
            dataGridView1.Location = new System.Drawing.Point( 3, 0 );
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new System.Drawing.Size( 701, 454 );
            dataGridView1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panel1.BackColor = System.Drawing.Color.FromArgb( 0, 32, 96 );
            panel1.Controls.Add( lblFechaG );
            panel1.Controls.Add( label5 );
            panel1.Controls.Add( label4 );
            panel1.Controls.Add( lblPeriodo );
            panel1.Controls.Add( label2 );
            panel1.Controls.Add( label1 );
            panel1.Controls.Add( pictureBox1 );
            panel1.Location = new System.Drawing.Point( 12, 12 );
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size( 704, 132 );
            panel1.TabIndex = 2;
            // 
            // lblFechaG
            // 
            lblFechaG.AutoSize = true;
            lblFechaG.ForeColor = System.Drawing.SystemColors.Control;
            lblFechaG.Location = new System.Drawing.Point( 404, 102 );
            lblFechaG.Name = "lblFechaG";
            lblFechaG.Size = new System.Drawing.Size( 23, 15 );
            lblFechaG.TabIndex = 6;
            lblFechaG.Text = "{}{}";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = System.Drawing.SystemColors.Control;
            label5.Location = new System.Drawing.Point( 308, 102 );
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size( 94, 15 );
            label5.TabIndex = 5;
            label5.Text = "Fecha generado:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font( "Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0 );
            label4.ForeColor = System.Drawing.SystemColors.Control;
            label4.Location = new System.Drawing.Point( 304, 45 );
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size( 219, 25 );
            label4.TabIndex = 4;
            label4.Text = "Dispositivos no retirados";
            // 
            // lblPeriodo
            // 
            lblPeriodo.AutoSize = true;
            lblPeriodo.ForeColor = System.Drawing.SystemColors.Control;
            lblPeriodo.Location = new System.Drawing.Point( 373, 76 );
            lblPeriodo.Name = "lblPeriodo";
            lblPeriodo.Size = new System.Drawing.Size( 23, 15 );
            lblPeriodo.TabIndex = 3;
            lblPeriodo.Text = "{}{}";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = System.Drawing.SystemColors.Control;
            label2.Location = new System.Drawing.Point( 322, 76 );
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size( 51, 15 );
            label2.TabIndex = 2;
            label2.Text = "Periodo:";
            // 
            // label1
            // 
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
            pictureBox1.Location = new System.Drawing.Point( 5, 14 );
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size( 103, 97 );
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // btnExportarPDF
            // 
            btnExportarPDF.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnExportarPDF.Location = new System.Drawing.Point( 722, 15 );
            btnExportarPDF.Name = "btnExportarPDF";
            btnExportarPDF.Size = new System.Drawing.Size( 75, 43 );
            btnExportarPDF.TabIndex = 8;
            btnExportarPDF.Text = "Imprimir PDF";
            btnExportarPDF.UseVisualStyleBackColor = true;
            btnExportarPDF.Click += btnExportarPDF_Click;
            // 
            // label3
            // 
            label3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point( 722, 159 );
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size( 46, 15 );
            label3.TabIndex = 10;
            label3.Text = "Fechas:";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            dateTimePicker1.Font = new System.Drawing.Font( "Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0 );
            dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new System.Drawing.Point( 722, 177 );
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new System.Drawing.Size( 85, 25 );
            dateTimePicker1.TabIndex = 9;
            dateTimePicker1.Value = new System.DateTime( 2025, 11, 10, 0, 0, 0, 0 );
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // txtID
            // 
            txtID.Frozen = true;
            txtID.HeaderText = "ID";
            txtID.Name = "txtID";
            txtID.ReadOnly = true;
            txtID.Width = 60;
            // 
            // lblFecha
            // 
            lblFecha.Frozen = true;
            lblFecha.HeaderText = "Fecha ingreso";
            lblFecha.Name = "lblFecha";
            lblFecha.ReadOnly = true;
            lblFecha.Width = 97;
            // 
            // lblCliente
            // 
            lblCliente.Frozen = true;
            lblCliente.HeaderText = "Cliente";
            lblCliente.Name = "lblCliente";
            lblCliente.ReadOnly = true;
            lblCliente.Width = 69;
            // 
            // lblDispostivo
            // 
            lblDispostivo.Frozen = true;
            lblDispostivo.HeaderText = "Dispositivo";
            lblDispostivo.Name = "lblDispostivo";
            lblDispostivo.ReadOnly = true;
            lblDispostivo.Width = 90;
            // 
            // lblEstado
            // 
            lblEstado.Frozen = true;
            lblEstado.HeaderText = "Estado de reparacion";
            lblEstado.Name = "lblEstado";
            lblEstado.ReadOnly = true;
            lblEstado.Width = 130;
            // 
            // lblDiasSinRetiro
            // 
            lblDiasSinRetiro.Frozen = true;
            lblDiasSinRetiro.HeaderText = "Días sin retirar";
            lblDiasSinRetiro.Name = "lblDiasSinRetiro";
            lblDiasSinRetiro.ReadOnly = true;
            lblDiasSinRetiro.Width = 97;
            // 
            // lblMontoPendiente
            // 
            lblMontoPendiente.Frozen = true;
            lblMontoPendiente.HeaderText = "Monto pendiente (L.)";
            lblMontoPendiente.Name = "lblMontoPendiente";
            lblMontoPendiente.ReadOnly = true;
            lblMontoPendiente.Width = 116;
            // 
            // ReporteExcepcion
            // 
            AutoScaleDimensions = new System.Drawing.SizeF( 7F, 15F );
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size( 821, 619 );
            Controls.Add( label3 );
            Controls.Add( dateTimePicker1 );
            Controls.Add( btnExportarPDF );
            Controls.Add( panel2 );
            Controls.Add( panel1 );
            Name = "ReporteExcepcion";
            Text = "ReporteExcepcion";
            Load += ReporteExcepcion_Load;
            panel2.ResumeLayout( false );
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout( false );
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout( false );
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblPeriodo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnExportarPDF;
        private System.Windows.Forms.Label lblFechaG;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtID;
        private System.Windows.Forms.DataGridViewTextBoxColumn lblFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn lblCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn lblDispostivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn lblEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn lblDiasSinRetiro;
        private System.Windows.Forms.DataGridViewTextBoxColumn lblMontoPendiente;
    }
}