using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Autonumeracion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private long num = 0; //Declaro una variable privada num, la cual se usara para la cantidad de paginas finales.

        private void Button1_Click(object sender, EventArgs e)
        {
            long pagini = Convert.ToInt64(numericUpDown2.Text);     //Declaro e igualo la variable pagina inicial.
            long pagfnl = Convert.ToInt64(numericUpDown1.Text);     //Declaro e igualo la variable pagina final.
            long intervalos = Convert.ToInt64(numericUpDown3.Text); //Declaro e igualo la variable intervalos.

            if (radioButton9.Checked == false && radioButton10.Checked == false && radioButton11.Checked == false) //Verifico que se seleccione un tipo de formulario.
            {
                MessageBox.Show("Debes seleccionar un tipo de formulario, para poder imprimir.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }

            else if ((num + pagfnl) == 0) //Verifico que la pagina inicial no se 0.
            {
                MessageBox.Show("La pagina inicial no puede ser CERO.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }

            else if ((num + pagfnl) > pagini) //Verifico que la pagina final no se menor a la pagina inicial.
            {
                MessageBox.Show("La pagina inicial no puede ser mayor a la pagina final.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }

            else if (intervalos == 0) //Verifico que intervalos no sea 0.
            {
                MessageBox.Show("El intervalo no puede ser CERO.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }

            else if (radioButton10.Checked == true) //Si el Formulario es el N° 11. Uso Hoja Tamaño Legal.
            {
                printDocument1 = new PrintDocument();
                PrinterSettings ps = new PrinterSettings();
                printDocument1.PrinterSettings = ps;
                printDocument1.PrintPage += printDocument1_PrintPage;
                PaperSize legal = new PaperSize();
                legal.RawKind = 5;
                printDocument1.DefaultPageSettings.PaperSize = legal;    
                printDocument1.Print();
            }
            else
            {
                printDocument1 = new PrintDocument();
                PrinterSettings ps = new PrinterSettings();
                printDocument1.PrinterSettings = ps;
                printDocument1.PrintPage += printDocument1_PrintPage;
                printDocument1.Print();
            }
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                Graphics graphic = e.Graphics;
                SolidBrush brush = new SolidBrush(Color.Black);

                Font font = new Font("Times New Roman", 12, FontStyle.Bold);
                Font fon2 = new Font("Courier New", 22, FontStyle.Bold);

                float pageWidth = e.PageSettings.PrintableArea.Width;
                float pageHeight = e.PageSettings.PrintableArea.Height;

                float fontHeight = font.GetHeight();

                int startXI = 63;     //Posicion Izquierda.
                int startXC = 180;    //Posicion Centro.
                int startXD = 700;    //Posicion Derecha.

                int startY  = 95;     //Valor inicial de Y.
                int startY2 = 35;     //Valor inicial de Y2.
                int startY3 = 190;    //Valor inicial de Y3.

                int offsetY2 = 10;    //Posicion Superior Vertical y Orizontal.

                long pagini = Convert.ToInt64(numericUpDown2.Text);
                long pagfnl = Convert.ToInt64(numericUpDown1.Text);
                long intervalos = Convert.ToInt64(numericUpDown3.Text);
                int contador = 0;                

                if (radioButton11.Checked == true) //Imprimo Formulario N° 11.
                {
                    int Numero = 0;    
                    while ((num + pagfnl) <= pagini)
                    {
                        
                        var Valor1 = (num + pagfnl);
                        var Valor2 = (Valor1 + 1);   

                        var Pagina1 = Valor1.ToString("D6"); //Relleno con ceros a la izquierda.
                        var Pagina2 = Valor2.ToString("D6"); //Relleno con ceros a la izquierda.

                        graphic.DrawString(Pagina1, fon2, brush, startXI, startY3 + offsetY2);        
                        graphic.DrawString(Pagina1, fon2, brush, (startXD - 455), startY3 + offsetY2);
                        graphic.DrawString(Pagina2, fon2, brush, (startXI + 555), startY3 + offsetY2);
                        graphic.DrawString(Pagina2, fon2, brush, (startXD - 255), startY3 + offsetY2);
                        
                        Numero = Numero + 1;

                        if (((Numero % 5) == 0) || ((Numero % 5) == 5))
                        {
                            offsetY2 += ((int)fontHeight + 300); //Espacios entre ambos numeros.
                        }
                        else
                        {
                            offsetY2 += ((int)fontHeight + 170); //Espacios entre ambos numeros.
                            num = num + 1;
                        }

                        if (offsetY2 >= pageHeight)
                        {
                            e.HasMorePages = true;
                            offsetY2 = 0;
                            return;
                        }
                        else
                        {
                            e.HasMorePages = false;
                        }
                        num = num + intervalos;
                    }
                    pagini = 0;
                    pagfnl = 0;
                    intervalos = 0;
                    contador = 0;
                    num = 0;
                }

                else if (radioButton10.Checked == true) //Imprimo Formulario N° 2.
                {
                    while ((num + pagfnl) <= pagini)
                    {
                        contador = contador + 1;

                        RectangleF rectF1 = new RectangleF(157, 30, 120, 45);  //Arriba-Izquierda.
                        RectangleF rectF2 = new RectangleF(667, 30, 120, 45);  //Arriba-Derecha.
                        RectangleF rectF3 = new RectangleF(157, 655, 120, 45); //Abajo-Izquierda.
                        RectangleF rectF4 = new RectangleF(667, 655, 120, 45); //Abajo-Derecha.
                        
                        Image newImage = Autonumeracion.Properties.Resources.Borde1;

                        e.Graphics.DrawImage(newImage, rectF1); //Imagen-Arriba-Izquierda.
                        e.Graphics.DrawImage(newImage, rectF2); //Imagen-Arriba-Derecha.

                        graphic.DrawString("N° " + (num + pagfnl), font, brush, startXC, startY2 + offsetY2);
                        graphic.DrawString("N° " + (num + pagfnl), font, brush, startXD, startY2 + offsetY2);
                        
                        if ((contador % 2) == 0)
                        {
                            offsetY2 += ((int)fontHeight + 805);    //Espacios entre ambos numeros. 
                            e.Graphics.DrawImage(newImage, rectF3); //Imagen-Abajo-Izquierda.
                            e.Graphics.DrawImage(newImage, rectF4); //Imagen-Abajo-Derecha.
                        }
                        else
                        {
                            offsetY2 += ((int)fontHeight + 605); //Espacios entre ambos numeros.
                        }

                        if (offsetY2 >= (pageHeight + 605))
                        {
                            e.HasMorePages = true;
                            offsetY2 = 0;
                            return;
                        }
                        else
                        {
                            e.HasMorePages = false;
                        }
                        num = num + intervalos;
                    }
                    pagini = 0;
                    pagfnl = 0;
                    intervalos = 0;
                    contador = 0;
                    num = 0;
                }

                else if (radioButton9.Checked == true) //Imprimo Formulario N° 3.
                {
                    while ((num + pagfnl) <= pagini)
                    {
                        contador = contador + 1;
                        graphic.DrawString("N° " + (num + pagfnl), font, brush, startXD, startY + offsetY2);
                        
                        if ((contador % 3) == 0)
                        {
                            offsetY2 += ((int)fontHeight + 490); //Espacios entre ambos numeros.
                        }
                        else
                        {
                            offsetY2 += ((int)fontHeight + 420); //Espacios entre ambos numeros.    
                        }

                        if (offsetY2 >= (pageHeight + 420))
                        {
                            e.HasMorePages = true;
                            offsetY2 = 0;
                            return;
                        }
                        else
                        {
                            e.HasMorePages = false;
                            
                        }
                        num = num + intervalos;
                    }
                    pagini = 0;
                    pagfnl = 0;
                    intervalos = 0;
                    contador = 0;
                    num = 0;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = Autonumeracion.Properties.Resources.A4___1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = Autonumeracion.Properties.Resources.A4___2;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = Autonumeracion.Properties.Resources.A4___3;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = Autonumeracion.Properties.Resources.A4___4;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = Autonumeracion.Properties.Resources.A4___5;
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = Autonumeracion.Properties.Resources.A4___6;
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = Autonumeracion.Properties.Resources.Hoja3;
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = Autonumeracion.Properties.Resources.Hoja11;
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = Autonumeracion.Properties.Resources.Hoja2;
        }

        private void printDocument1_PrintPage_1(object sender, PrintPageEventArgs e)
        {

        }

    }
}