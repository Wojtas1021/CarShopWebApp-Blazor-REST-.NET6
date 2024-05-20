using AutoMapper;
using Blazored.LocalStorage;
using CarShopApp.Blazor.Server.UI.Services.Base;

namespace CarShopApp.Blazor.Server.UI.Services
{
    public class ProducerService : BaseHttpService, IProducerService
    {
        private readonly IClient client;
        private readonly IMapper mapper;

        public ProducerService(IClient client, ILocalStorageService localStorage, IMapper mapper) 
            : base(client, localStorage)
        {
            this.client = client;
            this.mapper = mapper;
        }

        public async Task<Response<int>> CreateProducer(ProducerCreateDto producer)
        {
            Response<int> response = new();
            try
            {
                //get token
                await GetBearerToken();
                // get post method from api to create 
                await client.ProducersPOSTAsync(producer);

            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<int>(ex);
            }

            return response;
        }

        public async Task<Response<ProducerUpdateDto>> GetProducerForUpdate(int id)
        {
            Response<ProducerUpdateDto> response;
            try
            {
                await GetBearerToken();
                var data = await client.ProducersGETAsync(id);
                response = new Response<ProducerUpdateDto>
                {
                    Data = mapper.Map<ProducerUpdateDto>(data),
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<ProducerUpdateDto>(ex);
            }

            return response;
        }

        public async Task<Response<ProducerDetailsDto>> GetProducer(int id)
        {
            Response<ProducerDetailsDto> response;
            try
            {
                await GetBearerToken();
                var data = await client.ProducersGETAsync(id);
                response = new Response<ProducerDetailsDto>
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<ProducerDetailsDto>(ex);
            }

            return response;
        }
        public async Task<Response<List<ProducerReadOnlyDto>>> GetProducers()
        {
            Response<List<ProducerReadOnlyDto>> response;
            try
            {
                await GetBearerToken();
                var data = await client.ProducersAllAsync();
                response = new Response<List<ProducerReadOnlyDto>>
                {
                    Data = data.ToList(),
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<List<ProducerReadOnlyDto>>(ex);
            }

            return response;
        }

        public async Task<Response<int>> UpdateProducer(int id, ProducerUpdateDto producer)
        {
            Response<int> response = new();
            try
            {
                //get token
                await GetBearerToken();
                // get put method from api to create 
                await client.ProducersPUTAsync(id, producer);

            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<int>(ex);
            }

            return response;
        }
    }
}
