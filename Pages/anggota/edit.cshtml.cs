using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static anggotaorganisasi.Pages.anggota.IndexModel;

namespace anggotaorganisasi.Pages.anggota
{
    public class editModel : PageModel
    {
        public AnggotaInfo AnggotaInfo = new AnggotaInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source= P20;Initial Catalog=anggotaorganisasi;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM anggotaorganisasi WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                AnggotaInfo.id = "" + reader.GetInt32(0);
                                AnggotaInfo.name = reader.GetString(1);
                                AnggotaInfo.email = reader.GetString(2);
                                AnggotaInfo.phone = reader.GetString(3);
                                AnggotaInfo.address = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }
        public void OnPost()
        {
            AnggotaInfo.id = Request.Form["id"];
            AnggotaInfo.name = Request.Form["name"];
            AnggotaInfo.email = Request.Form["email"];
            AnggotaInfo.phone = Request.Form["phone"];
            AnggotaInfo.address = Request.Form["address"];

            if (AnggotaInfo.id.Length == 0 || AnggotaInfo.name.Length == 0 ||
                AnggotaInfo.email.Length == 0 || AnggotaInfo.phone.Length == 0 ||
                AnggotaInfo.address.Length == 0)
            {
                errorMessage = "Semua Kolom Harus Terisi";
                return;
            }

            try
            {
                String connectionString = "Data Source= P20;Initial Catalog=anggotaorganisasi;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE anggotaorganisasi " +
                                 "SET name=@name, email=@email, phone=@phone, address=@address " +
                                 "WHERE id=@id ";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", AnggotaInfo.name);
                        command.Parameters.AddWithValue("@email", AnggotaInfo.email);
                        command.Parameters.AddWithValue("@phone", AnggotaInfo.phone);
                        command.Parameters.AddWithValue("@address", AnggotaInfo.address);
                        command.Parameters.AddWithValue("@id", AnggotaInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/anggota/Index");
        }
    }
}