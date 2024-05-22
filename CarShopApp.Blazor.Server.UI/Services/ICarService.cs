using CarShopApp.Blazor.Server.UI.Services.Base;

namespace CarShopApp.Blazor.Server.UI.Services
{
    public interface ICarService
    {
        Task<Response<List<CarReadOnlyDto>>> GetCars();
        Task<Response<CarDetailsDto>> GetCar(int id);
        Task<Response<CarUpdateDto>> CarUpdate(int id);
        Task<Response<int>> CreateCar(CarCreateDto producer);
        Task<Response<int>> UpdateCar(int id, CarUpdateDto producer);
        Task<Response<int>> DeleteCar(int id);
    }
}