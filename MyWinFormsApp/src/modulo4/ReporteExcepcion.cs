
using iTextSharp.text;
using iTextSharp.text.pdf;
using MyWinFormsApp.src.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MyWinFormsApp.src.modulo4
{
    public partial class ReporteExcepcion : Form
    {
        public List<DispositivosNR> dispositivos = [];

        public ReporteExcepcion()
        {
            InitializeComponent();
        }

        private void ReporteExcepcion_Load( object sender, System.EventArgs e )
        {
            // Limpiar el DataGridView antes de cargar los datos
            dataGridView1.Rows.Clear();

            // Cargar los datos en el DataGridView
            dispositivos.Add( new DispositivosNR { ID = "DNR-001", Fecha = "24/10/2025", Cliente = "Juan Perez", Dispositivo = "Samsung S21 Ultra", Estado = "En espera", Dias = "29", MontoP = "110" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-002", Fecha = "26/10/2025", Cliente = "Pedro Ramos", Dispositivo = "Samsung S23 Ultra", Estado = "Reparado", Dias = "60", MontoP = "100" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-003", Fecha = "23/09/2025", Cliente = "Marina Lopez", Dispositivo = "Xiaomi Redmi Note 10", Estado = "Reparado", Dias = "50", MontoP = "320" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-004", Fecha = "12/09/2025", Cliente = "Claudia Mendez", Dispositivo = "Samsung A13", Estado = "Reparado", Dias = "23", MontoP = "842" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-005", Fecha = "10/09/2025", Cliente = "Diego Morales", Dispositivo = "Samsung S22 Ultra", Estado = "En espera", Dias = "8", MontoP = "790" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-006", Fecha = "14/10/2025", Cliente = "María López", Dispositivo = "Google Pixel 7", Estado = "En espera", Dias = "5", MontoP = "27" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-007", Fecha = "21/09/2025", Cliente = "Paula Herrera", Dispositivo = "iPhone 15 Pro", Estado = "En espera", Dias = "17", MontoP = "670" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-008", Fecha = "24/10/2025", Cliente = "Jorge Castillo", Dispositivo = "Huawei P30", Estado = "Reparado", Dias = "53", MontoP = "450" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-009", Fecha = "07/10/2025", Cliente = "Sergio Jimenez", Dispositivo = "Samsung S22 Ultra", Estado = "Reparado", Dias = "34", MontoP = "460" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-010", Fecha = "03/10/2025", Cliente = "Marta Ortiz", Dispositivo = "Xiaomi Redmi Note 10", Estado = "Reparado", Dias = "37", MontoP = "410" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-011", Fecha = "26/10/2025", Cliente = "Claudia Mendez", Dispositivo = "iPhone 14 Pro Max", Estado = "Reparado", Dias = "26", MontoP = "235" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-012", Fecha = "02/09/2025", Cliente = "Karina Gómez", Dispositivo = "iPad Air", Estado = "En espera", Dias = "74", MontoP = "400" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-013", Fecha = "21/09/2025", Cliente = "Raúl Pineda", Dispositivo = "Samsung S22", Estado = "Reparado", Dias = "11", MontoP = "249" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-014", Fecha = "27/10/2025", Cliente = "Sofía Martínez", Dispositivo = "Samsung S24", Estado = "Reparado", Dias = "6", MontoP = "360" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-015", Fecha = "18/10/2025", Cliente = "Marta Ortiz", Dispositivo = "Google Pixel 7", Estado = "Reparado", Dias = "66", MontoP = "540" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-016", Fecha = "08/09/2025", Cliente = "Juan Perez", Dispositivo = "Lenovo Tab M8", Estado = "En espera", Dias = "46", MontoP = "240" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-017", Fecha = "24/10/2025", Cliente = "Natalia Serrano", Dispositivo = "iPhone 15 Pro Max", Estado = "En espera", Dias = "89", MontoP = "860" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-018", Fecha = "16/09/2025", Cliente = "Natalia Serrano", Dispositivo = "iPhone 15", Estado = "Reparado", Dias = "68", MontoP = "675" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-019", Fecha = "13/10/2025", Cliente = "Andrés Cruz", Dispositivo = "iPad Mini", Estado = "Reparado", Dias = "25", MontoP = "160" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-020", Fecha = "20/09/2025", Cliente = "Fernando Duarte", Dispositivo = "iPhone 14", Estado = "Reparado", Dias = "23", MontoP = "13" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-021", Fecha = "03/10/2025", Cliente = "Isabel Torres", Dispositivo = "Motorola Moto G Power", Estado = "Reparado", Dias = "38", MontoP = "200" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-022", Fecha = "22/10/2025", Cliente = "Luis Hernández", Dispositivo = "Samsung Tab A8", Estado = "En espera", Dias = "28", MontoP = "600" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-023", Fecha = "24/09/2025", Cliente = "María López", Dispositivo = "Samsung S23 Ultra", Estado = "En espera", Dias = "27", MontoP = "615" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-024", Fecha = "23/09/2025", Cliente = "Isabel Torres", Dispositivo = "Huawei P30", Estado = "Reparado", Dias = "57", MontoP = "120" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-025", Fecha = "24/10/2025", Cliente = "Héctor Rivas", Dispositivo = "iPhone 14 Pro", Estado = "Reparado", Dias = "87", MontoP = "180" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-026", Fecha = "25/09/2025", Cliente = "Marina Lopez", Dispositivo = "Huawei P30", Estado = "Reparado", Dias = "22", MontoP = "275" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-027", Fecha = "30/09/2025", Cliente = "Adriana Silva", Dispositivo = "Huawei P30", Estado = "En espera", Dias = "21", MontoP = "280" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-028", Fecha = "05/10/2025", Cliente = "Ana Rodríguez", Dispositivo = "iPad Pro 12.9-inch", Estado = "En espera", Dias = "10", MontoP = "460" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-029", Fecha = "06/10/2025", Cliente = "Fernando Duarte", Dispositivo = "Huawei P30", Estado = "Reparado", Dias = "85", MontoP = "390" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-030", Fecha = "09/09/2025", Cliente = "Karina Gómez", Dispositivo = "Samsung S23 Ultra", Estado = "Reparado", Dias = "40", MontoP = "370" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-031", Fecha = "27/10/2025", Cliente = "Lorena Navarro", Dispositivo = "Samsung S24 Ultra", Estado = "En espera", Dias = "62", MontoP = "500" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-032", Fecha = "04/10/2025", Cliente = "Daniela Flores", Dispositivo = "Google Pixel 6", Estado = "En espera", Dias = "11", MontoP = "810" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-033", Fecha = "28/10/2025", Cliente = "María López", Dispositivo = "iPad Air", Estado = "En espera", Dias = "22", MontoP = "8" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-034", Fecha = "11/09/2025", Cliente = "María López", Dispositivo = "iPhone 14 Pro", Estado = "Reparado", Dias = "55", MontoP = "2009" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-035", Fecha = "24/09/2025", Cliente = "Valeria Ruiz", Dispositivo = "Samsung S23", Estado = "Reparado", Dias = "43", MontoP = "2971" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-036", Fecha = "11/10/2025", Cliente = "Raúl Pineda", Dispositivo = "iPhone 13 Pro Max", Estado = "En espera", Dias = "52", MontoP = "3211" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-037", Fecha = "06/09/2025", Cliente = "Roberto Aguilar", Dispositivo = "iPhone 15 Pro Max", Estado = "Reparado", Dias = "15", MontoP = "585" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-038", Fecha = "10/10/2025", Cliente = "Andrés Cruz", Dispositivo = "Samsung S22 Ultra", Estado = "En espera", Dias = "30", MontoP = "56" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-039", Fecha = "20/09/2025", Cliente = "Lucía Fernández", Dispositivo = "Google Pixel 7", Estado = "Reparado", Dias = "48", MontoP = "1606" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-040", Fecha = "11/10/2025", Cliente = "Carlos García", Dispositivo = "iPad Pro 11-inch", Estado = "Reparado", Dias = "67", MontoP = "608" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-041", Fecha = "27/09/2025", Cliente = "Eduardo Castillo", Dispositivo = "Huawei P30", Estado = "En espera", Dias = "25", MontoP = "3036" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-042", Fecha = "07/10/2025", Cliente = "Carlos García", Dispositivo = "iPhone 14", Estado = "Reparado", Dias = "72", MontoP = "849" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-043", Fecha = "10/10/2025", Cliente = "Ricardo Salinas", Dispositivo = "OnePlus 10", Estado = "En espera", Dias = "72", MontoP = "62" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-044", Fecha = "06/10/2025", Cliente = "Karina Gómez", Dispositivo = "Huawei P40", Estado = "En espera", Dias = "60", MontoP = "3123" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-045", Fecha = "21/09/2025", Cliente = "Monica Rojas", Dispositivo = "Samsung A13", Estado = "En espera", Dias = "8", MontoP = "1522" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-046", Fecha = "04/09/2025", Cliente = "Raúl Pineda", Dispositivo = "Samsung S23", Estado = "Reparado", Dias = "15", MontoP = "1926" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-047", Fecha = "27/10/2025", Cliente = "Carlos García", Dispositivo = "Samsung S22", Estado = "En espera", Dias = "24", MontoP = "3279" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-048", Fecha = "09/10/2025", Cliente = "Miguel Sánchez", Dispositivo = "Samsung S24", Estado = "Reparado", Dias = "27", MontoP = "449" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-049", Fecha = "02/10/2025", Cliente = "Lucía Fernández", Dispositivo = "iPhone 14 Plus", Estado = "Reparado", Dias = "9", MontoP = "209" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-050", Fecha = "10/10/2025", Cliente = "Marta Ortiz", Dispositivo = "Google Pixel 8", Estado = "Reparado", Dias = "63", MontoP = "2044" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-051", Fecha = "31/10/2025", Cliente = "Ana Rodríguez", Dispositivo = "Samsung A52", Estado = "Reparado", Dias = "33", MontoP = "1349" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-052", Fecha = "21/09/2025", Cliente = "Raúl Pineda", Dispositivo = "Huawei P40", Estado = "Reparado", Dias = "25", MontoP = "738" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-053", Fecha = "27/09/2025", Cliente = "Natalia Serrano", Dispositivo = "Huawei P30", Estado = "Reparado", Dias = "25", MontoP = "2755" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-054", Fecha = "29/10/2025", Cliente = "Juan Perez", Dispositivo = "OnePlus 9", Estado = "Reparado", Dias = "14", MontoP = "3212" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-055", Fecha = "09/09/2025", Cliente = "Natalia Serrano", Dispositivo = "iPhone 14 Pro", Estado = "Reparado", Dias = "38", MontoP = "2321" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-056", Fecha = "01/10/2025", Cliente = "Marta Ortiz", Dispositivo = "Samsung S20", Estado = "Reparado", Dias = "28", MontoP = "1516" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-057", Fecha = "28/10/2025", Cliente = "Camila Blanco", Dispositivo = "Xiaomi Redmi Note 10", Estado = "Reparado", Dias = "88", MontoP = "1845" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-058", Fecha = "04/10/2025", Cliente = "Jorge Castillo", Dispositivo = "Google Pixel 8", Estado = "Reparado", Dias = "30", MontoP = "2212" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-059", Fecha = "18/10/2025", Cliente = "Patricia Vargas", Dispositivo = "Samsung S24 Ultra", Estado = "En espera", Dias = "15", MontoP = "1726" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-060", Fecha = "30/10/2025", Cliente = "Lucía Fernández", Dispositivo = "Samsung S22", Estado = "Reparado", Dias = "11", MontoP = "1244" } );

            dispositivos.Add( new DispositivosNR { ID = "DNR-061", Fecha = "01/11/2025", Cliente = "Carlos Medina", Dispositivo = "Samsung A35", Estado = "Reparado", Dias = "20", MontoP = "820" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-062", Fecha = "01/11/2025", Cliente = "Laura Peralta", Dispositivo = "iPhone 12", Estado = "Reparado", Dias = "20", MontoP = "710" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-063", Fecha = "02/11/2025", Cliente = "Mario Duarte", Dispositivo = "Xiaomi Redmi 13", Estado = "En espera", Dias = "19", MontoP = "650" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-064", Fecha = "02/11/2025", Cliente = "Natalia Herrera", Dispositivo = "iPhone 14", Estado = "Reparado", Dias = "19", MontoP = "600" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-065", Fecha = "03/11/2025", Cliente = "Sofía Lagos", Dispositivo = "Samsung S21", Estado = "Reparado", Dias = "18", MontoP = "560" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-066", Fecha = "03/11/2025", Cliente = "Daniel Rosales", Dispositivo = "Motorola G73", Estado = "Reparado", Dias = "18", MontoP = "410" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-067", Fecha = "04/11/2025", Cliente = "Brenda Cáceres", Dispositivo = "Samsung A52", Estado = "En espera", Dias = "17", MontoP = "350" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-068", Fecha = "04/11/2025", Cliente = "Felipe Guardado", Dispositivo = "iPhone 11", Estado = "Reparado", Dias = "17", MontoP = "600" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-069", Fecha = "05/11/2025", Cliente = "Andrea Montalván", Dispositivo = "Google Pixel 6", Estado = "Reparado", Dias = "16", MontoP = "600" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-070", Fecha = "05/11/2025", Cliente = "Diego Maldonado", Dispositivo = "Xiaomi Redmi Note 12", Estado = "Reparado", Dias = "16", MontoP = "700" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-071", Fecha = "06/11/2025", Cliente = "Elena Caballero", Dispositivo = "Samsung S22", Estado = "Reparado", Dias = "15", MontoP = "500" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-072", Fecha = "06/11/2025", Cliente = "Ricardo Ventura", Dispositivo = "iPhone 13 Mini", Estado = "Reparado", Dias = "15", MontoP = "600" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-073", Fecha = "07/11/2025", Cliente = "Paola Benítez", Dispositivo = "Samsung A14", Estado = "En espera", Dias = "14", MontoP = "400" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-074", Fecha = "07/11/2025", Cliente = "Oscar Mejía", Dispositivo = "iPhone XR", Estado = "Reparado", Dias = "14", MontoP = "600" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-075", Fecha = "08/11/2025", Cliente = "Carolina López", Dispositivo = "Xiaomi Poco X5", Estado = "Reparado", Dias = "13", MontoP = "500" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-076", Fecha = "08/11/2025", Cliente = "Mauricio Rivera", Dispositivo = "Samsung S20 FE", Estado = "Reparado", Dias = "13", MontoP = "700" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-077", Fecha = "09/11/2025", Cliente = "Valeria Molina", Dispositivo = "iPhone 13", Estado = "Reparado", Dias = "12", MontoP = "600" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-078", Fecha = "09/11/2025", Cliente = "Javier Rivas", Dispositivo = "Motorola G32", Estado = "En espera", Dias = "12", MontoP = "300" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-079", Fecha = "10/11/2025", Cliente = "Rocío Sánchez", Dispositivo = "Samsung A25", Estado = "Reparado", Dias = "11", MontoP = "400" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-060", Fecha = "10/11/2025", Cliente = "Pablo Zamora", Dispositivo = "iPhone SE 2020", Estado = "Reparado", Dias = "11", MontoP = "600" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-081", Fecha = "11/11/2025", Cliente = "Tamara Fiallos", Dispositivo = "Xiaomi Mi 11 Lite", Estado = "Reparado", Dias = "10", MontoP = "700" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-082", Fecha = "11/11/2025", Cliente = "Luis Segura", Dispositivo = "Google Pixel 7a", Estado = "Reparado", Dias = "10", MontoP = "600" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-083", Fecha = "12/11/2025", Cliente = "Melisa Barahona", Dispositivo = "Samsung S23", Estado = "Reparado", Dias = "10", MontoP = "600" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-084", Fecha = "12/11/2025", Cliente = "Kevin Madrid", Dispositivo = "iPhone 12 Pro", Estado = "Reparado", Dias = "10", MontoP = "700" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-085", Fecha = "13/11/2025", Cliente = "Raquel Duarte", Dispositivo = "Xiaomi Redmi Note 11", Estado = "Reparado", Dias = "10", MontoP = "500" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-086", Fecha = "13/11/2025", Cliente = "Fabián Romero", Dispositivo = "Samsung A54", Estado = "Reparado", Dias = "10", MontoP = "600" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-087", Fecha = "14/11/2025", Cliente = "Daniela Aguilar", Dispositivo = "iPhone 15", Estado = "En espera", Dias = "10", MontoP = "400" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-088", Fecha = "14/11/2025", Cliente = "Erick Carrión", Dispositivo = "Samsung A04s", Estado = "Reparado", Dias = "10", MontoP = "300" } );

            // Filtrar datagridview
            FiltrarPorMes( DateTime.Now.Month );
        }

        private void btnExportarPDF_Click( object sender, System.EventArgs e )
        {
            if ( dispositivos.Count == 0 )
            {
                MessageBox.Show( "No hay datos para exportar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Archivos PDF (*.pdf)|*.pdf",
                Title = "Guardar reporte como PDF",
                FileName = "Reporte_DispositivosNR_" + DateTime.Now.ToString( "yyyyMMdd" ) + ".pdf"
            };

            if ( saveFileDialog.ShowDialog() == DialogResult.OK )
            {
                string filePath = saveFileDialog.FileName;
                try
                {
                    // Crear documento PDF
                    iTextSharp.text.Document doc = new( PageSize.A4.Rotate(), 10, 10, 10, 26 );
                    PdfWriter writer = PdfWriter.GetInstance( doc, new FileStream( filePath, FileMode.Create ) );
                    writer.PageEvent = new PdfFooter();
                    doc.Open();

                    // Capturar panel como imagen
                    Bitmap bmp = new Bitmap( panel1.Width, panel1.Height );
                    panel1.DrawToBitmap( bmp, new System.Drawing.Rectangle( 0, 0, bmp.Width, bmp.Height ) );

                    // Insertar panel como imagen en el PDF
                    using ( MemoryStream ms = new MemoryStream() )
                    {
                        bmp.Save( ms, System.Drawing.Imaging.ImageFormat.Png );
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance( ms.ToArray() );
                        img.ScaleToFit( doc.PageSize.Width - 20, doc.PageSize.Height - 20 );
                        img.Alignment = Element.ALIGN_CENTER;
                        doc.Add( img );
                    }

                    doc.Add( new Paragraph( "\n" ) );

                    // Insertar datos del DataGridView en una tabla PDF
                    PdfPTable pdfTable = new PdfPTable( dataGridView1.Columns.Count );
                    pdfTable.WidthPercentage = 100;

                    float[] columnWidths = new float[dataGridView1.Columns.Count];

                    // Columna ID más estrecha
                    columnWidths[0] = 7f;    // ID
                    columnWidths[1] = 12f;   // Fecha
                    columnWidths[2] = 16f;   // Cliente
                    columnWidths[3] = 16f;   // Dispositivo
                    columnWidths[4] = 12f;   // Estado
                    columnWidths[5] = 7f;    // Dias
                    columnWidths[6] = 15f;   // Monto pendiente
                    pdfTable.SetWidths( columnWidths );

                    // Estilo de tabla
                    iTextSharp.text.Font headerFont = new( iTextSharp.text.Font.FontFamily.UNDEFINED, 9, iTextSharp.text.Font.BOLD, BaseColor.WHITE );

                    // Agregar encabezados
                    foreach ( DataGridViewColumn column in dataGridView1.Columns )
                    {
                        PdfPCell cell = new PdfPCell( new Phrase( column.HeaderText, headerFont ) )
                        {
                            BackgroundColor = new BaseColor( 0, 32, 96 ),
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE,
                            MinimumHeight = 40f,
                            Padding = 5,
                        };
                        pdfTable.AddCell( cell );
                    }

                    // Agregar filas
                    foreach ( DataGridViewRow row in dataGridView1.Rows )
                    {
                        bool esFilaTotal = row.Cells[0].Value != null &&
                                           row.Cells[0].Value.ToString().Contains( "Total" );

                        BaseColor rowColor = esFilaTotal
                            ? BaseColor.LIGHT_GRAY
                            : (row.Index % 2 == 0 ? BaseColor.WHITE : new BaseColor( 225, 225, 225 ));

                        foreach ( DataGridViewCell cell in row.Cells )
                        {
                            string columnHeader = dataGridView1.Columns[cell.ColumnIndex].HeaderText;
                            object cellValue = cell.Value ?? string.Empty;

                            // 🔹 Fuente según si es fila total o no
                            var font = esFilaTotal
                                ? new iTextSharp.text.Font( iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD )
                                : new iTextSharp.text.Font( iTextSharp.text.Font.FontFamily.HELVETICA, 9 );

                            PdfPCell pdfCell = new PdfPCell( new Phrase( cellValue.ToString(), font ) )
                            {
                                BackgroundColor = rowColor,
                                HorizontalAlignment = Element.ALIGN_CENTER,
                                Padding = 4
                            };

                            pdfTable.AddCell( pdfCell );
                        }
                    }
                    doc.Add( pdfTable );
                    doc.Close();

                    MessageBox.Show( "Reporte exportado exitosamente a " + filePath, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information );
                }
                catch ( Exception ex )
                {
                    MessageBox.Show( "Error al exportar el reporte: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            }
        }

        public (DateTime inicio, DateTime fin) ObtenerRangoMes( DateTime fecha )
        {
            DateTime inicio = new DateTime( fecha.Year, fecha.Month, 1 );
            DateTime fin = inicio.AddMonths( 1 ).AddDays( -1 );
            return (inicio, fin);
        }

        private void FiltrarPorMes( int mesOffset )
        {
            DateTime fechaActual = DateTime.Now;
            DateTime fechaFiltro = fechaActual.AddMonths( mesOffset );

            DateTime diaInicio = new DateTime( fechaFiltro.Year, fechaFiltro.Month, 1 );
            DateTime diaFin = diaInicio.AddMonths( 1 ).AddDays( -1 );

            lblPeriodo.Text = $"({diaInicio:dd/MM/yyyy} - {diaFin:dd/MM/yyyy})";
            lblFechaG.Text = DateTime.Now.ToString( "dd 'de' MMMM 'de' yyyy", new System.Globalization.CultureInfo( "es-ES" ) );

            var (inicio, fin) = ObtenerRangoMes( fechaFiltro );
            var dispositivosFiltrados = dispositivos.Where( d =>
            {
                if ( DateTime.TryParse( d.Fecha, out DateTime fechaDispositivo ) )
                {
                    return fechaDispositivo >= inicio && fechaDispositivo <= fin;
                }
                return false;
            } ).OrderBy( d => DateTime.Parse( d.Fecha ) )
                .ToList();

            dataGridView1.Rows.Clear();
            foreach ( var dispositivo in dispositivosFiltrados )
            {
                string txtMonto = decimal.TryParse( dispositivo.MontoP, out decimal monto ) ? "L." + monto.ToString( "N2" ) : "0.00";
                dataGridView1.Rows.Add( dispositivo.ID, dispositivo.Fecha, dispositivo.Cliente, dispositivo.Dispositivo, dispositivo.Estado, dispositivo.Dias, txtMonto );
            }

            // Calcular y mostrar el total
            decimal totalMonto = dispositivosFiltrados.Sum( d => decimal.TryParse( d.MontoP, out decimal monto ) ? monto : 0 );
            int index = dataGridView1.Rows.Add();
            DataGridViewRow totalRow = dataGridView1.Rows[index];

            totalRow.DefaultCellStyle.Font = new System.Drawing.Font( dataGridView1.Font, FontStyle.Bold );
            totalRow.DefaultCellStyle.BackColor = Color.LightGray;
            totalRow.Cells[0].Value = "Total:";
            totalRow.Cells[6].Value = "L." + totalMonto.ToString( "N2" ).PadLeft( 18 );
        }

        private void dateTimePicker1_ValueChanged( object sender, EventArgs e )
        {
            FiltrarPorMes( dateTimePicker1.Value.Month - DateTime.Now.Month );
        }

        private void btnAgregar_Click( object sender, EventArgs e )
        {

        }

        private void btnEditar_Click( object sender, EventArgs e )
        {
            if ( dataGridView1.SelectedRows.Count == 0 )
            {
                MessageBox.Show( "Por favor, seleccione una fila para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            string idDispositivo = selectedRow.Cells[0].Value.ToString();
            DispositivosNR dispositivoToEdit = dispositivos.FirstOrDefault( d => d.ID == idDispositivo );

        }

        private void btnEliminar_Click( object sender, EventArgs e )
        {
            if ( dataGridView1.SelectedRows.Count == 0 )
            {
                MessageBox.Show( "Por favor, seleccione una fila para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            string idDispositivo = selectedRow.Cells[0].Value.ToString();
            var result = MessageBox.Show( $"¿Está seguro de que desea eliminar el dispositivo con ID {idDispositivo}?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question );
            if ( result == DialogResult.Yes )
            {
                var dispositivoToRemove = dispositivos.FirstOrDefault( d => d.ID == idDispositivo );
                if ( dispositivoToRemove != null )
                {
                    dispositivos.Remove( dispositivoToRemove );
                    dataGridView1.Rows.Remove( selectedRow );
                    MessageBox.Show( "Registro eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information );
                    FiltrarPorMes( DateTime.Now.Month );
                }
            }
        }
    }
}
