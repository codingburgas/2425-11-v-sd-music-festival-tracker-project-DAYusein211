using FestivalApp.DAL.Models;
namespace FestivalApp.BLL
{
    public interface IFestivalService
    {
        Task<IEnumerable<Festival>> GetAllFestivalsAsync();
        Task<Festival> GetFestivalByIdAsync(int id);
        Task AddFestivalAsync(Festival festival);
        Task UpdateFestivalAsync(Festival festival);
    }
}