using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Entities;

namespace BackEnd.DAL
{
   public interface IUserDAL
    {
        bool insertar(int idRole, string login);
        bool insertar(string roleName, string login);

        bool miInsertar(int idUser, int idRole);
        bool EliminaUsuarioRol(int idUser);
        Users getUser(string userName);
        Users getUser(string userName, string password);
        Users getUser(int EmpleadoId);
        Users get(int id);
        List<Users> getAll();
        bool isUserInRole(string userName, string roleName);
        string[] getRolesForUser(string userName);
        List<Users> getUsuariosRole(string roleName);
        List<Roles> listarRoles();
        bool eliminar(string idRole, int idUsuario);
        Users ObtenerPorID(int id);
    }
}
