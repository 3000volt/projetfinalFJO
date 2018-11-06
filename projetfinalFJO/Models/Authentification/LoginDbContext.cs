using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace projetfinalFJO.Models.Authentification
{
    public class LoginDbContext : IdentityDbContext<LoginUser, LoginRole, string>
    {
        private readonly string ConnectionString;
        IConfiguration config;

        public LoginDbContext(DbContextOptions<LoginDbContext> options, IConfiguration config)
            : base(options)
        {
            this.config = config;
            this.ConnectionString = config.GetConnectionString("LoginConnection");
        }

        public void SupprimerUtilisateur(string email)
        {
            //utiliser le connectionString pour pouvoir affecter la BD
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                //requete pour supprimer un livre
                string sqlStr = "delete from AspNetUsers where UserName = @adresseCourriel";
                //Code pour Affecter la BD
                SqlCommand cmd = new SqlCommand(sqlStr, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                //Associer la valeur de l isbn en paramettre
                cmd.Parameters.AddWithValue("adresseCourriel", email);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void ModifierRole(string roleId, string userId)
        {
            //utiliser le connectionString pour pouvoir affecter la BD
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                //requete pour supprimer un livre
                string sqlStr = "update AspNetUserRoles set RoleId = @roleId where UserId = @userId";
                //Code pour Affecter la BD
                SqlCommand cmd = new SqlCommand(sqlStr, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                //Associer la valeur de l isbn en paramettre
                cmd.Parameters.AddWithValue("roleId", roleId);
                cmd.Parameters.AddWithValue("userId", userId);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }

}
