using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamiento
{
    public class Auto
    {
        public string patente { get; set; }
        public double totalFacturado { get; set; }
        public int totalHoras { get; set; }
        public DateTime date { get; set; }
        public TimeSpan time { get; set; }

        public override string ToString()
        {
            return " | Ingreso la fecha: " + date.ToShortDateString() + " | A la hora: " + time.ToString(@"hh\:mm\:ss");
        }

    }

    
}
