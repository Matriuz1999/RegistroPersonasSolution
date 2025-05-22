using Microsoft.IdentityModel.Protocols;
using Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Data
{
    public class PersonaRepository
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;

        public bool ExisteDocumento(string documento)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            var cmd = new SqlCommand("SELECT COUNT(*) FROM Personas WHERE DocumentoIdentidad = @doc", conn);
            cmd.Parameters.AddWithValue("@doc", documento);
            conn.Open();
            return (int)cmd.ExecuteScalar() > 0;
        }

        public void RegistrarPersona(Persona p)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            var tran = conn.BeginTransaction();

            try
            {
                var cmd = new SqlCommand(@"INSERT INTO Personas (DocumentoIdentidad, Nombres, Apellidos, FechaNacimiento)
                                           VALUES (@doc, @nom, @ape, @fecha)", conn, tran);
                cmd.Parameters.AddWithValue("@doc", p.DocumentoIdentidad);
                cmd.Parameters.AddWithValue("@nom", p.Nombres);
                cmd.Parameters.AddWithValue("@ape", p.Apellidos);
                cmd.Parameters.AddWithValue("@fecha", p.FechaNacimiento);
                cmd.ExecuteNonQuery();

                foreach (var tel in p.Telefonos)
                {
                    var telCmd = new SqlCommand("INSERT INTO Telefonos (DocumentoIdentidad, Telefono) VALUES (@doc, @tel)", conn, tran);
                    telCmd.Parameters.AddWithValue("@doc", p.DocumentoIdentidad);
                    telCmd.Parameters.AddWithValue("@tel", tel);
                    telCmd.ExecuteNonQuery();
                }

                foreach (var email in p.Correos)
                {
                    var emailCmd = new SqlCommand("INSERT INTO Correos (DocumentoIdentidad, Correo) VALUES (@doc, @email)", conn, tran);
                    emailCmd.Parameters.AddWithValue("@doc", p.DocumentoIdentidad);
                    emailCmd.Parameters.AddWithValue("@email", email);
                    emailCmd.ExecuteNonQuery();
                }

                foreach (var dir in p.Direcciones)
                {
                    var dirCmd = new SqlCommand("INSERT INTO Direcciones (DocumentoIdentidad, Direccion) VALUES (@doc, @dir)", conn, tran);
                    dirCmd.Parameters.AddWithValue("@doc", p.DocumentoIdentidad);
                    dirCmd.Parameters.AddWithValue("@dir", dir);
                    dirCmd.ExecuteNonQuery();
                }

                tran.Commit();
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }
    }
}
