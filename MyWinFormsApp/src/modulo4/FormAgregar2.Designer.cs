namespace MyWinFormsApp.src.modulo4
{
    partial class FormAgregar2
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
            btnCancelar = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            btnAgregar = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnCancelar.Font = new System.Drawing.Font( "Segoe UI", 11.25F, System.Drawing.FontStyle.Bold );
            btnCancelar.Location = new System.Drawing.Point( 269, 477 );
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new System.Drawing.Size( 104, 45 );
            btnCancelar.TabIndex = 45;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font( "Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0 );
            label1.Location = new System.Drawing.Point( 184, 48 );
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size( 143, 25 );
            label1.TabIndex = 41;
            label1.Text = "Editar Registro";
            // 
            // btnAgregar
            // 
            btnAgregar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnAgregar.Font = new System.Drawing.Font( "Segoe UI", 11.25F, System.Drawing.FontStyle.Bold );
            btnAgregar.Location = new System.Drawing.Point( 150, 477 );
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new System.Drawing.Size( 104, 45 );
            btnAgregar.TabIndex = 40;
            btnAgregar.Text = "Guardar";
            btnAgregar.UseVisualStyleBackColor = true;
            // 
            // FormAgregar2
            // 
            AutoScaleDimensions = new System.Drawing.SizeF( 7F, 15F );
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size( 591, 570 );
            Controls.Add( btnCancelar );
            Controls.Add( label1 );
            Controls.Add( btnAgregar );
            Name = "FormAgregar2";
            Text = "FormAgregar2";
            ResumeLayout( false );
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAgregar;
    }
}