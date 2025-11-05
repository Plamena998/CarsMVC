namespace WebApp.Contracts
{
    using WebApp.Models;

    public interface ICarsService
    {
        Task<string> GetCarAsync(string make);
        Task<List<CarViewModel>> GetCarsAsync(string make); // make се използва само за съвместимост
    }
}