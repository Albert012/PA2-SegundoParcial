using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Serializable]
    public class PrestamosDetalles
    {
        [Key]
        public int Id { get; set; }
        public int PrestamoId { get; set; }
        public int NoCuota { get; set; }
        public decimal Interes { get; set; }
        public decimal Capital { get; set; }
        public decimal ValorPrestamo { get; set; }
        public decimal Balance { get; set; }

        public PrestamosDetalles()
        {
            Id = 0;
            NoCuota = 0;
            Interes = 0;
            Capital = 0;
            ValorPrestamo = 0;
            Balance = 0;
        }

        public PrestamosDetalles(int id, int prestamoId, int noCuota, decimal interes, decimal capital, decimal valorPrestamo, decimal balance)//, Cuentas cuenta)
        {
            Id = id;
            PrestamoId = prestamoId;
            NoCuota = noCuota;
            Interes = interes;
            Capital = capital;
            ValorPrestamo = valorPrestamo;
            Balance = balance;
            //Cuenta = cuenta;
        }
    }
}
