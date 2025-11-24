namespace MyWinFormsApp.src.modulo4
{
    partial class FormAgregar1
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
            btnAgregar = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            btnCancelar = new System.Windows.Forms.Button();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            cmbDispositivo = new System.Windows.Forms.ComboBox();
            cmbTipo = new System.Windows.Forms.ComboBox();
            cmbTecnico = new System.Windows.Forms.ComboBox();
            txtDescripcion = new System.Windows.Forms.TextBox();
            txtCosto = new System.Windows.Forms.TextBox();
            txtMateriales = new System.Windows.Forms.TextBox();
            txtObservaciones = new System.Windows.Forms.TextBox();
            cmbMaterial = new System.Windows.Forms.ComboBox();
            btnAddPart = new System.Windows.Forms.Button();
            btnDeletePart = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // btnAgregar
            // 
            btnAgregar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnAgregar.Font = new System.Drawing.Font( "Segoe UI", 11.25F, System.Drawing.FontStyle.Bold );
            btnAgregar.Location = new System.Drawing.Point( 150, 452 );
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new System.Drawing.Size( 104, 45 );
            btnAgregar.TabIndex = 18;
            btnAgregar.Text = "Guardar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font( "Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0 );
            label1.Location = new System.Drawing.Point( 151, 24 );
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size( 291, 25 );
            label1.TabIndex = 19;
            label1.Text = "Agregar Nuevo Mantenimiento";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font( "Segoe UI", 11.25F );
            label2.Location = new System.Drawing.Point( 41, 79 );
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size( 50, 20 );
            label2.TabIndex = 20;
            label2.Text = "Fecha:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font( "Segoe UI", 11.25F );
            label3.Location = new System.Drawing.Point( 41, 149 );
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size( 86, 20 );
            label3.TabIndex = 21;
            label3.Text = "Dispositivo:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font( "Segoe UI", 11.25F );
            label4.Location = new System.Drawing.Point( 41, 218 );
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size( 147, 20 );
            label4.TabIndex = 22;
            label4.Text = "Tipo Mantenimiento:";
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnCancelar.Font = new System.Drawing.Font( "Segoe UI", 11.25F, System.Drawing.FontStyle.Bold );
            btnCancelar.Location = new System.Drawing.Point( 269, 452 );
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new System.Drawing.Size( 104, 45 );
            btnCancelar.TabIndex = 23;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font( "Segoe UI", 11.25F );
            label5.Location = new System.Drawing.Point( 291, 87 );
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size( 90, 20 );
            label5.TabIndex = 24;
            label5.Text = "Descripción:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font( "Segoe UI", 11.25F );
            label6.Location = new System.Drawing.Point( 41, 292 );
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size( 62, 20 );
            label6.TabIndex = 25;
            label6.Text = "Técnico:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font( "Segoe UI", 11.25F );
            label7.Location = new System.Drawing.Point( 291, 201 );
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size( 81, 20 );
            label7.TabIndex = 26;
            label7.Text = "Materiales:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font( "Segoe UI", 11.25F );
            label8.Location = new System.Drawing.Point( 291, 333 );
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size( 108, 20 );
            label8.TabIndex = 27;
            label8.Text = "Observaciones:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font( "Segoe UI", 11.25F );
            label9.Location = new System.Drawing.Point( 41, 369 );
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size( 140, 20 );
            label9.TabIndex = 28;
            label9.Text = "Costo Estimado (L.):";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Checked = false;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new System.Drawing.Point( 41, 100 );
            dateTimePicker1.MinDate = new System.DateTime( 2024, 1, 1, 0, 0, 0, 0 );
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new System.Drawing.Size( 104, 23 );
            dateTimePicker1.TabIndex = 29;
            // 
            // cmbDispositivo
            // 
            cmbDispositivo.FormattingEnabled = true;
            cmbDispositivo.Items.AddRange( new object[] { "iPhone 15 Pro Max", "iPhone 15 Pro", "iPhone 15", "iPhone 15 Plus", "iPhone 14 Pro Max", "iPhone 14 Pro", "iPhone 14", "iPhone 13 mini", "iPhone SE (2022)", "Samsung Galaxy S24 Ultra", "Samsung Galaxy S24+", "Samsung Galaxy S24", "Samsung Galaxy Z Fold5", "Samsung Galaxy Z Flip5", "Samsung Galaxy A54 5G", "Samsung Galaxy A34 5G", "Samsung Galaxy M54", "Google Pixel 8 Pro", "Google Pixel 8", "Google Pixel 7a", "Google Pixel Fold", "Xiaomi 13 Ultra", "Xiaomi 13 Pro", "Redmi Note 13 Pro+", "POCO F5", "OnePlus 12", "OnePlus 11", "OnePlus Nord 3", "Motorola Edge 40 Pro", "Motorola Razr 40 Ultra", "Honor Magic 5 Pro" } );
            cmbDispositivo.Location = new System.Drawing.Point( 41, 172 );
            cmbDispositivo.Name = "cmbDispositivo";
            cmbDispositivo.Size = new System.Drawing.Size( 147, 23 );
            cmbDispositivo.TabIndex = 30;
            // 
            // cmbTipo
            // 
            cmbTipo.FormattingEnabled = true;
            cmbTipo.Items.AddRange( new object[] { "Correctivo", "Preventivo" } );
            cmbTipo.Location = new System.Drawing.Point( 41, 241 );
            cmbTipo.Name = "cmbTipo";
            cmbTipo.Size = new System.Drawing.Size( 147, 23 );
            cmbTipo.TabIndex = 31;
            // 
            // cmbTecnico
            // 
            cmbTecnico.FormattingEnabled = true;
            cmbTecnico.Items.AddRange( new object[] { "L. Reyes", "M. Perez", "C. Torres", "J. Martinez", "A. Gomez", "D. Lopez" } );
            cmbTecnico.Location = new System.Drawing.Point( 41, 315 );
            cmbTecnico.Name = "cmbTecnico";
            cmbTecnico.Size = new System.Drawing.Size( 147, 23 );
            cmbTecnico.TabIndex = 32;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new System.Drawing.Point( 291, 110 );
            txtDescripcion.MaxLength = 200;
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new System.Drawing.Size( 259, 59 );
            txtDescripcion.TabIndex = 33;
            // 
            // txtCosto
            // 
            txtCosto.Location = new System.Drawing.Point( 41, 392 );
            txtCosto.MaxLength = 8;
            txtCosto.Name = "txtCosto";
            txtCosto.PlaceholderText = "0.00";
            txtCosto.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            txtCosto.Size = new System.Drawing.Size( 147, 23 );
            txtCosto.TabIndex = 34;
            txtCosto.KeyPress += txtCosto_KeyPress;
            // 
            // txtMateriales
            // 
            txtMateriales.Location = new System.Drawing.Point( 291, 253 );
            txtMateriales.Multiline = true;
            txtMateriales.Name = "txtMateriales";
            txtMateriales.ReadOnly = true;
            txtMateriales.Size = new System.Drawing.Size( 259, 59 );
            txtMateriales.TabIndex = 35;
            // 
            // txtObservaciones
            // 
            txtObservaciones.Location = new System.Drawing.Point( 291, 356 );
            txtObservaciones.MaxLength = 200;
            txtObservaciones.Multiline = true;
            txtObservaciones.Name = "txtObservaciones";
            txtObservaciones.Size = new System.Drawing.Size( 259, 59 );
            txtObservaciones.TabIndex = 36;
            // 
            // cmbMaterial
            // 
            cmbMaterial.FormattingEnabled = true;
            cmbMaterial.Items.AddRange( new object[] { "Puerto USB-C", "Marco frontal", "Resorte botón", "Pantalla original", "Cristal templado", "Lente cámara", "Adhesivo", "Altavoz", "Micrófono", "Conector de carga", "Pasta térmica", "Sensor de proximidad" } );
            cmbMaterial.Location = new System.Drawing.Point( 293, 224 );
            cmbMaterial.Name = "cmbMaterial";
            cmbMaterial.Size = new System.Drawing.Size( 178, 23 );
            cmbMaterial.TabIndex = 37;
            // 
            // btnAddPart
            // 
            btnAddPart.Font = new System.Drawing.Font( "Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0 );
            btnAddPart.Location = new System.Drawing.Point( 477, 224 );
            btnAddPart.Name = "btnAddPart";
            btnAddPart.Size = new System.Drawing.Size( 35, 23 );
            btnAddPart.TabIndex = 38;
            btnAddPart.Text = "+";
            btnAddPart.UseVisualStyleBackColor = true;
            btnAddPart.Click += btnAddPart_Click;
            // 
            // btnDeletePart
            // 
            btnDeletePart.Font = new System.Drawing.Font( "Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0 );
            btnDeletePart.Location = new System.Drawing.Point( 515, 224 );
            btnDeletePart.Name = "btnDeletePart";
            btnDeletePart.Size = new System.Drawing.Size( 35, 23 );
            btnDeletePart.TabIndex = 39;
            btnDeletePart.Text = "-";
            btnDeletePart.UseVisualStyleBackColor = true;
            btnDeletePart.Click += btnDeletePart_Click;
            // 
            // FormAgregar1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF( 7F, 15F );
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size( 596, 534 );
            Controls.Add( btnDeletePart );
            Controls.Add( btnAddPart );
            Controls.Add( cmbMaterial );
            Controls.Add( txtObservaciones );
            Controls.Add( txtMateriales );
            Controls.Add( txtCosto );
            Controls.Add( txtDescripcion );
            Controls.Add( cmbTecnico );
            Controls.Add( cmbTipo );
            Controls.Add( cmbDispositivo );
            Controls.Add( dateTimePicker1 );
            Controls.Add( label9 );
            Controls.Add( label8 );
            Controls.Add( label7 );
            Controls.Add( label6 );
            Controls.Add( label5 );
            Controls.Add( btnCancelar );
            Controls.Add( label4 );
            Controls.Add( label3 );
            Controls.Add( label2 );
            Controls.Add( label1 );
            Controls.Add( btnAgregar );
            Name = "FormAgregar1";
            Text = "FormAgregar1";
            Load += FormAgregar1_Load;
            ResumeLayout( false );
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ComboBox cmbDispositivo;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.ComboBox cmbTecnico;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtCosto;
        private System.Windows.Forms.TextBox txtMateriales;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.ComboBox cmbMaterial;
        private System.Windows.Forms.Button btnAddPart;
        private System.Windows.Forms.Button btnDeletePart;
    }
}
