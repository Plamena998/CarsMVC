namespace WebApp.Contracts
{
    using WebApp.Models;

    public interface ICarsService
    {
        Task<List<CarViewModel>> GetCarsAsync(int year);
    }
}