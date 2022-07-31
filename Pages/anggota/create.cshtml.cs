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
    public class createModel : PageModel
    {
        public AnggotaInfo AnggotaInfo = new AnggotaInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            AnggotaInfo.name = Request.Form["name"];
            AnggotaInfo.email = Request.Form["email"];
            AnggotaInfo.phone = Request.Form["phone"];
            AnggotaInfo.address = Request.Form["address"];

            if (AnggotaInfo.name.Length == 0 || AnggotaInfo.email.Length == 0 ||
                AnggotaInfo.phone.Length == 0 || AnggotaInfo.address.Length == 0)
            {
                errorMessage = "Semua Kolom Tidak Boleh Kosong";
                return;
            }

            try
            {
                String connectionString = "Data Source= P20;Initial Catalog=anggotaorganisasi;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO anggotaorganisasi" +
                                 "(name, email, phone,address) VALUES" +
                                 "(@name, @email, @phone, @address);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", AnggotaInfo.name);
                        command.Parameters.AddWithValue("@email", AnggotaInfo.email);
                        command.Parameters.AddWithValue("@phone", AnggotaInfo.phone);
                        command.Parameters.AddWithValue("@address", AnggotaInfo.address);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            AnggotaInfo.name = ""; AnggotaInfo.email = ""; AnggotaInfo.phone = ""; AnggotaInfo.address = "";
            successMessage = "Anggota Baru Berhasil Ditambahkan";

            Response.Redirect("/anggota/Index");
        }
    }
}