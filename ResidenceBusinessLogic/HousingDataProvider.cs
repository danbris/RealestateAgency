using System.Collections.Generic;
using System.Linq;
using ResidenceBusinessLogic.DTO;
using Residence.DataLayer;

namespace ResidenceBusinessLogic
{
    public class HousingDataProvider
    {
        #region Variables

        private ResidenceContext _context;

        #endregion

        #region Constructor
        public HousingDataProvider()
        {
            _context = new ResidenceContext();
        }

        #endregion

        #region CRUD Methods
        public HousingDto GetHouse(int id)
        {
            var houseModel = _context.Houses.SingleOrDefault(x => x.ID == id);
            if (houseModel == null) return null;

            return new HousingDto(houseModel);
        }
        public IList<HousingDto> GetHouses()
        {
            return _context.Houses.Select(x => new HousingDto(x)).ToList();
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

        public bool SaveHousing(HousingDto housing, out string validationMessage)
        {
            validationMessage = string.Empty;
            var operationSucceded = false;
            if (housing.ID > 0)
            {
                //this is an update operation
                operationSucceded = UpdateHousing(housing, out validationMessage);
            }
            else
            {
                //this is an add operation
                operationSucceded = AddNewHousing(housing);
            }

            return operationSucceded;
        }

        #endregion

        #region Private Methods

        private bool AddNewHousing(HousingDto housing)
        {
            var entity = new Housing();
            housing.UpdateEntity(entity);
            var result = _context.Houses.Add(entity);
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

        private bool UpdateHousing(HousingDto housing, out string validationResponse)
        {
            var entity = _context.Houses.SingleOrDefault(b => b.ID == housing.ID);
            validationResponse = ValidateData(housing);
            if (entity != null && string.IsNullOrEmpty(validationResponse))
            {
                //validate values for min/max, NaN..., or whatever else could break the data layer
                housing.UpdateEntity(entity);
                //result.Description = housing.Description;
                //result.Surface = housing.Surface;
                //result.NoOfRooms = housing.NoOfRooms;
                //result.NoOfFlats = housing.NoOfFlats;
                //result.FlatNo = housing.FlatNo;
                //result.HouseNo = housing.HouseNo;
                _context.SaveChanges();
                return true;
            }
            else 
            {
                return false; 
            }
        }

        //you have to add validations for all properties here
        private string ValidateData(HousingDto housingModel)
        {
            if (double.IsNaN(housingModel.Surface) || double.IsInfinity(housingModel.Surface)) return "Invalid Surface";

            return string.Empty;
        }

        #endregion
    }
}
