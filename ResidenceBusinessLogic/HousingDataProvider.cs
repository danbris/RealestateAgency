using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResidenceBusinessLogic.DTO;
using Residence.DataLayer;

namespace ResidenceBusinessLogic
{
    public class HousingDataProvider
    {
        #region Variables

        private Residence.DataLayer.ResidenceContext _context;

        #endregion

        #region Constructor
        public HousingDataProvider()
        {
            _context = new Residence.DataLayer.ResidenceContext();
        }

        #endregion

        #region CRUD Methods
        public HousingDto GetHouse(int id)
        {
            return _context.Houses.Select(x => new HousingDto()
            {
                ID = x.ID,
                Description = x.Description,
                FlatNo = x.FlatNo,
                HouseNo = x.HouseNo,
                HousingType = x.HousingType,
                NoOfFlats = x.NoOfFlats,
                NoOfRooms = x.NoOfRooms,
                Surface = x.Surface,
                Comodities = x.Comodities.Select(y => new ComodityDto()
                {
                    ID = y.ID,
                    Description = y.Description,
                    Currency = y.Currency,
                    Price = y.Price
                }).ToList()
            }).ToList().First(i => i.ID == id);
        }
        public IList<HousingDto> GetHouses()
        {
            return _context.Houses.Select(x => new HousingDto()
            {
                ID = x.ID,
                Description = x.Description,
                FlatNo = x.FlatNo,
                HouseNo = x.HouseNo,
                HousingType = x.HousingType,
                NoOfFlats = x.NoOfFlats,
                NoOfRooms = x.NoOfRooms,
                Surface = x.Surface,
                Comodities = x.Comodities.Select(y => new ComodityDto()
                {
                    ID = y.ID,
                    Description = y.Description,
                    Currency = y.Currency,
                    Price = y.Price
                }).ToList()
            }).ToList();
        }

        public bool DeleteHousing(HousingDto housing)
        {
            var result = _context.Houses.SingleOrDefault(b => b.ID == housing.ID);
            if (result != null)
            {
                _context.Houses.Remove(result);
                _context.SaveChanges();
                return true;
            }
            else 
            {
                return false; 
            }
        }

        public bool SaveHousing(HousingDto housing)
        {
            var operationSucceded = false;
            if (housing.ID > 0)
            {
                //this is an update operation
                operationSucceded = UpdateHousing(housing);
            }
            else
            {
                //this is an add operation
                operationSucceded = AddNewHousing(housing);
            }

            return operationSucceded;
        }

        #endregion

        #region Private Methods for helping CRUD functionality

        private Housing DtoToEntity(HousingDto housing)
        {
            var entityObject = new Housing();
            entityObject.Description = housing.Description;
            entityObject.Surface = housing.Surface;
            entityObject.NoOfRooms = housing.NoOfRooms;
            entityObject.NoOfFlats = housing.NoOfFlats;
            entityObject.FlatNo = housing.FlatNo;
            entityObject.HouseNo = housing.HouseNo;
            return entityObject;
        }

        private bool AddNewHousing(HousingDto housing)
        {
            var result = _context.Houses.Add(DtoToEntity(housing));
            if (result != null)
            {
                _context.SaveChanges();
                return true;
            }
            else 
            { 
                return false;
            }
        }

        private bool UpdateHousing(HousingDto housing)
        {
            var result = _context.Houses.SingleOrDefault(b => b.ID == housing.ID);
            if (result != null)
            {
                result.Description = housing.Description;
                result.Surface = housing.Surface;
                result.NoOfRooms = housing.NoOfRooms;
                result.NoOfFlats = housing.NoOfFlats;
                result.FlatNo = housing.FlatNo;
                result.HouseNo = housing.HouseNo;
                _context.SaveChanges();
                return true;
            }
            else 
            { 
                return false; 
            }
        }

        #endregion
    }
}
