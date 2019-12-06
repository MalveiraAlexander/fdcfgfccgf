using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Estacionamiento
{
    public partial class Form1 : Form, IParkingLot
    {
        public Form1()
        {
            InitializeComponent();
            _EspaciosDisponibles = _CapacidadTotal;
            
        }



        private void button1_Click(object sender, EventArgs e)
        {
            IngresoDetectado(patente.Text);
            todosAutos.DataSource = new BindingSource(automoviles, null);
        }

        Dictionary<string, Auto> automoviles = new Dictionary<string, Auto>();

        public void IngresoDetectado(string _patente)
        {
            if (_EspaciosDisponibles != 0)
            {
                if (!automoviles.ContainsKey(_patente))
                {
                    automoviles.Add(_patente, new Auto { patente = _patente, totalFacturado = 0, totalHoras = 0, date = DateTime.Now.Date, time = DateTime.Now.TimeOfDay });
                }
                else
                {
                    automoviles.Remove(_patente);
                    automoviles.Add(_patente, new Auto { patente = _patente, totalFacturado = 0, totalHoras = 0, date = DateTime.Now.Date, time = DateTime.Now.TimeOfDay });
                }
                _CantidadEstacionados++;
                _EspaciosDisponibles--;
            }
            else
            {
                MessageBox.Show("No quedan mas espacios disponibles!", "Error faltan espacios", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        public void EgresoDetectado(string patente)
        {
            Auto auto = new Auto();
            
            if (automoviles.TryGetValue(patente, out auto))
            {
                automoviles.Remove(patente);
                if (DateTime.Now.Date.Equals(auto.date))
                {
                    TimeSpan ts = DateTime.Now.TimeOfDay - auto.time;
                    auto.totalFacturado = Math.Round(Math.Abs(ts.TotalHours) * PrecioPorHora);
                }
                else
                {
                    TimeSpan ts = DateTime.Now.Date - auto.date;
                    auto.totalFacturado = Math.Round((Math.Abs(ts.TotalHours)*24)* PrecioPorHora);
                }
                label2.Text = "Ingreso el dia: " + auto.date.ToShortDateString() + " a lo hora: " + auto.time.ToString(@"hh\:mm\:ss") + ". Debe: $" + auto.totalFacturado;
            }
            else
            {
                MessageBox.Show("No se a encontrado un vehiculo con esa patente!", "Error con patente: " + patente, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.patente.Text = "";
                this.patente.Focus();
            }
            _EspaciosDisponibles++;
            _CantidadEstacionados--;
            _TotalFacturado = _TotalFacturado + auto.totalFacturado;
            total.Text = _TotalFacturado.ToString();
        }

        public IList<string> VehiculoEstacionados()
        {
            return null;
        }

        

        private int _PrecioPorHora = 115;

        public int PrecioPorHora
        {
            get { return _PrecioPorHora; }
            set { _PrecioPorHora = value; }
        }

        private int _CapacidadTotal = 3;

        public int CapacidadTotal
        {
            get { return _CapacidadTotal; }
            set { _CapacidadTotal = value; }
        }

        private int _CantidadEstacionados;

        public int CantidadEstacionados
        {
            get { return _CantidadEstacionados; }
        }

        private int _EspaciosDisponibles;

        public int EspaciosDisponibles
        {
            get { return _EspaciosDisponibles; }
        }

        private double _TotalFacturado;

        public double TotalFacturado
        {
            get { return _TotalFacturado; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EgresoDetectado(patente.Text);
            todosAutos.DataSource = new BindingSource(automoviles, null);
        }
    }
}
