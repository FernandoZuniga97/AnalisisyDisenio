using System;
using System.Drawing;
using System.IO;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
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
            panel1.BackColor = Color.FromArgb(0, 32, 96);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(lblFechas);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(985, 121);
            panel1.TabIndex = 0;
            // 
            // label4
            // 
            label4.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = SystemColors.Control;
            label4.Location = new Point(459, 54);
            label4.Name = "label4";
            label4.Size = new Size(237, 25);
            label4.TabIndex = 4;
            label4.Text = "Reporte de Mantenimiento";
            // 
            // lblFechas
            // 
            lblFechas.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblFechas.AutoSize = true;
            lblFechas.Font = new Font("Segoe UI", 11.25F);
            lblFechas.ForeColor = SystemColors.Control;
            lblFechas.Location = new Point(514, 86);
            lblFechas.Name = "lblFechas";
            lblFechas.Size = new Size(29, 20);
            lblFechas.TabIndex = 3;
            lblFechas.Text = "{}{}";
            // 
            // label2
            // 
            label2.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F);
            label2.ForeColor = SystemColors.Control;
            label2.Location = new Point(456, 86);
            label2.Name = "label2";
            label2.Size = new Size(63, 20);
            label2.TabIndex = 2;
            label2.Text = "Periodo:";
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(435, 13);
            label1.Name = "label1";
            label1.Size = new Size(293, 32);
            label1.TabIndex = 1;
            label1.Text = "IMPORTACIONES GICELL";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Image.FromFile(
    Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "..", "..", "..", "src", "login", "Image", "logo_g.jpg"
    )
);
            pictureBox1.Location = new Point(7, 5);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(110, 110);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // btnExportarPDF
            // 
            btnExportarPDF.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnExportarPDF.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExportarPDF.Location = new Point(894, 139);
            btnExportarPDF.Name = "btnExportarPDF";
            btnExportarPDF.Size = new Size(100, 38);
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
            dataGridViewCellStyle14.BackColor = Color.FromArgb(224, 224, 224);
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle14;
            dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.BackColor = Color.FromArgb(0, 32, 96);
            dataGridViewCellStyle15.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle15.ForeColor = Color.White;
            dataGridViewCellStyle15.SelectionBackColor = Color.FromArgb(0, 32, 96);
            dataGridViewCellStyle15.SelectionForeColor = Color.White;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle15;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { txtID, lblFecha, lblDispostivo, lblTipo, lblDescripcion, lblTenico, lblMateriales, lblCosto, lblObservaciones });
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle25.BackColor = SystemColors.Window;
            dataGridViewCellStyle25.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle25.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle25.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle25.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle25;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle26.BackColor = Color.FromArgb(0, 32, 96);
            dataGridViewCellStyle26.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle26.ForeColor = Color.White;
            dataGridViewCellStyle26.SelectionBackColor = Color.FromArgb(0, 32, 96);
            dataGridViewCellStyle26.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle26;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Size = new Size(982, 481);
            dataGridView1.TabIndex = 0;
            // 
            // txtID
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            txtID.DefaultCellStyle = dataGridViewCellStyle16;
            txtID.Frozen = true;
            txtID.HeaderText = "ID";
            txtID.Name = "txtID";
            txtID.ReadOnly = true;
            txtID.Width = 55;
            // 
            // lblFecha
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            lblFecha.DefaultCellStyle = dataGridViewCellStyle17;
            lblFecha.Frozen = true;
            lblFecha.HeaderText = "Fecha";
            lblFecha.Name = "lblFecha";
            lblFecha.ReadOnly = true;
            lblFecha.Width = 80;
            // 
            // lblDispostivo
            // 
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            lblDispostivo.DefaultCellStyle = dataGridViewCellStyle18;
            lblDispostivo.Frozen = true;
            lblDispostivo.HeaderText = "Dispositivo";
            lblDispostivo.Name = "lblDispostivo";
            lblDispostivo.ReadOnly = true;
            lblDispostivo.Width = 130;
            // 
            // lblTipo
            // 
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            lblTipo.DefaultCellStyle = dataGridViewCellStyle19;
            lblTipo.Frozen = true;
            lblTipo.HeaderText = "Tipo de Mantenimiento";
            lblTipo.Name = "lblTipo";
            lblTipo.ReadOnly = true;
            // 
            // lblDescripcion
            // 
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            lblDescripcion.DefaultCellStyle = dataGridViewCellStyle20;
            lblDescripcion.Frozen = true;
            lblDescripcion.HeaderText = "Descripcion de la actividad";
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.ReadOnly = true;
            lblDescripcion.Width = 150;
            // 
            // lblTenico
            // 
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            lblTenico.DefaultCellStyle = dataGridViewCellStyle21;
            lblTenico.Frozen = true;
            lblTenico.HeaderText = "Tecnico responsable";
            lblTenico.Name = "lblTenico";
            lblTenico.ReadOnly = true;
            // 
            // lblMateriales
            // 
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            lblMateriales.DefaultCellStyle = dataGridViewCellStyle22;
            lblMateriales.Frozen = true;
            lblMateriales.HeaderText = "Materiales usados";
            lblMateriales.Name = "lblMateriales";
            lblMateriales.ReadOnly = true;
            lblMateriales.Width = 125;
            // 
            // lblCosto
            // 
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle23.NullValue = null;
            lblCosto.DefaultCellStyle = dataGridViewCellStyle23;
            lblCosto.Frozen = true;
            lblCosto.HeaderText = "Costo estimado (L.)";
            lblCosto.Name = "lblCosto";
            lblCosto.ReadOnly = true;
            lblCosto.Width = 110;
            // 
            // lblObservaciones
            // 
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            lblObservaciones.DefaultCellStyle = dataGridViewCellStyle24;
            lblObservaciones.Frozen = true;
            lblObservaciones.HeaderText = "Observaciones";
            lblObservaciones.Name = "lblObservaciones";
            lblObservaciones.ReadOnly = true;
            lblObservaciones.Width = 150;
            // 
            // panel2
            // 
            panel2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panel2.Controls.Add(dataGridView1);
            panel2.Location = new Point(12, 183);
            panel2.Name = "panel2";
            panel2.Size = new Size(985, 484);
            panel2.TabIndex = 1;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            dateTimePicker1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new Point(67, 142);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(103, 25);
            dateTimePicker1.TabIndex = 6;
            dateTimePicker1.Value = new DateTime(2025, 11, 10, 0, 0, 0, 0);
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // label5
            // 
            label5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(15, 145);
            label5.Name = "label5";
            label5.Size = new Size(52, 17);
            label5.TabIndex = 7;
            label5.Text = "Fechas:";
            // 
            // ReporteMantenimiento
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1009, 679);
            Controls.Add(label5);
            Controls.Add(dateTimePicker1);
            Controls.Add(btnExportarPDF);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "ReporteMantenimiento";
            Text = "ReporteMantenimiento";
            Load += ReporteMantenimiento_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
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
