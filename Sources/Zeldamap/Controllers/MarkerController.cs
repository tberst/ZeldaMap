using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using zeldassistant.Data;
using zeldassistant.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using zeldassistant.ViewModel;
using zeldassistant.Code;

namespace zeldassistant.web.Controllers
{
   
    public class MarkerController : Controller
    {
        private MarkerService _service;

        public  MarkerController(ZeldaDataContext context)
        {
            this._service = new MarkerService(context);
        }

      
        public JsonResult Index()
        {
            List<UserMarker> data = null;
            if (this.User.Identity.IsAuthenticated)
            {
                int userId = Helper.GetUserId(this.User.Identity as ClaimsIdentity);

                 data = _service.GetByUserId(userId);
              
            }
            else
            {
                data = _service.GetDefaults();
            }

            List<VmMarker> markers = map(data);
            JsonResult result = new JsonResult(new { Data = markers } );


            return result;
        }


        private List<VmMarker> map(List<UserMarker> data)
        {
            var color = new ColorHelper();
            List<VmMarker> result = data.Select(m => new VmMarker() {
                Description = m.Description,
                DoneTime = m.DoneTime,
                Guid = m.Guid,
                Id = m.Id,
                IsDeleted = m.IsDeleted,
                IsDone = m.IsDone,
                Lat = m.Lat.ToString().Replace(",","."),
                Lng = m.Lng.ToString().Replace(",", "."),
                Name = m.Name,
                Type = m.Type,
                UserId = m.UserId,
                X = m.X,
                Y = m.Y,
                Color = color.Get(m.Type)
            }).ToList();
            return result;
        }

        [Authorize]
        [HttpPost]
        public JsonResult Save(UserMarker model)
        {
            int userId = Helper.GetUserId(this.User.Identity as ClaimsIdentity);
            var UserMarker = new UserMarker()
            {
                Description = model.Description,
                Guid = model.Guid,
                IsDone = model.IsDone,
                Id = model.Id,
                Name = model.Name

            };
            UserMarker = this._service.Update(UserMarker, userId);

            return new JsonResult(UserMarker);
        }

        [Authorize]
        [HttpPost]

        public JsonResult Set(VmMarker m)
        {
            decimal lattitude = 0, longitude = 0;
            if (!decimal.TryParse(m.Lat, out lattitude))
            {
                decimal.TryParse(m.Lat.Replace(".", ","), out lattitude);
            }
            if (!decimal.TryParse(m.Lng, out longitude))
            {
                decimal.TryParse(m.Lng.Replace(".", ","), out longitude);
            }

            UserMarker marker = new UserMarker()
            {
                IsDeleted =false,
                Lat = lattitude,
                Lng = longitude,
                Type = m.Type,
                Name = m.Name,
                Description = m.Description
                
            };
           var currentUserId= Helper.GetUserId(this.User.Identity as ClaimsIdentity);
            marker =  _service.Insert(marker, currentUserId);
            return new JsonResult(marker);
        }

        [Authorize]
        [HttpPost]
 
        public JsonResult Delete(int id)
        {
            var userId = Helper.GetUserId(this.User.Identity as ClaimsIdentity);
            bool result = _service.DeleteById(id, userId);
            return new JsonResult(result);
        }

        [Authorize]
        [HttpPost]

        public JsonResult Done(int id)
        {
            var userId = Helper.GetUserId(this.User.Identity as ClaimsIdentity);
            var result = _service.DoneById(id, userId);
            return new JsonResult(result);
        }


        [Authorize]
        [HttpPost]

        public JsonResult Undone(int id)
        {
            var userId = Helper.GetUserId(this.User.Identity as ClaimsIdentity);
            var result = _service.UndoneById(id, userId);
            return new JsonResult(result);
        }

    }
}