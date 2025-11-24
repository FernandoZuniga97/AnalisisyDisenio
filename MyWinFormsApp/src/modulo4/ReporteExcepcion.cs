
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
        private readonly List<DispositivosNR> dispositivos = [];

        public ReporteExcepcion()
        {
            InitializeComponent();
        }

        private void ReporteExcepcion_Load( object sender, System.EventArgs e )
        {
            // Limpiar el DataGridView antes de cargar los datos
            dataGridView1.Rows.Clear();
            FiltrarPorMes( DateTime.Now.Month );

            // Cargar los datos en el DataGridView
            dispositivos.Add( new DispositivosNR { ID = "DNR-001", Fecha = "24/10/2025", Cliente = "Juan Perez", Dispositivo = "Samsung S21 Ultra", Estado = "En espera", Dias = "29", MontoP = "1106" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-002", Fecha = "26/10/2025", Cliente = "Pedro Ramos", Dispositivo = "Samsung S23 Ultra", Estado = "Reparado", Dias = "60", MontoP = "1069" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-003", Fecha = "23/09/2025", Cliente = "Marina Lopez", Dispositivo = "Xiaomi Redmi Note 10", Estado = "Reparado", Dias = "50", MontoP = "3023" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-004", Fecha = "12/09/2025", Cliente = "Claudia Mendez", Dispositivo = "Samsung A13", Estado = "Reparado", Dias = "23", MontoP = "842" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-005", Fecha = "10/09/2025", Cliente = "Diego Morales", Dispositivo = "Samsung S22 Ultra", Estado = "En espera", Dias = "8", MontoP = "1879" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-006", Fecha = "14/10/2025", Cliente = "María López", Dispositivo = "Google Pixel 7", Estado = "En espera", Dias = "5", MontoP = "27" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-007", Fecha = "21/09/2025", Cliente = "Paula Herrera", Dispositivo = "iPhone 15 Pro", Estado = "En espera", Dias = "17", MontoP = "2672" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-008", Fecha = "24/10/2025", Cliente = "Jorge Castillo", Dispositivo = "Huawei P30", Estado = "Reparado", Dias = "53", MontoP = "952" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-009", Fecha = "07/10/2025", Cliente = "Sergio Jimenez", Dispositivo = "Samsung S22 Ultra", Estado = "Reparado", Dias = "34", MontoP = "490" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-010", Fecha = "03/10/2025", Cliente = "Marta Ortiz", Dispositivo = "Xiaomi Redmi Note 10", Estado = "Reparado", Dias = "37", MontoP = "2410" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-011", Fecha = "26/10/2025", Cliente = "Claudia Mendez", Dispositivo = "iPhone 14 Pro Max", Estado = "Reparado", Dias = "26", MontoP = "1234" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-012", Fecha = "02/09/2025", Cliente = "Karina Gómez", Dispositivo = "iPad Air", Estado = "En espera", Dias = "74", MontoP = "1400" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-013", Fecha = "21/09/2025", Cliente = "Raúl Pineda", Dispositivo = "Samsung S22", Estado = "Reparado", Dias = "11", MontoP = "249" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-014", Fecha = "27/10/2025", Cliente = "Sofía Martínez", Dispositivo = "Samsung S24", Estado = "Reparado", Dias = "6", MontoP = "3163" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-015", Fecha = "18/10/2025", Cliente = "Marta Ortiz", Dispositivo = "Google Pixel 7", Estado = "Reparado", Dias = "66", MontoP = "2547" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-016", Fecha = "08/09/2025", Cliente = "Juan Perez", Dispositivo = "Lenovo Tab M8", Estado = "En espera", Dias = "46", MontoP = "3162" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-017", Fecha = "24/10/2025", Cliente = "Natalia Serrano", Dispositivo = "iPhone 15 Pro Max", Estado = "En espera", Dias = "89", MontoP = "2861" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-018", Fecha = "16/09/2025", Cliente = "Natalia Serrano", Dispositivo = "iPhone 15", Estado = "Reparado", Dias = "68", MontoP = "675" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-019", Fecha = "13/10/2025", Cliente = "Andrés Cruz", Dispositivo = "iPad Mini", Estado = "Reparado", Dias = "25", MontoP = "1180" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-020", Fecha = "20/09/2025", Cliente = "Fernando Duarte", Dispositivo = "iPhone 14", Estado = "Reparado", Dias = "23", MontoP = "13" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-021", Fecha = "03/10/2025", Cliente = "Isabel Torres", Dispositivo = "Motorola Moto G Power", Estado = "Reparado", Dias = "38", MontoP = "2069" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-022", Fecha = "22/10/2025", Cliente = "Luis Hernández", Dispositivo = "Samsung Tab A8", Estado = "En espera", Dias = "28", MontoP = "996" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-023", Fecha = "24/09/2025", Cliente = "María López", Dispositivo = "Samsung S23 Ultra", Estado = "En espera", Dias = "27", MontoP = "1616" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-024", Fecha = "23/09/2025", Cliente = "Isabel Torres", Dispositivo = "Huawei P30", Estado = "Reparado", Dias = "57", MontoP = "2102" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-025", Fecha = "24/10/2025", Cliente = "Héctor Rivas", Dispositivo = "iPhone 14 Pro", Estado = "Reparado", Dias = "87", MontoP = "1886" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-026", Fecha = "25/09/2025", Cliente = "Marina Lopez", Dispositivo = "Huawei P30", Estado = "Reparado", Dias = "22", MontoP = "2174" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-027", Fecha = "30/09/2025", Cliente = "Adriana Silva", Dispositivo = "Huawei P30", Estado = "En espera", Dias = "21", MontoP = "2204" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-028", Fecha = "05/10/2025", Cliente = "Ana Rodríguez", Dispositivo = "iPad Pro 12.9-inch", Estado = "En espera", Dias = "10", MontoP = "1490" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-029", Fecha = "06/10/2025", Cliente = "Fernando Duarte", Dispositivo = "Huawei P30", Estado = "Reparado", Dias = "85", MontoP = "3108" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-030", Fecha = "09/09/2025", Cliente = "Karina Gómez", Dispositivo = "Samsung S23 Ultra", Estado = "Reparado", Dias = "40", MontoP = "3056" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-031", Fecha = "27/10/2025", Cliente = "Lorena Navarro", Dispositivo = "Samsung S24 Ultra", Estado = "En espera", Dias = "62", MontoP = "2500" } );
            dispositivos.Add( new DispositivosNR { ID = "DNR-032", Fecha = "04/10/2025", Cliente = "Daniela Flores", Dispositivo = "Google Pixel 6", Estado = "En espera", Dias = "11", MontoP = "1085" } );
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
            //dispositivos.Add( new DispositivosNR { ID = "DNR-044", Fecha = "06/10/2025", Cliente = "Karina Gómez", Dispositivo = "Huawei P40", Estado = "En espera", Dias = "80", MontoP = "3123" } );
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
            //dispositivos.Add( new DispositivosNR { ID = "DNR-061", Fecha = "25/10/2025", Cliente = "Ivan Morales", Dispositivo = "iPhone 14 Pro", Estado = "Reparado", Dias = "32", MontoP = "2984" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-062", Fecha = "20/10/2025", Cliente = "Eduardo Castillo", Dispositivo = "Huawei P30", Estado = "Reparado", Dias = "85", MontoP = "22" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-063", Fecha = "06/10/2025", Cliente = "Valeria Ruiz", Dispositivo = "iPhone 12 Mini", Estado = "En espera", Dias = "47", MontoP = "2841" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-064", Fecha = "26/09/2025", Cliente = "Daniela Flores", Dispositivo = "iPhone 12 Mini", Estado = "En espera", Dias = "51", MontoP = "882" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-065", Fecha = "03/10/2025", Cliente = "Andrés Cruz", Dispositivo = "iPhone 14", Estado = "Reparado", Dias = "37", MontoP = "2292" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-066", Fecha = "01/09/2025", Cliente = "María López", Dispositivo = "iPhone 15", Estado = "Reparado", Dias = "47", MontoP = "873" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-067", Fecha = "17/09/2025", Cliente = "Marina Lopez", Dispositivo = "Motorola Edge", Estado = "En espera", Dias = "70", MontoP = "1170" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-068", Fecha = "04/09/2025", Cliente = "Marta Ortiz", Dispositivo = "Samsung A52", Estado = "En espera", Dias = "79", MontoP = "1644" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-069", Fecha = "27/10/2025", Cliente = "Ivan Morales", Dispositivo = "Xiaomi Redmi Note 10", Estado = "En espera", Dias = "69", MontoP = "1156" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-070", Fecha = "08/10/2025", Cliente = "Luis Hernández", Dispositivo = "OnePlus 10", Estado = "En espera", Dias = "66", MontoP = "2658" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-071", Fecha = "21/09/2025", Cliente = "Gabriela Castro", Dispositivo = "iPhone 13 Pro", Estado = "Reparado", Dias = "20", MontoP = "1091" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-072", Fecha = "11/10/2025", Cliente = "Roberto Aguilar", Dispositivo = "Samsung S24 Ultra", Estado = "En espera", Dias = "16", MontoP = "1596" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-073", Fecha = "16/09/2025", Cliente = "Gabriela Castro", Dispositivo = "iPad Air", Estado = "Reparado", Dias = "63", MontoP = "637" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-074", Fecha = "02/09/2025", Cliente = "Francisco Reyes", Dispositivo = "iPhone 15 Plus", Estado = "En espera", Dias = "23", MontoP = "1586" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-075", Fecha = "21/09/2025", Cliente = "Daniela Flores", Dispositivo = "Samsung S22", Estado = "En espera", Dias = "60", MontoP = "160" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-076", Fecha = "16/10/2025", Cliente = "Patricia Vargas", Dispositivo = "Motorola Moto G Power", Estado = "En espera", Dias = "8", MontoP = "1420" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-077", Fecha = "21/10/2025", Cliente = "Elena Varela", Dispositivo = "iPhone 14 Pro Max", Estado = "En espera", Dias = "77", MontoP = "1223" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-078", Fecha = "10/10/2025", Cliente = "Miguel Sánchez", Dispositivo = "Google Pixel 6", Estado = "En espera", Dias = "6", MontoP = "1909" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-079", Fecha = "18/10/2025", Cliente = "Andrés Cruz", Dispositivo = "Samsung A13", Estado = "En espera", Dias = "33", MontoP = "2526" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-080", Fecha = "05/10/2025", Cliente = "Adriana Silva", Dispositivo = "Google Pixel 6", Estado = "En espera", Dias = "81", MontoP = "936" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-081", Fecha = "24/10/2025", Cliente = "Héctor Rivas", Dispositivo = "iPhone 14 Plus", Estado = "Reparado", Dias = "28", MontoP = "2186" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-082", Fecha = "06/09/2025", Cliente = "Lucía Fernández", Dispositivo = "Samsung S24", Estado = "Reparado", Dias = "52", MontoP = "262" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-083", Fecha = "26/09/2025", Cliente = "Marta Ortiz", Dispositivo = "iPhone 12", Estado = "Reparado", Dias = "48", MontoP = "1068" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-084", Fecha = "02/10/2025", Cliente = "Fernando Duarte", Dispositivo = "Motorola Edge", Estado = "En espera", Dias = "83", MontoP = "2347" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-085", Fecha = "10/10/2025", Cliente = "Francisco Reyes", Dispositivo = "Samsung S22", Estado = "En espera", Dias = "7", MontoP = "2379" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-086", Fecha = "16/09/2025", Cliente = "Paula Herrera", Dispositivo = "Samsung A72", Estado = "En espera", Dias = "87", MontoP = "1292" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-087", Fecha = "01/10/2025", Cliente = "Eduardo Castillo", Dispositivo = "iPhone 14 Plus", Estado = "Reparado", Dias = "42", MontoP = "2753" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-088", Fecha = "08/09/2025", Cliente = "Ricardo Salinas", Dispositivo = "iPhone 14 Pro Max", Estado = "Reparado", Dias = "72", MontoP = "806" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-089", Fecha = "05/10/2025", Cliente = "Marta Ortiz", Dispositivo = "iPad Pro 12.9-inch", Estado = "En espera", Dias = "11", MontoP = "326" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-090", Fecha = "19/09/2025", Cliente = "Andrés Cruz", Dispositivo = "iPhone 13", Estado = "Reparado", Dias = "7", MontoP = "146" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-091", Fecha = "19/09/2025", Cliente = "Raúl Pineda", Dispositivo = "Samsung A52", Estado = "En espera", Dias = "8", MontoP = "709" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-092", Fecha = "20/09/2025", Cliente = "Ricardo Salinas", Dispositivo = "Google Pixel 8", Estado = "Reparado", Dias = "67", MontoP = "429" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-093", Fecha = "16/10/2025", Cliente = "Luis Hernández", Dispositivo = "iPhone 15", Estado = "Reparado", Dias = "14", MontoP = "1451" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-094", Fecha = "17/09/2025", Cliente = "Lorena Navarro", Dispositivo = "Samsung S22 Ultra", Estado = "Reparado", Dias = "42", MontoP = "802" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-095", Fecha = "09/10/2025", Cliente = "Adriana Silva", Dispositivo = "Samsung S24 Ultra", Estado = "Reparado", Dias = "47", MontoP = "665" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-096", Fecha = "03/10/2025", Cliente = "Samuel Mendoza", Dispositivo = "Xiaomi Redmi Note 10", Estado = "Reparado", Dias = "76", MontoP = "2931" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-097", Fecha = "17/09/2025", Cliente = "Raúl Pineda", Dispositivo = "iPhone 12 Mini", Estado = "En espera", Dias = "23", MontoP = "1049" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-098", Fecha = "18/09/2025", Cliente = "Karina Gómez", Dispositivo = "Xiaomi Redmi Note 10", Estado = "En espera", Dias = "37", MontoP = "1187" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-099", Fecha = "01/09/2025", Cliente = "Claudia Mendez", Dispositivo = "Huawei P30", Estado = "En espera", Dias = "56", MontoP = "2265" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-100", Fecha = "26/09/2025", Cliente = "Raúl Pineda", Dispositivo = "iPhone 14 Plus", Estado = "Reparado", Dias = "86", MontoP = "2208" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-101", Fecha = "22/09/2025", Cliente = "Monica Rojas", Dispositivo = "Google Pixel 7", Estado = "Reparado", Dias = "14", MontoP = "2712" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-102", Fecha = "14/09/2025", Cliente = "Jorge Castillo", Dispositivo = "Samsung A13", Estado = "Reparado", Dias = "10", MontoP = "1839" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-103", Fecha = "16/09/2025", Cliente = "Camila Blanco", Dispositivo = "iPad Pro 12.9-inch", Estado = "En espera", Dias = "21", MontoP = "2146" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-104", Fecha = "06/10/2025", Cliente = "Andrés Cruz", Dispositivo = "iPhone 15 Pro", Estado = "Reparado", Dias = "59", MontoP = "603" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-105", Fecha = "06/10/2025", Cliente = "Patricia Vargas", Dispositivo = "iPhone 14 Pro", Estado = "En espera", Dias = "89", MontoP = "1622" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-106", Fecha = "04/10/2025", Cliente = "Marta Ortiz", Dispositivo = "Samsung S23 Ultra", Estado = "En espera", Dias = "44", MontoP = "21" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-107", Fecha = "08/10/2025", Cliente = "Marina Lopez", Dispositivo = "Samsung A52", Estado = "En espera", Dias = "58", MontoP = "1988" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-108", Fecha = "14/09/2025", Cliente = "Samuel Mendoza", Dispositivo = "Google Pixel 5", Estado = "Reparado", Dias = "80", MontoP = "188" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-109", Fecha = "15/10/2025", Cliente = "Isabel Torres", Dispositivo = "Google Pixel 7", Estado = "Reparado", Dias = "33", MontoP = "3076" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-110", Fecha = "10/09/2025", Cliente = "Paula Herrera", Dispositivo = "Xiaomi Redmi Note 10", Estado = "Reparado", Dias = "27", MontoP = "540" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-111", Fecha = "23/09/2025", Cliente = "Juan Perez", Dispositivo = "iPhone 14 Plus", Estado = "Reparado", Dias = "6", MontoP = "3101" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-112", Fecha = "08/09/2025", Cliente = "Eduardo Castillo", Dispositivo = "iPhone 14 Pro", Estado = "En espera", Dias = "6", MontoP = "1972" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-113", Fecha = "29/09/2025", Cliente = "Adriana Silva", Dispositivo = "Lenovo Tab M8", Estado = "Reparado", Dias = "8", MontoP = "53" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-114", Fecha = "23/10/2025", Cliente = "Jorge Castillo", Dispositivo = "Samsung S21 Ultra", Estado = "En espera", Dias = "13", MontoP = "300" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-115", Fecha = "20/10/2025", Cliente = "Karina Gómez", Dispositivo = "Samsung A72", Estado = "En espera", Dias = "65", MontoP = "3030" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-116", Fecha = "19/09/2025", Cliente = "Claudia Mendez", Dispositivo = "Samsung S24", Estado = "Reparado", Dias = "57", MontoP = "590" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-117", Fecha = "26/10/2025", Cliente = "Francisco Reyes", Dispositivo = "iPad Air", Estado = "En espera", Dias = "6", MontoP = "1891" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-118", Fecha = "15/10/2025", Cliente = "Lucía Fernández", Dispositivo = "Google Pixel 5", Estado = "En espera", Dias = "20", MontoP = "2492" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-119", Fecha = "05/10/2025", Cliente = "Luis Hernández", Dispositivo = "Google Pixel 8", Estado = "Reparado", Dias = "8", MontoP = "426" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-120", Fecha = "22/10/2025", Cliente = "Héctor Rivas", Dispositivo = "Xiaomi Redmi Note 10", Estado = "En espera", Dias = "88", MontoP = "746" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-121", Fecha = "01/10/2025", Cliente = "Valeria Ruiz", Dispositivo = "Samsung A52", Estado = "Reparado", Dias = "23", MontoP = "498" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-122", Fecha = "22/09/2025", Cliente = "Raúl Pineda", Dispositivo = "iPad Air", Estado = "Reparado", Dias = "29", MontoP = "2979" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-123", Fecha = "23/10/2025", Cliente = "Juan Perez", Dispositivo = "iPhone 15 Pro Max", Estado = "En espera", Dias = "58", MontoP = "2057" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-124", Fecha = "13/10/2025", Cliente = "Ivan Morales", Dispositivo = "Samsung S24", Estado = "En espera", Dias = "39", MontoP = "1671" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-125", Fecha = "07/09/2025", Cliente = "Isabel Torres", Dispositivo = "OnePlus 9", Estado = "Reparado", Dias = "51", MontoP = "1814" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-126", Fecha = "02/09/2025", Cliente = "Daniela Flores", Dispositivo = "iPhone 13 Pro", Estado = "En espera", Dias = "89", MontoP = "2233" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-127", Fecha = "14/09/2025", Cliente = "Claudia Mendez", Dispositivo = "Samsung A72", Estado = "En espera", Dias = "21", MontoP = "171" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-128", Fecha = "11/09/2025", Cliente = "Pedro Ramos", Dispositivo = "iPhone 15", Estado = "En espera", Dias = "77", MontoP = "1795" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-129", Fecha = "04/09/2025", Cliente = "Héctor Rivas", Dispositivo = "Samsung A13", Estado = "En espera", Dias = "48", MontoP = "1692" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-130", Fecha = "09/10/2025", Cliente = "Elena Varela", Dispositivo = "iPad Pro 12.9-inch", Estado = "En espera", Dias = "59", MontoP = "326" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-131", Fecha = "23/09/2025", Cliente = "Patricia Vargas", Dispositivo = "Google Pixel 6", Estado = "En espera", Dias = "78", MontoP = "2194" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-132", Fecha = "12/09/2025", Cliente = "Adriana Silva", Dispositivo = "Samsung S20", Estado = "En espera", Dias = "66", MontoP = "1419" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-133", Fecha = "10/10/2025", Cliente = "Daniela Flores", Dispositivo = "Xiaomi Mi 11", Estado = "En espera", Dias = "28", MontoP = "1387" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-134", Fecha = "10/09/2025", Cliente = "Jorge Castillo", Dispositivo = "Samsung Tab A8", Estado = "En espera", Dias = "63", MontoP = "3235" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-135", Fecha = "28/10/2025", Cliente = "Ivan Morales", Dispositivo = "iPad Mini", Estado = "En espera", Dias = "33", MontoP = "2810" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-136", Fecha = "02/10/2025", Cliente = "Francisco Reyes", Dispositivo = "Samsung Tab A8", Estado = "En espera", Dias = "60", MontoP = "1666" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-137", Fecha = "07/10/2025", Cliente = "Jorge Castillo", Dispositivo = "iPhone 12 Mini", Estado = "En espera", Dias = "61", MontoP = "3113" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-138", Fecha = "03/09/2025", Cliente = "Patricia Vargas", Dispositivo = "OnePlus 10", Estado = "Reparado", Dias = "23", MontoP = "1528" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-139", Fecha = "01/09/2025", Cliente = "Diego Morales", Dispositivo = "iPhone 12 Mini", Estado = "En espera", Dias = "55", MontoP = "2053" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-140", Fecha = "24/09/2025", Cliente = "Diego Morales", Dispositivo = "Samsung S22 Ultra", Estado = "En espera", Dias = "48", MontoP = "1729" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-141", Fecha = "02/10/2025", Cliente = "Diego Morales", Dispositivo = "Samsung Tab A8", Estado = "Reparado", Dias = "34", MontoP = "936" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-142", Fecha = "30/09/2025", Cliente = "Tomás Herrera", Dispositivo = "iPhone 15 Pro", Estado = "Reparado", Dias = "64", MontoP = "568" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-143", Fecha = "18/09/2025", Cliente = "Monica Rojas", Dispositivo = "Google Pixel 8", Estado = "En espera", Dias = "83", MontoP = "930" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-144", Fecha = "12/10/2025", Cliente = "María López", Dispositivo = "iPhone 14 Plus", Estado = "Reparado", Dias = "22", MontoP = "502" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-145", Fecha = "16/09/2025", Cliente = "Adriana Silva", Dispositivo = "Motorola Moto G Power", Estado = "En espera", Dias = "79", MontoP = "1360" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-146", Fecha = "05/10/2025", Cliente = "Francisco Reyes", Dispositivo = "iPad Mini", Estado = "En espera", Dias = "16", MontoP = "1746" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-147", Fecha = "26/09/2025", Cliente = "Tomás Herrera", Dispositivo = "iPad Mini", Estado = "Reparado", Dias = "25", MontoP = "2628" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-148", Fecha = "23/09/2025", Cliente = "Natalia Serrano", Dispositivo = "Samsung S23 Ultra", Estado = "Reparado", Dias = "20", MontoP = "2638" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-149", Fecha = "06/10/2025", Cliente = "Karina Gómez", Dispositivo = "Samsung S22 Ultra", Estado = "Reparado", Dias = "69", MontoP = "2320" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-150", Fecha = "01/10/2025", Cliente = "María López", Dispositivo = "iPad Pro 12.9-inch", Estado = "En espera", Dias = "67", MontoP = "1362" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-151", Fecha = "01/09/2025", Cliente = "Héctor Rivas", Dispositivo = "Google Pixel 7", Estado = "Reparado", Dias = "51", MontoP = "2493" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-152", Fecha = "02/10/2025", Cliente = "María López", Dispositivo = "iPhone 13 Pro Max", Estado = "Reparado", Dias = "33", MontoP = "1482" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-153", Fecha = "08/10/2025", Cliente = "Tomás Herrera", Dispositivo = "Samsung Tab A8", Estado = "Reparado", Dias = "9", MontoP = "2784" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-154", Fecha = "06/10/2025", Cliente = "Luis Hernández", Dispositivo = "Huawei P30", Estado = "Reparado", Dias = "46", MontoP = "895" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-155", Fecha = "25/10/2025", Cliente = "Héctor Rivas", Dispositivo = "Google Pixel 8", Estado = "Reparado", Dias = "80", MontoP = "442" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-156", Fecha = "26/09/2025", Cliente = "Marina Lopez", Dispositivo = "Samsung S21 Ultra", Estado = "Reparado", Dias = "22", MontoP = "1722" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-157", Fecha = "25/09/2025", Cliente = "Monica Rojas", Dispositivo = "Samsung S22 Ultra", Estado = "Reparado", Dias = "58", MontoP = "1515" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-158", Fecha = "17/09/2025", Cliente = "Francisco Reyes", Dispositivo = "iPad Air", Estado = "En espera", Dias = "59", MontoP = "2987" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-159", Fecha = "03/10/2025", Cliente = "Daniela Flores", Dispositivo = "iPad Pro 11-inch", Estado = "En espera", Dias = "15", MontoP = "1011" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-160", Fecha = "23/10/2025", Cliente = "Isabel Torres", Dispositivo = "Xiaomi Mi 11", Estado = "Reparado", Dias = "82", MontoP = "1390" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-161", Fecha = "06/10/2025", Cliente = "Elena Varela", Dispositivo = "iPhone 12", Estado = "En espera", Dias = "59", MontoP = "1053" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-162", Fecha = "01/10/2025", Cliente = "Jorge Castillo", Dispositivo = "iPad Pro 12.9-inch", Estado = "En espera", Dias = "32", MontoP = "2235" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-163", Fecha = "01/09/2025", Cliente = "Juan Perez", Dispositivo = "OnePlus 9", Estado = "En espera", Dias = "22", MontoP = "652" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-164", Fecha = "24/09/2025", Cliente = "Daniela Flores", Dispositivo = "Samsung S24 Ultra", Estado = "Reparado", Dias = "62", MontoP = "3236" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-165", Fecha = "11/09/2025", Cliente = "Francisco Reyes", Dispositivo = "Samsung S24", Estado = "Reparado", Dias = "77", MontoP = "2322" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-166", Fecha = "24/10/2025", Cliente = "Luis Hernández", Dispositivo = "iPhone 14 Plus", Estado = "Reparado", Dias = "9", MontoP = "2759" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-167", Fecha = "21/09/2025", Cliente = "Héctor Rivas", Dispositivo = "iPhone 13 Pro", Estado = "Reparado", Dias = "36", MontoP = "2080" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-168", Fecha = "28/09/2025", Cliente = "Sergio Jimenez", Dispositivo = "iPhone 14 Pro", Estado = "En espera", Dias = "24", MontoP = "2714" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-169", Fecha = "18/10/2025", Cliente = "Raúl Pineda", Dispositivo = "Samsung S21", Estado = "Reparado", Dias = "8", MontoP = "1343" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-170", Fecha = "11/10/2025", Cliente = "Monica Rojas", Dispositivo = "iPhone 14 Plus", Estado = "En espera", Dias = "5", MontoP = "812" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-171", Fecha = "06/10/2025", Cliente = "Lorena Navarro", Dispositivo = "iPhone 12 Mini", Estado = "En espera", Dias = "51", MontoP = "2565" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-172", Fecha = "01/09/2025", Cliente = "Tomás Herrera", Dispositivo = "Samsung A52", Estado = "En espera", Dias = "6", MontoP = "2096" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-173", Fecha = "15/09/2025", Cliente = "Luis Hernández", Dispositivo = "Google Pixel 8", Estado = "Reparado", Dias = "55", MontoP = "3090" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-174", Fecha = "07/10/2025", Cliente = "Jorge Castillo", Dispositivo = "OnePlus 9", Estado = "En espera", Dias = "60", MontoP = "380" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-175", Fecha = "15/10/2025", Cliente = "Roberto Aguilar", Dispositivo = "iPhone 13 Pro Max", Estado = "En espera", Dias = "73", MontoP = "2843" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-176", Fecha = "01/09/2025", Cliente = "Marina Lopez", Dispositivo = "Google Pixel 8", Estado = "En espera", Dias = "7", MontoP = "805" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-177", Fecha = "23/10/2025", Cliente = "Fernando Duarte", Dispositivo = "OnePlus 10", Estado = "Reparado", Dias = "45", MontoP = "2241" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-178", Fecha = "05/10/2025", Cliente = "Adriana Silva", Dispositivo = "Samsung S21 Ultra", Estado = "Reparado", Dias = "67", MontoP = "3300" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-179", Fecha = "24/09/2025", Cliente = "Ana Rodríguez", Dispositivo = "Samsung S24", Estado = "En espera", Dias = "28", MontoP = "2483" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-180", Fecha = "12/09/2025", Cliente = "Sofía Martínez", Dispositivo = "iPad Pro 11-inch", Estado = "En espera", Dias = "55", MontoP = "44" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-181", Fecha = "20/10/2025", Cliente = "Ricardo Salinas", Dispositivo = "Samsung S24", Estado = "En espera", Dias = "14", MontoP = "214" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-182", Fecha = "13/09/2025", Cliente = "Lucía Fernández", Dispositivo = "Lenovo Tab M8", Estado = "Reparado", Dias = "34", MontoP = "3098" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-183", Fecha = "20/09/2025", Cliente = "Juan Perez", Dispositivo = "iPad Pro 11-inch", Estado = "Reparado", Dias = "87", MontoP = "758" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-184", Fecha = "16/10/2025", Cliente = "Héctor Rivas", Dispositivo = "iPhone 13 Pro Max", Estado = "Reparado", Dias = "7", MontoP = "3174" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-185", Fecha = "19/10/2025", Cliente = "Francisco Reyes", Dispositivo = "iPhone 14", Estado = "En espera", Dias = "30", MontoP = "1076" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-186", Fecha = "09/10/2025", Cliente = "Samuel Mendoza", Dispositivo = "OnePlus 9", Estado = "Reparado", Dias = "59", MontoP = "505" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-187", Fecha = "26/09/2025", Cliente = "Gabriela Castro", Dispositivo = "Samsung A72", Estado = "En espera", Dias = "64", MontoP = "1001" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-188", Fecha = "22/10/2025", Cliente = "Lorena Navarro", Dispositivo = "Google Pixel 8", Estado = "En espera", Dias = "81", MontoP = "1610" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-189", Fecha = "24/10/2025", Cliente = "Natalia Serrano", Dispositivo = "Lenovo Tab M8", Estado = "Reparado", Dias = "6", MontoP = "1358" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-190", Fecha = "15/09/2025", Cliente = "Eduardo Castillo", Dispositivo = "Samsung S20", Estado = "En espera", Dias = "88", MontoP = "559" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-191", Fecha = "04/10/2025", Cliente = "Gabriela Castro", Dispositivo = "Samsung Tab A8", Estado = "En espera", Dias = "16", MontoP = "1586" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-192", Fecha = "04/10/2025", Cliente = "Eduardo Castillo", Dispositivo = "Samsung A13", Estado = "Reparado", Dias = "70", MontoP = "2184" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-193", Fecha = "07/10/2025", Cliente = "Héctor Rivas", Dispositivo = "Samsung S24", Estado = "En espera", Dias = "25", MontoP = "1371" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-194", Fecha = "22/09/2025", Cliente = "Patricia Vargas", Dispositivo = "iPhone 15 Plus", Estado = "En espera", Dias = "20", MontoP = "1473" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-195", Fecha = "25/09/2025", Cliente = "Patricia Vargas", Dispositivo = "Samsung A13", Estado = "Reparado", Dias = "50", MontoP = "3228" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-196", Fecha = "08/09/2025", Cliente = "Tomás Herrera", Dispositivo = "Lenovo Tab M8", Estado = "En espera", Dias = "14", MontoP = "2121" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-197", Fecha = "04/09/2025", Cliente = "Elena Varela", Dispositivo = "Google Pixel 6", Estado = "Reparado", Dias = "25", MontoP = "1242" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-198", Fecha = "25/09/2025", Cliente = "Fernando Duarte", Dispositivo = "Samsung S21 Ultra", Estado = "Reparado", Dias = "43", MontoP = "517" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-199", Fecha = "08/10/2025", Cliente = "Tomás Herrera", Dispositivo = "iPhone 15", Estado = "Reparado", Dias = "50", MontoP = "304" } );
            //dispositivos.Add( new DispositivosNR { ID = "DNR-200", Fecha = "03/09/2025", Cliente = "Isabel Torres", Dispositivo = "Huawei P30", Estado = "Reparado", Dias = "11", MontoP = "365" } );
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
                                Padding = 5
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
                
            }
        }
    }
}
