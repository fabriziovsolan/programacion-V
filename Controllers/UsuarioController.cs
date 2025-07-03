﻿using Microsoft.AspNetCore.Mvc;
using MvcMovie1.Models;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

namespace MvcMovie1.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        string connectionString = "Server=GlobalTechology;Database=programacionV;User Id=sa;Password=1612;Trusted_Connection=True;TrustServerCertificate=True;";

        public ActionResult Lista()
        {
            List<UsuarioModel> usuarios = new List<UsuarioModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT id, username, name, fechanacimiento, email, password from Usuario";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    usuarios.Add(new UsuarioModel
                    {
                        id = reader.GetInt32(0),
                        username = reader.GetString(1),
                        name = reader.GetString(2),
                        fechanacimiento = reader.GetDateTime(3),
                        email = reader.GetString(4),
                        password = reader.GetString(5)



                    });
                }
            
                return View(usuarios);
                
            }                                 
            return View();


        }
            
        public ActionResult Eliminar(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "delete from Usuario where id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }

                return RedirectToAction("Lista");
        }   
        
        public ActionResult AltaUsuario(string username, string password, string name, string fechanacimiento, string email)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Usuario(username, name, fechanacimiento, email, password)" +
                        "VALUES (@username, @name, @fechanacimiento, @email, @password) ";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@fechanacimiento", fechanacimiento);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        ViewBag.Mensaje = "Usuario Creado Exitosamente!";
                    }
                    else
                    {
                        ViewBag.Mensaje = "Error - Usuario no creado";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al insertar el usuario " + username + ": " + ex.Message;
            }
        


            return View("Index");
        }

        //Este método editar va a recibir como parámetro un objeto
        //y modelo ususario y se encarga hacer el update
        [HttpPost]
        public IActionResult Editar(UsuarioModel usuario)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            {
                string query = @"UPDATE Usuario SET
                                username = @username,
                                name = @name,
                                fechanacimiento = @fechanacimiento,
                                email = @email,      
                                password = @password
                                where id = @id ";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", usuario.username);
                command.Parameters.AddWithValue("@name", usuario.name);
                command.Parameters.AddWithValue("@fechanacimiento", usuario.fechanacimiento);
                command.Parameters.AddWithValue("@email", usuario.email);
                command.Parameters.AddWithValue("@password", usuario.password);
                command.Parameters.AddWithValue("@id", usuario.id);

                connection.Open();
                command.ExecuteNonQuery();


                return RedirectToAction("Lista");
            }
        }

        //y este se encarga de ir a bucar los datos del usuario específico
        //Para mostrar los datos y permitirme en el front modificarlos
          
        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = new UsuarioModel();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT id, username, name, fechanacimiento, email, password from Usuario where id = " + id.ToString();

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    usuario.id = reader.GetInt32(0);
                    usuario.username = reader.GetString(1);
                    usuario.name = reader.GetString(2);
                    usuario.fechanacimiento = reader.GetDateTime(3);
                    usuario.email = reader.GetString(4);
                    usuario.password = reader.GetString(5);
                
                }

                return View(usuario);

            }
        }
    }
}
