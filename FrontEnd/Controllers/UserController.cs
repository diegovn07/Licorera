using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BackEnd.DAL;
using BackEnd.Entities;
using FrontEnd.Models;

namespace FrontEnd.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UserController : Controller
    {
        private IUserDAL userDAL;
        string urlDomain = "https://localhost:44358/";

        private UserViewModel Convertir(Users usuario)
        {
            UserViewModel usuarioView = new UserViewModel
            {
                UserId = usuario.UserId,
                UserName = usuario.UserName,
                cedula = usuario.cedula,
                nombre = usuario.nombre,
                apellido1 = usuario.apellido1,
                apellido2 = usuario.apellido2,
                telefono = usuario.telefono,
                telefono2 = usuario.telefono2,
                Correo = usuario.Correo,
                Password = usuario.Password,
                direccion = usuario.direccion
            };
            return usuarioView;
        }
        private Users Convertir(UserViewModel usuario)
        {
            Users usuarioView = new Users
            {
                UserId = usuario.UserId,
                UserName = usuario.UserName,
                cedula = usuario.cedula,
                nombre = usuario.nombre,
                apellido1 = usuario.apellido1,
                apellido2 = usuario.apellido2,
                telefono = usuario.telefono,
                telefono2 = usuario.telefono2,
                Correo = usuario.Correo,
                Password = usuario.Password,
                direccion = usuario.direccion

            };
            return usuarioView;
        }
        [AllowAnonymous]
        public ActionResult CreadoExito()
        {
            return View();
        }

        public ActionResult Index()
        {
            List<Users> usuarios;
            using (UnidadDeTrabajo<Users> Unidad = new UnidadDeTrabajo<Users>(new BDContext()))
            {
                usuarios = Unidad.genericDAL.GetAll().ToList();
            }

            List<UserViewModel> lista = new List<UserViewModel>();

            foreach (var item in usuarios)
            {
                lista.Add(this.Convertir(item));
            }
            return View(lista);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserViewModel usuarioViewModel)
        {
            Users usuario = this.Convertir(usuarioViewModel);
            usuario.Password = CryptoEngine.Encrypt(usuario.Password);

            using (UnidadDeTrabajo<Users> unidad = new UnidadDeTrabajo<Users>(new BDContext()))
            {
                unidad.genericDAL.Add(usuario);
                unidad.Complete();
            }

            //Asigna Role usuario
            userDAL = new UserDALImpl();
            var userid = usuario.UserId;
            userDAL.miInsertar(userid, 1);

            if (usuarioViewModel.UsuarioAdmin == true)
            {
                userDAL = new UserDALImpl();
                userDAL.miInsertar(userid, 2);
            }


            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        public ActionResult CreateNoUser()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult CreateNoUser(UserViewModel usuarioViewModel)
        {
            Users usuario = this.Convertir(usuarioViewModel);
            usuario.Password = CryptoEngine.Encrypt(usuario.Password);

            using (UnidadDeTrabajo<Users> unidad = new UnidadDeTrabajo<Users>(new BDContext()))
            {
                unidad.genericDAL.Add(usuario);

                unidad.Complete();
            }
            // Agrega role User 
            userDAL = new UserDALImpl();
            var userid = usuario.UserId;
            userDAL.miInsertar(userid, 1);
            return RedirectToAction("CreadoExito", "User");
        }

        public ActionResult Edit(int id)
        {

            Users usuario;
            using (UnidadDeTrabajo<Users> unidad = new UnidadDeTrabajo<Users>(new BDContext()))
            {
                usuario = unidad.genericDAL.Get(id);
            }
            userDAL = new UserDALImpl();
            string[] roles = userDAL.getRolesForUser(usuario.UserName);
            UserViewModel user = this.Convertir(usuario);
            user.UsuarioAdmin = roles.Contains("Administrador");
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel usuarioViewModel)
        {
            usuarioViewModel.Password = CryptoEngine.Encrypt(usuarioViewModel.Password);

            using (UnidadDeTrabajo<Users> unidad = new UnidadDeTrabajo<Users>(new BDContext()))
            {
                unidad.genericDAL.Update(this.Convertir(usuarioViewModel));
                unidad.Complete();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {

            Users users;
            using (UnidadDeTrabajo<Users> unidad = new UnidadDeTrabajo<Users>(new BDContext()))
            {
                users = unidad.genericDAL.Get(id);

            }

            userDAL = new UserDALImpl();
            string[] roles = userDAL.getRolesForUser(users.UserName);
            UserViewModel user = this.Convertir(users);
            user.UsuarioAdmin = roles.Contains("Administrador");

            return View(users);
        }

        public ActionResult Delete(int id)
        {

            Users usuario;
            using (UnidadDeTrabajo<Users> unidad = new UnidadDeTrabajo<Users>(new BDContext()))
            {
                usuario = unidad.genericDAL.Get(id);

            }

            userDAL = new UserDALImpl();
            string[] roles = userDAL.getRolesForUser(usuario.UserName);
            UserViewModel user = this.Convertir(usuario);
            user.UsuarioAdmin = roles.Contains("Administrador");
            return View(user);
        }

        [HttpPost]
        public ActionResult Delete(UserViewModel usuarioViewModel)
        {
            //elimina roles del usuario
            userDAL = new UserDALImpl();
            userDAL.EliminaUsuarioRol(usuarioViewModel.UserId);

            using (UnidadDeTrabajo<Users> unidad = new UnidadDeTrabajo<Users>(new BDContext()))
            {
                unidad.genericDAL.Remove(this.Convertir(usuarioViewModel));
                unidad.Complete();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult getClient (UserViewModel user)
        {
            Users users;
            using (UnidadDeTrabajo<Users> unidad = new UnidadDeTrabajo<Users>(new BDContext()))
            {
                users = unidad.genericDAL.Find(x => x.cedula == user.cedula).ToList().FirstOrDefault();
            }

            if (users != null)
            {
                return Json(new
                {
                    status = true,
                    nombre = users.nombre + ' ' + users.apellido1 + ' ' + users.apellido2,
                    identificacion = users.cedula,
                    direccion = users.direccion,
                    correo = users.Correo,
                    telefono = users.telefono
                });
            }
            else {
                return Json(new
                {
                    status = false,
                });
            }
            
        }

        //---------------------LOGIN----------------------------------------//


        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Autherize(UserViewModel userModel)
        {

            userDAL = new UserDALImpl();
            var userDetails = userDAL.getUser(userModel.UserName, CryptoEngine.Encrypt(userModel.Password));


            if (userDetails == null)
            {
                userModel.LoginErrorMensaje = "Nombre de Usuario o Password Incorrectos";
                return View("Login", userModel);
            }
            else
            {
                Session["userID"] = userDetails.UserId;
                Session["userName"] = userDetails.UserName;
                HttpCookie userInfo = new HttpCookie("userInfo");
                userInfo["userID"] = userDetails.UserId.ToString();
                userInfo.Expires.Add(new TimeSpan(0, 1, 0));
                Response.Cookies.Add(userInfo);

                var authTicket = new FormsAuthenticationTicket(userDetails.UserName, true, 100000);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                                            FormsAuthentication.Encrypt(authTicket));
                Response.Cookies.Add(cookie);
                var name = User.Identity.Name;
                return RedirectToAction("Index", "Home");
            }
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon();

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            // clear session cookie
            HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);

            HttpCookie cookie3 = new HttpCookie("userInfo");
            cookie3.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie3);
            return RedirectToAction("Index", "Home");
        }


        //---------------------RECUPERAR PASS----------------------------------------//
        [AllowAnonymous]
        public ActionResult RecuperaExito()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpGet]
        public ActionResult StartRecovery() // pantalla donde va ver lo de recuperar contraseña
        {
            Models.ViewModel.RecoveryViewModel model = new Models.ViewModel.RecoveryViewModel();
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult StartRecovery(Models.ViewModel.RecoveryViewModel model) // pantalla donde va ver lo de recuperar contraseña
        {
            try
            {
                if (!ModelState.IsValid) //para saber si pasa las validaciones que se agregaron
                {
                    return View(model);
                }

                string token = GetSha256(Guid.NewGuid().ToString());

                using (BDContext db = new BDContext())
                {
                    var oUser = db.Users.Where(d => d.Correo == model.Email).FirstOrDefault();
                    if (oUser != null)
                    {
                        oUser.token_recovery = token;
                        db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        //enviar correo
                        SendEmail(oUser.Correo, token);
                    }
                }

                return RedirectToAction("RecuperaExito", "User");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Recovery(string token) //pantalla que le va mostrar cuando este en el correo para recuperar la pass
        {
            Models.ViewModel.RecoveryPasswordViewModel model = new Models.ViewModel.RecoveryPasswordViewModel();
            model.token = token;
            using (BDContext db = new BDContext())
            {
                if (model.token == null || model.token.Trim().Equals(""))
                {
                    return View("Index");
                }
                var oUser = db.Users.Where(d => d.token_recovery == model.token).FirstOrDefault();
                if (oUser == null)
                {
                    ViewBag.Error = "Tu token ha expirado.";
                    return View("Index");
                }
            }


            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Recovery(Models.ViewModel.RecoveryPasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) //para saber si pasa las validaciones que se agregaron
                {
                    return View(model);
                }

                using (BDContext db = new BDContext())
                {
                    var oUser = db.Users.Where(d => d.token_recovery == model.token).FirstOrDefault();

                    if (oUser != null)
                    {
                        oUser.Password = CryptoEngine.Encrypt(model.Password);
                        oUser.token_recovery = null;
                        db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            ViewBag.Message = "Contraseña modificada con éxito";
            return View("Login");
        }

        #region Helpers
        private string GetSha256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        private void SendEmail(string EmailDestino, string token)
        {
            string EmailOrigen = "happylicorera@gmail.com";
            string Contraseña = "LicoCR2020";
            string url = urlDomain + "/User/Recovery/?token=" + token;
            MailMessage oMailMessage = new MailMessage(EmailOrigen, EmailDestino, "Recuperación de contraseña",
                "<p>Correo para recuperación de contrasña</p><br>" +
                "<a href='" + url + "'>Click para recuperar</a>");

            oMailMessage.IsBodyHtml = true;

            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com"); //esto es por que se tiene un correo gmail
            oSmtpClient.EnableSsl = true;
            oSmtpClient.UseDefaultCredentials = false;
            //oSmtpClient.Host = "smtp.gmail.com";
            oSmtpClient.Port = 587; //puerto de gmail con el que se envian los correos
            oSmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, Contraseña);

            oSmtpClient.Send(oMailMessage);

            oSmtpClient.Dispose();

        }

        #endregion
    }
}