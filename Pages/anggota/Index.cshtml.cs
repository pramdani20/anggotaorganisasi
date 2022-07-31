using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace anggotaorganisasi.Pages.anggota
{
    public class IndexModel : PageModel
    {
        public List<AnggotaInfo> ListAnggota = new List<AnggotaInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source= P20;Initial Catalog=anggotaorganisasi;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM anggotaorganisasi";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AnggotaInfo anggotaInfo = new AnggotaInfo();
                                anggotaInfo.id = "" + reader.GetInt32(0);
                                anggotaInfo.name = reader.GetString(1);
                                anggotaInfo.email = reader.GetString(2);
                                anggotaInfo.phone = reader.GetString(3);
                                anggotaInfo.address = reader.GetString(4);
                                anggotaInfo.created_at = reader.GetDateTime(5).ToString();

                                ListAnggota.Add(anggotaInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Expection: " + ex.ToString());
            }
        }

        public class AnggotaInfo
        {
            public String id;
            public String name;
            public String email;
            public String phone;
            public String address;
            public String created_at;
        }

    }
}