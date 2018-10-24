using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PrestamoRepositorio : Repositorio<Prestamos>
    {
        public override bool Save(Prestamos prestamo)
        {
            bool step = false;
            Contexto contexto = new Contexto();
            decimal monto = 0;
            try
            {
                foreach (var item in prestamo.Detalle)
                {
                    monto = item.Capital + item.Interes;
                }
                contexto.Cuentas.Find(prestamo.CuentaId).Balance += monto;

                if (contexto.Prestamos.Add(prestamo) != null)
                {
                    contexto.SaveChanges();
                    step = true;
                }

            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return step;
        }

        public override bool Modify(Prestamos prestamo)
        {
            bool step = false;
            Contexto contexto = new Contexto();
            decimal montoDB = 0;
            decimal monto = 0;
            try
            {
                var PrestamoAnt = contexto.Cuotas.Where(c => c.PrestamoId == prestamo.PrestamoId).ToList();

                foreach (var item in PrestamoAnt)
                {
                    montoDB += item.Capital + item.Interes;
                }

                foreach (var item in prestamo.Detalle)
                {
                    monto += item.Capital + item.Interes;
                }

                contexto.Cuentas.Find(prestamo.CuentaId).Balance -= montoDB;
                contexto.Cuentas.Find(prestamo.CuentaId).Balance += monto;

                if (prestamo.Detalle.Count < PrestamoAnt.Count)
                {
                    foreach (var item in PrestamoAnt)
                    {
                        if (!prestamo.Detalle.Exists(x => x.Id.Equals(item.Id)))
                        {
                            contexto.Entry(item).State = EntityState.Deleted;
                        }
                    }
                }

                foreach (var item in prestamo.Detalle)
                {
                    contexto.Entry(item).State = item.Id == 0 ? EntityState.Added : EntityState.Modified;
                }

                contexto.Entry(prestamo).State = EntityState.Modified;
                if (contexto.SaveChanges() > 0)
                    step = true;

            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }


            return step;
        }

        public override bool Delete(int id)
        {
            bool step = false;
            decimal monto = 0;
            Contexto contexto = new Contexto();

            try
            {
                var prestamo = contexto.Prestamos.Find(id);
                foreach (var item in prestamo.Detalle)
                {
                    monto += item.Capital + item.Interes;
                }

                contexto.Cuentas.Find(prestamo.CuentaId).Balance -= monto;
                contexto.Prestamos.Remove(prestamo);

                if (contexto.SaveChanges() > 0)
                {
                    step = true;
                }

            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return step;
        }

        public List<PrestamosDetalles> CalcularCuotas(int meses, double montoCapital, double interes)
        {
            List<PrestamosDetalles> list = new List<PrestamosDetalles>();
            decimal intere = 0;
            decimal valor = Convert.ToDecimal((montoCapital * interes) / (1 - Math.Pow((1 + interes), -meses)));
            decimal capital = 0;
            decimal balance = 0;

            for (int i = 0; i < meses; i++)
            {
                if (i == 0)
                    intere = Convert.ToDecimal(interes * montoCapital);
                else
                    intere = balance * Convert.ToDecimal(interes);

                capital = valor - intere;

                if (i == 0)
                    balance = Convert.ToDecimal(montoCapital) - capital;
                else
                    balance -= capital;

                if (balance < 0)
                    balance = 0;

                list.Add(new PrestamosDetalles(0, 0, i + 1, decimal.Round(intere, 2), decimal.Round(capital, 2), decimal.Round(valor, 2), decimal.Round(balance, 2)));

            }
            return list;
        }

        public List<PrestamosDetalles> CalcularCuotasModificadas(List<PrestamosDetalles> prestamo, int prestamoId, int meses, double montoCapital, double interes)
        {
            List<PrestamosDetalles> list = new List<PrestamosDetalles>();

            int inicio = list.ElementAt(0).Id, k = 0;
            decimal intere = 0;
            decimal valor = Convert.ToDecimal((montoCapital * interes) / (1 - Math.Pow((1 + interes), -meses)));
            decimal capital = 0;
            decimal balance = 0;

            for (int i = 0; i < meses; i++)
            {
                if (i == 0)
                    intere = Convert.ToDecimal(interes * montoCapital);
                else
                    intere = balance * Convert.ToDecimal(interes);

                capital = valor - intere;

                if (i == 0)
                    balance = Convert.ToDecimal(montoCapital) - capital;
                else
                    balance -= capital;

                if (balance < 0)
                    balance = 0;

                if (k == list.Count)
                    list.Add(new PrestamosDetalles(0, prestamoId, i + 1, decimal.Round(intere, 2), decimal.Round(capital, 2), decimal.Round(valor, 2), decimal.Round(balance, 2)));
                else
                    list.Add(new PrestamosDetalles(inicio, prestamoId, i + 1, decimal.Round(intere, 2), decimal.Round(capital, 2), decimal.Round(valor, 2), decimal.Round(balance, 2)));
                inicio += 1;
                k += 1;
            }
            return list;
        }

        public override Prestamos Search(int id)
        {
            return base.Search(id);
        }
    }
}
