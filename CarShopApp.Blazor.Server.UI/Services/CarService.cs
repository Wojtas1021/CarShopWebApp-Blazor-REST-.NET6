using AutoMapper;
using Blazored.LocalStorage;
using CarShopApp.Blazor.Server.UI.Services.Base;

namespace CarShopApp.Blazor.Server.UI.Services
{
    public class CarService : BaseHttpService, ICarService
    {
        private readonly IClient client;
        private readonly IMapper mapper;

        public CarService(IClient client, ILocalStorageService localStorage, IMapper mapper)
            : base(client, localStorage)
        {
            this.client = client;
            this.mapper = mapper;
        }

        public async Task<Response<int>> CreateCar(CarCreateDto car)
        {
            Response<int> response = new();
            try
            {
                await GetBearerToken();
                await client.CarsPOSTAsync(car);
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<int>(ex);
            }

            return response;
        }

        public async Task<Response<CarUpdateDto>> CarUpdate(int id)
        {
            Response<CarUpdateDto> response;
            try
            {
                await GetBearerToken();
                var data = await client.CarsGETAsync(id);
                response = new Response<CarUpdateDto>
                {
                    Data = mapper.Map<CarUpdateDto>(data),
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<CarUpdateDto>(ex);
            }

            return response;
        }

        public async Task<Response<CarDetailsDto>> GetCar(int id)
        {
            Response<CarDetailsDto> response;
            try
            {
                await GetBearerToken();
                var data = await client.CarsGETAsync(id);
                response = new Response<CarDetailsDto>
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<CarDetailsDto>(ex);
            }

            return response;
        }
        public async Task<Response<List<CarReadOnlyDto>>> GetCars()
        {
            Response<List<CarReadOnlyDto>> response;
            try
            {
                await GetBearerToken();
                var data = await client.CarsAllAsync();
                response = new Response<List<CarReadOnlyDto>>
                {
                    Data = data.ToList(),
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<List<CarReadOnlyDto>>(ex);
            }

            return response;
        }
        public async Task<Response<int>> UpdateCar(int id, CarUpdateDto car)
        {
            Response<int> response = new();
            try
            {
                await GetBearerToken();
                await client.CarsPUTAsync(id, car);
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<int>(ex);
            }

            return response;
        }

        public async Task<Response<int>> DeleteCar(int id)
        {
            Response<int> response = new();
            try
            {
                await GetBearerToken();
                await client.CarsDELETEAsync(id);
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<int>(ex);
            }
            return response;
        }
    }
}
