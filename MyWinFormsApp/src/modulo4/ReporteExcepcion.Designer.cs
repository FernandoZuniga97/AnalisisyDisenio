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
            txtID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblDispostivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblDiasSinRetiro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblMontoPendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            panel1 = new System.Windows.Forms.Panel();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.Controls.Add( dataGridView1 );
            panel2.Location = new System.Drawing.Point( 12, 118 );
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size( 776, 320 );
            panel2.TabIndex = 3;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange( new System.Windows.Forms.DataGridViewColumn[] { txtID, lblFecha, lblCliente, lblDispostivo, lblEstado, lblDiasSinRetiro, lblMontoPendiente } );
            dataGridView1.Location = new System.Drawing.Point( 0, 3 );
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new System.Drawing.Size( 776, 317 );
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
            lblMontoPendiente.HeaderText = "Monto pendiente";
            lblMontoPendiente.Name = "lblMontoPendiente";
            lblMontoPendiente.ReadOnly = true;
            lblMontoPendiente.Width = 114;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.FromArgb( 0, 32, 96 );
            panel1.Controls.Add( label4 );
            panel1.Controls.Add( label3 );
            panel1.Controls.Add( label2 );
            panel1.Controls.Add( label1 );
            panel1.Controls.Add( pictureBox1 );
            panel1.Location = new System.Drawing.Point( 12, 12 );
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size( 776, 100 );
            panel1.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font( "Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0 );
            label4.ForeColor = System.Drawing.SystemColors.Control;
            label4.Location = new System.Drawing.Point( 292, 45 );
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size( 219, 25 );
            label4.TabIndex = 4;
            label4.Text = "Dispositivos no retirados";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = System.Drawing.SystemColors.Control;
            label3.Location = new System.Drawing.Point( 413, 76 );
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size( 23, 15 );
            label3.TabIndex = 3;
            label3.Text = "{}{}";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = System.Drawing.SystemColors.Control;
            label2.Location = new System.Drawing.Point( 358, 76 );
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
            pictureBox1.Location = new System.Drawing.Point( 3, 3 );
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size( 103, 97 );
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // ReporteExcepcion
            // 
            AutoScaleDimensions = new System.Drawing.SizeF( 7F, 15F );
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size( 800, 450 );
            Controls.Add( panel2 );
            Controls.Add( panel1 );
            Name = "ReporteExcepcion";
            Text = "ReporteExcepcion";
            panel2.ResumeLayout( false );
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout( false );
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout( false );
        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtID;
        private System.Windows.Forms.DataGridViewTextBoxColumn lblFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn lblCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn lblDispostivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn lblEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn lblDiasSinRetiro;
        private System.Windows.Forms.DataGridViewTextBoxColumn lblMontoPendiente;
    }
}