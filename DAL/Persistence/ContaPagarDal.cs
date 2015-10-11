using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entity;
using System.Data.Entity;

namespace DAL.Persistence
{
    public class ContaPagarDal
    {
        public void Insert(ContaPagar c)
        {
            try
            {
                using (Conexao Con = new Conexao()) 
                {
                    Con.ContasPagar.Add(c);
                    Con.SaveChanges();
                }
            }
            catch
            {
                
                throw;
            }
        
        }

        public ContaPagar FindById(int IdContaPagar)
        {
            try
            {
                using (Conexao Con = new Conexao())
                {
                    return Con.ContasPagar.Find(IdContaPagar);
                }

            }

            catch
            {
                throw;
            }


        }

        public List<ContaPagar> FindByUsuario(int IdUsuario)
        {
            try
            {
                using (Conexao Con = new Conexao())
                {
                    return Con.ContasPagar.Where(c => c.Usuario.IdUsuario.Equals(IdUsuario)).ToList(); ;
                }

            }

            catch
            {
                throw;
            }


        }
        public void Delete(int IdContaPagar) 
        {
            try
            {
                using (Conexao Con = new Conexao())
                {
                    ContaPagar c = Con.ContasPagar.Find(IdContaPagar);
                    Con.ContasPagar.Remove(c);
                    Con.SaveChanges();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        
        }

        public void Update(ContaPagar c)
        {
            try
            {
                using (Conexao Con = new Conexao())
                {

                    Con.Entry(c).State = EntityState.Modified;
                    Con.SaveChanges();
                }

            }
            catch
            {

                throw;
            }
        }

        public List<ContaPagar> FindByRelatorio(DateTime DataInicio, DateTime DataFim, int IdUsuario)
        {
            try
            {
                using (Conexao Con = new Conexao())
                {
                    if (DataInicio != DateTime.MinValue
                        && DataFim != DateTime.MinValue)
                    {
                        return Con.ContasPagar.Where(r => r.DataPagamento >= DataInicio
                                                    && r.DataPagamento <= DataFim
                                                    && r.Usuario.IdUsuario.Equals(IdUsuario)).ToList();
                    }
                    else if (DataFim != DateTime.MinValue)
                    {
                        return Con.ContasPagar.Where(r => r.DataPagamento <= DataFim
                                                    && r.Usuario.IdUsuario.Equals(IdUsuario)).ToList();
                    }
                    else if (DataInicio != DateTime.MinValue)
                    {
                        return Con.ContasPagar.Where(r => r.DataPagamento >= DataInicio
                                                    && r.Usuario.IdUsuario.Equals(IdUsuario)).ToList();
                    }
                    else
                    {
                        return FindByUsuario(IdUsuario);
                    }

                }

            }

            catch
            {
                throw;
            }


        }
    }
}
