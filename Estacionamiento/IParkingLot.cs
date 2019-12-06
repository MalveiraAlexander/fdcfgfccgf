using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamiento
{
    public interface IParkingLot
    {

        public int PrecioPorHora
        { 
            get; 
            set; 
        }

        public int CapacidadTotal
        {
            get;
            set;
        }

        public int CantidadEstacionados
        {
            get;
        }

        public int EspaciosDisponibles
        {
            get;
        }

        public double TotalFacturado
        {
            get;
        }

        void IngresoDetectado(string patente);
        void EgresoDetectado(string patente);
        IList<string> VehiculoEstacionados();

    }
}
