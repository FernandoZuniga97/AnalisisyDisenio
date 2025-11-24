using System;
using System.Drawing;
using System.IO;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            panel2 = new System.Windows.Forms.Panel();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            txtID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblDispostivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblDiasSinRetiro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblMontoPendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            btnAgregar = new System.Windows.Forms.Button();
            btnEditar = new System.Windows.Forms.Button();
            btnEliminar = new System.Windows.Forms.Button();
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
            panel2.Location = new Point( 12, 188 );
            panel2.Name = "panel2";
            panel2.Size = new Size( 947, 427 );
            panel2.TabIndex = 3;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb( 224, 224, 224 );
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb( 0, 32, 96 );
            dataGridViewCellStyle2.Font = new Font( "Segoe UI", 9F, FontStyle.Bold );
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding( 5 );
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb( 0, 32, 96 );
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange( new System.Windows.Forms.DataGridViewColumn[] { txtID, lblFecha, lblCliente, lblDispostivo, lblEstado, lblDiasSinRetiro, lblMontoPendiente } );
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.Location = new Point( 3, 2 );
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowTemplate.ReadOnly = true;
            dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size( 944, 421 );
            dataGridView1.TabIndex = 0;
            // 
            // txtID
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            txtID.DefaultCellStyle = dataGridViewCellStyle3;
            txtID.Frozen = true;
            txtID.HeaderText = "ID";
            txtID.Name = "txtID";
            txtID.ReadOnly = true;
            txtID.Width = 75;
            // 
            // lblFecha
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            lblFecha.DefaultCellStyle = dataGridViewCellStyle4;
            lblFecha.Frozen = true;
            lblFecha.HeaderText = "Fecha ingreso";
            lblFecha.Name = "lblFecha";
            lblFecha.ReadOnly = true;
            lblFecha.Width = 110;
            // 
            // lblCliente
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            lblCliente.DefaultCellStyle = dataGridViewCellStyle5;
            lblCliente.Frozen = true;
            lblCliente.HeaderText = "Cliente";
            lblCliente.Name = "lblCliente";
            lblCliente.ReadOnly = true;
            lblCliente.Width = 130;
            // 
            // lblDispostivo
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            lblDispostivo.DefaultCellStyle = dataGridViewCellStyle6;
            lblDispostivo.Frozen = true;
            lblDispostivo.HeaderText = "Dispositivo";
            lblDispostivo.Name = "lblDispostivo";
            lblDispostivo.ReadOnly = true;
            lblDispostivo.Width = 140;
            // 
            // lblEstado
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            lblEstado.DefaultCellStyle = dataGridViewCellStyle7;
            lblEstado.Frozen = true;
            lblEstado.HeaderText = "Estado de reparacion";
            lblEstado.Name = "lblEstado";
            lblEstado.ReadOnly = true;
            lblEstado.Width = 130;
            // 
            // lblDiasSinRetiro
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            lblDiasSinRetiro.DefaultCellStyle = dataGridViewCellStyle8;
            lblDiasSinRetiro.Frozen = true;
            lblDiasSinRetiro.HeaderText = "Días sin retirar";
            lblDiasSinRetiro.Name = "lblDiasSinRetiro";
            lblDiasSinRetiro.ReadOnly = true;
            // 
            // lblMontoPendiente
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            lblMontoPendiente.DefaultCellStyle = dataGridViewCellStyle9;
            lblMontoPendiente.Frozen = true;
            lblMontoPendiente.HeaderText = "Monto pendiente (L.)";
            lblMontoPendiente.Name = "lblMontoPendiente";
            lblMontoPendiente.ReadOnly = true;
            lblMontoPendiente.Width = 130;
            // 
            // panel1
            // 
            panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panel1.BackColor = Color.FromArgb( 0, 32, 96 );
            panel1.Controls.Add( lblFechaG );
            panel1.Controls.Add( label5 );
            panel1.Controls.Add( label4 );
            panel1.Controls.Add( lblPeriodo );
            panel1.Controls.Add( label2 );
            panel1.Controls.Add( label1 );
            panel1.Controls.Add( pictureBox1 );
            panel1.Location = new Point( 12, 12 );
            panel1.Name = "panel1";
            panel1.Size = new Size( 947, 132 );
            panel1.TabIndex = 2;
            // 
            // lblFechaG
            // 
            lblFechaG.AutoSize = true;
            lblFechaG.Font = new Font( "Segoe UI", 11.25F );
            lblFechaG.ForeColor = SystemColors.Control;
            lblFechaG.Location = new Point( 550, 104 );
            lblFechaG.Name = "lblFechaG";
            lblFechaG.Size = new Size( 29, 20 );
            lblFechaG.TabIndex = 6;
            lblFechaG.Text = "{}{}";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font( "Segoe UI", 11.25F );
            label5.ForeColor = SystemColors.Control;
            label5.Location = new Point( 435, 104 );
            label5.Name = "label5";
            label5.Size = new Size( 118, 20 );
            label5.TabIndex = 5;
            label5.Text = "Fecha generado:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font( "Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0 );
            label4.ForeColor = SystemColors.Control;
            label4.Location = new Point( 458, 45 );
            label4.Name = "label4";
            label4.Size = new Size( 219, 25 );
            label4.TabIndex = 4;
            label4.Text = "Dispositivos no retirados";
            // 
            // lblPeriodo
            // 
            lblPeriodo.AutoSize = true;
            lblPeriodo.Font = new Font( "Segoe UI", 11.25F );
            lblPeriodo.ForeColor = SystemColors.Control;
            lblPeriodo.Location = new Point( 506, 78 );
            lblPeriodo.Name = "lblPeriodo";
            lblPeriodo.Size = new Size( 29, 20 );
            lblPeriodo.TabIndex = 3;
            lblPeriodo.Text = "{}{}";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font( "Segoe UI", 11.25F );
            label2.ForeColor = SystemColors.Control;
            label2.Location = new Point( 447, 78 );
            label2.Name = "label2";
            label2.Size = new Size( 63, 20 );
            label2.TabIndex = 2;
            label2.Text = "Periodo:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font( "Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0 );
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point( 424, 5 );
            label1.Name = "label1";
            label1.Size = new Size( 293, 32 );
            label1.TabIndex = 1;
            label1.Text = "IMPORTACIONES GICELL";
            // 
            // pictureBox1
            // 
            pictureBox1.ImageLocation = "../../../src/login/Image/logo_g.jpg";
            pictureBox1.Location = new Point( 6, 10 );
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size( 110, 110 );
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // btnExportarPDF
            // 
            btnExportarPDF.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnExportarPDF.Font = new Font( "Segoe UI Semibold", 9.75F, FontStyle.Bold );
            btnExportarPDF.Location = new Point( 854, 147 );
            btnExportarPDF.Name = "btnExportarPDF";
            btnExportarPDF.Size = new Size( 104, 38 );
            btnExportarPDF.TabIndex = 8;
            btnExportarPDF.Text = "Imprimir PDF";
            btnExportarPDF.UseVisualStyleBackColor = true;
            btnExportarPDF.Click += btnExportarPDF_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font( "Segoe UI Semibold", 9.75F, FontStyle.Bold );
            label3.Location = new Point( 17, 158 );
            label3.Name = "label3";
            label3.Size = new Size( 52, 17 );
            label3.TabIndex = 10;
            label3.Text = "Fechas:";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CustomFormat = "MMM yyyy";
            dateTimePicker1.Font = new Font( "Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0 );
            dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new Point( 69, 155 );
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size( 97, 25 );
            dateTimePicker1.TabIndex = 9;
            dateTimePicker1.Value = new DateTime( 2025, 11, 10, 0, 0, 0, 0 );
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // btnAgregar
            // 
            btnAgregar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnAgregar.Font = new Font( "Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0 );
            btnAgregar.Location = new Point( 518, 147 );
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size( 100, 38 );
            btnAgregar.TabIndex = 13;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Visible = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnEditar
            // 
            btnEditar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnEditar.Font = new Font( "Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0 );
            btnEditar.Location = new Point( 624, 147 );
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size( 100, 38 );
            btnEditar.TabIndex = 12;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Visible = false;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnEliminar.Font = new Font( "Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0 );
            btnEliminar.Location = new Point( 730, 147 );
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size( 100, 38 );
            btnEliminar.TabIndex = 11;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // ReporteExcepcion
            // 
            AutoScaleDimensions = new SizeF( 7F, 15F );
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new Size( 970, 616 );
            Controls.Add( btnAgregar );
            Controls.Add( btnEditar );
            Controls.Add( btnEliminar );
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
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnEliminar;
    }
}
