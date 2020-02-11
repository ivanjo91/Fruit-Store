using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPV_Frutas
{
    public partial class Form1 : Form
    {

        String[] nombres = { "chirimoya", "ciruela", "fresa", "kiwi", "mandarina", "melocoton", "melon", "naranja", "nectarina", "papaya", "peras", "piña", "peras", "piña", "platanos", "pomelos", "prunus", "sandias" };
        String[] precios = { "1,25", "2,75", "3,25", "1,50", "2,75", "3,25", "1", "2", "3", "1", "2", "3", "1", "2", "3", "1", "2", "3" };
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            for(int i=0; i<nombres.Length; i++)
            {

                TableLayoutPanel panel = new TableLayoutPanel();
                panel.ColumnCount = 1;
                panel.RowCount = 2;
                panel.AutoSize = true;

                
                Button botonX = new Button();
                botonX.Tag = i;
                botonX.Size = new Size(80, 80);
                botonX.BackgroundImage = Image.FromFile(@"imagenes\" + nombres[i] + ".PNG");
                botonX.BackgroundImageLayout = ImageLayout.Zoom;

                botonX.Click += new EventHandler(clickFruta);
                
                
                panel.Controls.Add(botonX);
                

                TextBox textbox = new TextBox();
                textbox.Tag = i;
                textbox.Width = 80;
                textbox.Text = precios[i];
                textbox.Enabled = false;

                panel.Controls.Add(textbox);

                tableLayoutPanel1.Controls.Add(panel);
               
            }

            calcularTotal();
                        
        }

        private void clickFruta(object sender, EventArgs e)
        {
            Button botonX = (Button)sender;
            
            String cadena = Interaction.InputBox("Introduce cantidad", "TPV Fruta");
            //Reemplazar punto por coma
            cadena = cadena.Replace('.', ',');           

            try
            {
                float cantidad = float.Parse(cadena);
                float precio = float.Parse(precios[Convert.ToInt32(botonX.Tag)]);
                float totalParcial = cantidad * precio;

                //Añadir fila
                dataGridView1.Rows.Add();
                //Obtener posición de la fila
                int indiceFila = dataGridView1.RowCount - 1;
                //Añadir nombre de fruta
                dataGridView1.Rows[indiceFila].Cells[0].Value = nombres[Convert.ToInt32(botonX.Tag)];
                //Añadir precio
                dataGridView1.Rows[indiceFila].Cells[1].Value = precios[Convert.ToInt32(botonX.Tag)];
                //Añadir cantidad
                dataGridView1.Rows[indiceFila].Cells[2].Value = cantidad;
                //Añadir total
                dataGridView1.Rows[indiceFila].Cells[3].Value = totalParcial;

                calcularTotal();
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Cantidad introducida no válida");
            }
            
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals("frutas"))
            {
                foreach(Control ctrl in tableLayoutPanel1.Controls)
                {
                    foreach(Control ctrl2 in ctrl.Controls)
                    {
                        ctrl2.Enabled = true;
                    }
                }
                btnModificar.Enabled = true;
            }
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            label2.Visible = true;
            textBox1.Visible = true;
            textBox1.Enabled = true;
            btnModificar.Visible = true;
            textBox1.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            calcularTotal();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in tableLayoutPanel1.Controls)
            {
                foreach (Control ctrl2 in ctrl.Controls)
                {
                    if(ctrl2 is TextBox)
                    {
                        precios[Convert.ToInt32(ctrl2.Tag)] = ctrl2.Text;
                        ctrl2.Enabled = false;
                    }
                }
            }

            label2.Visible = false;
            textBox1.Text = "";
            textBox1.Visible = false;
            textBox1.Enabled = false;
            btnModificar.Visible = false;
            btnModificar.Enabled = false;

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
            calcularTotal();
        }

        private void calcularTotal()
        {
            float total = 0;

            for(int i=0; i<dataGridView1.RowCount; i++)
            {
                total += float.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
            }

            label1.Text = total + "€";
        }
    }
}
