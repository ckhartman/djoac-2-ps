using djoac_2_ps.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace djoac_2_ps
{
    public class DjoacDAL
    {
        private string _connectionString;
        public DjoacDAL(IConfiguration iconfiguration)
        {
            _connectionString = iconfiguration.GetConnectionString("Default");
        }
        public List<DjoacModel> GetList()
        {
            var listDjoacModel = new List<DjoacModel>();
            try
            {
                using(SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("select * from v_djoac_forExport", con);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Decimal dec = Convert.ToDecimal(rdr[3]);
                        listDjoacModel.Add(new DjoacModel
                        {
                            DtAgreed = rdr[0].ToString(),
                            EmpId = rdr[1].ToString(),
                            PsCode = rdr[2].ToString().Trim(),
                            IdJid = dec.ToString("0.00").Replace(".00", String.Empty)
                        });

                    }
                                        
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listDjoacModel;
        }
    }
}
