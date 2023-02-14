using System;
using System.Linq;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Web.Mvc;
using Einstein.Domain.Services;
using Einstein.WebUI.Models;
using Einstein.Domain.Entities;

namespace Einstein.WebUI.Controllers.Services
{
    [Authorize(Roles = "1")]
    public class UserServiceController : Controller
    {
        private IRepository repos;
        private ICryptoService crypto;
        public UserServiceController(IRepository _repos, ICryptoService _crypto)
        {
            repos = _repos;
            crypto = _crypto;
        }

        protected override void Dispose(bool disposing)
        {
            repos.Dispose();

            base.Dispose(disposing);
        }

        //Read
        public ActionResult ReadForGrid([DataSourceRequest] DataSourceRequest request)
        {
            var users = repos.Users.ToDataSourceResult(request, u => new UserViewModel
            {
                id = u.ID,
                login =u.LOGIN,
                roleid=u.ROLEID,
                password=u.PASSWORD
            });

            return Json(users);
        }


       

        //Create
        [HttpPost]
        public ActionResult CreateForGrid([DataSourceRequest]DataSourceRequest request, UserViewModel user)
        {
            if (repos.Users.Any(u=>u.LOGIN == user.login))
            {
                ModelState.AddModelError("errors", "Пользователь с таким Логином уже существует!");
                
            }

            if (ModelState.IsValid)
            {
                var entity = user.ToEntity(new USER(),crypto);

                try
                {
                    repos.AddUser(entity);
                    user.id = entity.ID;
                    user.password = crypto.EncryptPassword(user.password);

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("USER", ex.Message);
                }
            }

            return Json(new[] { user }.ToDataSourceResult(request, ModelState));

        }

        //Update
        [HttpPost]
        public ActionResult UpdateForGrid([DataSourceRequest]DataSourceRequest request, UserViewModel user)
        {
            
                USER entity = repos.Users.FirstOrDefault(c => c.ID == user.id);

                if (entity == null)
                {
                    ModelState.AddModelError("User", String.Format("Пользователь '{0}' не обнаружена в базе данных!", user.login));
                }
                else
                {
                    //TODO Validate not found
                    entity = user.ToEntity(entity,crypto);

                   
                }

            if (repos.Users.Any(u => u.LOGIN == user.login && u.ID != user.id)) 
            {
                ModelState.AddModelError("errors", "Пользователь с таким Логином уже существует!");

            }

            if (ModelState.IsValid)
            {
                try
                {
                    repos.UpdateUser(entity);
                    user.password = crypto.EncryptPassword(user.password);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("User", ex.Message);
                }
            }

            return Json(new[] { user }.ToDataSourceResult(request, ModelState));

        }
       
        //Delete
        [HttpPost]
        public ActionResult DestroyForGrid([DataSourceRequest]DataSourceRequest request, UserViewModel user)
        {

            USER entity = repos.Users.FirstOrDefault(c => c.ID == user.id);

            if (entity == null)
            {
                ModelState.AddModelError("User", String.Format("Пользователь '{0}' не обнаружена в базе данных!", user.login));
            }

            //Used  in test 
            

            if (ModelState.IsValid)
            {
                try
                {
                    repos.DeleteUser(entity);

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("USER", ex.Message);
                }
            }

            return Json(new[] { user }.ToDataSourceResult(request, ModelState));

        }

        [HttpPost]
        public ActionResult DecryptPassword(string cryptpass)
        {
            try
            {
                var password = crypto.DecryptPassword(cryptpass);
                return Json(new { password = password });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
            
        }

    }
}