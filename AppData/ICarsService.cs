using Core.Models;
namespace Core.Contracts
{

    public interface ICarsService
    {
        public Task<List<CarViewModel>> GetCarsAsync(int year);
        public Task<List<CarViewModel>> GetTop10CarsByYear( int year);
        public Task<CarViewModel?> GetCarFromLoadedDataAsync(string make, string model, int year);
    }
}