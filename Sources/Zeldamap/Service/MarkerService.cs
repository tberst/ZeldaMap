using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zeldassistant.Data;
using zeldassistant.Models;

namespace zeldassistant
{
    public class MarkerService
    {
        private ZeldaDataContext _context;


        private DbSet<Marker> _Markers { get { return _context.Markers; } }
        private DbSet<UserMarker> _userMarkers { get { return _context.UserMarkers; } }


        public MarkerService(ZeldaDataContext context)
        {
            this._context = context;


        }

        public List<UserMarker> GetByUserId(int userId)
        {
            var userMarkers = _userMarkers.Where(m => m.UserId == userId && m.IsDeleted != true).ToList();
            var defaultMarker = _Markers.Where(m => m.IsDeleted != true && !(m is UserMarker)).ToList().Select(m => new UserMarker(m)) ;

          
           // return userMarkers;
            return userMarkers.Union(defaultMarker, new UserMarkerComparer()).ToList();

            
           
        }

        internal List<UserMarker> GetDefaults()
        {
            var defaultMarkers = _Markers.Where(m => m.IsDeleted != true && !(m is UserMarker)).ToList().Select(m => new UserMarker(m));
            return defaultMarkers.ToList() ;
        }

        //public List<Marker> GetAll()
        //{
        //    //var result =  this._context.Markers.GroupBy(m => m.X).Select(g => g.OrderBy(m => m.Y).First());
        //    // return result.ToList() ;
        //    return _defaultMarkers.ToList();
        //}

        public UserMarker Insert(UserMarker m, int? userId)
        {
            if (userId.HasValue)
            {
               
                m.UserId = userId.Value;
                m.Guid = Guid.NewGuid();
            }
            _userMarkers.Add(m);
            _context.SaveChanges();
            return m; ;
        }

        internal UserMarker Update(UserMarker userMarker, int userId)
        {
            //check if it's a user defined marker
            UserMarker entity = this._userMarkers.FirstOrDefault(m => m.Id == userMarker.Id);
            
            if (entity!=null && entity.UserId == userId)
            {
                entity.Name = userMarker.Name;
                entity.Description = userMarker.Description;
                if (entity.IsDone != userMarker.IsDone)
                {
                    entity.IsDone = userMarker.IsDone;
                    entity.DoneTime = DateTime.UtcNow;

                }
                this._userMarkers.Update(entity);
                _context.SaveChanges();
            }
            else
            {
                //someone's trying to update a default marker, let's clone it to a usermarker
                Marker defaultentity = this._Markers.FirstOrDefault(m => m.Id == userMarker.Id);
                if (defaultentity != null)
                {
                    UserMarker userM = UserMarkerFromDefault(defaultentity);
                    userM.UserId = userId;
                    _userMarkers.Add(userM);
                    _context.SaveChanges();
                    entity = userM;
                }
            }
            return entity;
        }

        internal bool DeleteById(int id, int userId)
        {
            bool result = false;

            if (userId > 0)
            {
                var marker = _userMarkers.FirstOrDefault(m => m.Id == id);
                if (marker != null && marker.UserId == userId && marker.Type == MarkerType.Custom)
                {
                    _userMarkers.Remove(marker);
                    _context.SaveChanges();
                    result = true;
                }
            }
            else
            {
                var marker = _Markers.FirstOrDefault(m => m.Id == id);
                if (marker != null && marker.Type == MarkerType.Custom)
                {
                    _Markers.Remove(marker);
                    _context.SaveChanges();
                    result = true;
                }
            }



            return result;

        }

        //internal bool AddRange(List<Marker> markers)
        //{
        //    _Markers.AddRange(markers);
        //    _context.SaveChanges();
        //    return true;
        //}

        /// <summary>
        /// TODO : reimplementer le doneById
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        internal UserMarker DoneById(int id, int userId)
        {
            UserMarker result = null;
            var dm = _Markers.FirstOrDefault(m => m.Id == id);
            if (dm != null)
            {
                var marker = _userMarkers.FirstOrDefault(m => m.Guid == dm.Guid && m.UserId == userId);

                if (marker != null)
                {
                    marker.IsDone = true;
                    marker.DoneTime = DateTime.UtcNow;
                    _userMarkers.Update(marker);
                    _context.SaveChanges();
                    result = marker;
                }
                else
                {
                    var um = UserMarkerFromDefault(dm);
                    um.UserId = userId;
                    _userMarkers.Add(um);
                    _context.SaveChanges();
                    result = um;
                }
            }
            else
            {
                var marker =_userMarkers.FirstOrDefault(m => m.Id == id && m.UserId == userId);
                if (marker != null)
                {
                    marker.IsDone = true;
                    marker.DoneTime = DateTime.UtcNow;
                    _Markers.Update(marker);
                    _context.SaveChanges();
                    result = marker;
                }
            }


            return result;
        }

        private UserMarker UserMarkerFromDefault(Marker dm)
        {
            UserMarker um = new UserMarker();

            um.Description = dm.Description;
            um.DoneTime = DateTime.UtcNow;
            um.Guid = dm.Guid;
            um.IsDone = true;
            um.Lat = dm.Lat;
            um.Lng = dm.Lng;
            um.Name = dm.Name;
            um.Type = dm.Type;

            um.X = dm.X;
            um.Y = dm.Y;
            return um;
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        internal UserMarker UndoneById(int id, int userId)
        {
            UserMarker result = null;
            var marker = _userMarkers.FirstOrDefault(m => m.Id == id && m.UserId == userId);
            if (marker != null)
            {
                marker.IsDone = false;
                marker.DoneTime = DateTime.MinValue;
                _userMarkers.Update(marker);
                _context.SaveChanges();
                result = marker;
            }
            else
            {
                var dm = _Markers.FirstOrDefault(m => m.Id == id);
                marker = _userMarkers.FirstOrDefault(m => m.Guid == dm.Guid && m.UserId == userId);
                if (marker != null)
                {
                    marker.IsDone = false;
                    marker.DoneTime = DateTime.MinValue;
                    _userMarkers.Update(marker);
                    _context.SaveChanges();
                    result = marker;
                }
            }

            return result;
        }
    }
}
