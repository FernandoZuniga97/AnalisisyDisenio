using MyWinFormsApp.src.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyWinFormsApp.src.modulo4
{
    public partial class FormAgregar1 : Form
    {
        private RegistroMantenimiento registroAEditar;

        public FormAgregar1( RegistroMantenimiento registroAEditar = null )
        {
            InitializeComponent();

            this.registroAEditar = registroAEditar;
        }

        private void FormAgregar1_Load( object sender, EventArgs e )
        {
            ClearFields();

            if ( registroAEditar != null )
            {
                label1.Text = "Editar Registro de Mantenimiento";

                // Dispositivo
                if ( !cmbDispositivo.Items.Contains( registroAEditar.Dispositivo ) )
                {
                    cmbDispositivo.Items.Add( registroAEditar.Dispositivo );
                }
                cmbDispositivo.SelectedItem = registroAEditar.Dispositivo;

                // Fecha
                dateTimePicker1.Value = DateTime.Parse( registroAEditar.Fecha );

                // Tipo Dispositivo

                // Tipo Mantenimiento
                cmbTipo.SelectedItem = registroAEditar.Tipo;

                // Descripcion
                txtDescripcion.Text = registroAEditar.Descripcion;

                // Tecnico
                if ( !cmbTecnico.Items.Contains( registroAEditar.Tecnico ) )
                {
                    cmbTecnico.Items.Add( registroAEditar.Tecnico );
                }
                cmbTecnico.SelectedItem = registroAEditar.Tecnico;

                // Materiales
                txtMateriales.Text = registroAEditar.Materiales.Replace( ", ", "; " );

                // Costo
                txtCosto.Text = registroAEditar.Costo;

                // Observaciones
                txtObservaciones.Text = registroAEditar.Observaciones;
            }
            else
            {
                dateTimePicker1.MinDate = DateTime.Now;
            }
        }

        public RegistroMantenimiento NuevoRegistro
        {
            get
            {
                Random rnd = new Random();
                int lastID = rnd.Next(311, 1000); // Simulando la obtención del último ID utilizado
                string newID = "M-" + (lastID).ToString();

                return new RegistroMantenimiento
                {
                    ID = registroAEditar != null ? registroAEditar.ID : newID,
                    Fecha = dateTimePicker1.Value.ToString( "dd/MM/yyyy" ),
                    Dispositivo = cmbDispositivo.Text,
                    TipoDispositivo = registroAEditar != null ? registroAEditar.TipoDispositivo : "Smartphone",
                    Tipo = cmbTipo.Text,
                    Descripcion = txtDescripcion.Text,
                    Tecnico = cmbTecnico.Text,
                    Materiales = txtMateriales.Text,
                    Costo = txtCosto.Text,
                    Observaciones = txtObservaciones.Text

                };
            }
        }

        public void ClearFields()
        {
            cmbDispositivo.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
            cmbTipo.SelectedIndex = -1;
            txtDescripcion.Clear();
            cmbTecnico.SelectedIndex = -1;
            txtMateriales.Clear();
            txtCosto.Clear();
            txtObservaciones.Clear();
        }

        private void btnAgregar_Click( object sender, EventArgs e )
        {
            if ( string.IsNullOrWhiteSpace( cmbDispositivo.Text ) ||
                 string.IsNullOrWhiteSpace( cmbTipo.Text ) ||
                 string.IsNullOrWhiteSpace( txtDescripcion.Text ) ||
                 string.IsNullOrWhiteSpace( cmbTecnico.Text ) ||
                 string.IsNullOrWhiteSpace( txtMateriales.Text ) ||
                 string.IsNullOrWhiteSpace( txtCosto.Text ) )
            {
                MessageBox.Show( "Por favor, complete todos los campos obligatorios.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click( object sender, EventArgs e )
        {
            this.Close();
        }

        private void txtCosto_KeyPress( object sender, KeyPressEventArgs e )
        {
            if ( !char.IsControl( e.KeyChar ) && !char.IsDigit( e.KeyChar ) && e.KeyChar != '.' )
            {
                e.Handled = true;
            }

            // Permitir solo un punto decimal
            if ( e.KeyChar == '.' && (sender as TextBox).Text.Contains( '.' ) )
            {
                e.Handled = true;
            }
        }

        private void btnAddPart_Click( object sender, EventArgs e )
        {
            txtMateriales.Text += cmbMaterial.Text + "; ";
        }

        private void btnDeletePart_Click( object sender, EventArgs e )
        {
            if ( txtMateriales.Text.Contains( cmbMaterial.Text + "; " ) )
            {
                txtMateriales.Text = txtMateriales.Text.Replace( cmbMaterial.Text + "; ", "" );
            }
        }
    }
}
