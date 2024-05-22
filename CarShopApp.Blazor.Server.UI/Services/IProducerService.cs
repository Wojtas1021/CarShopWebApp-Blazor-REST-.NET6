using CarShopApp.Blazor.Server.UI.Services.Base;

namespace CarShopApp.Blazor.Server.UI.Services
{
    public interface IProducerService
    {
        Task<Response<List<ProducerReadOnlyDto>>> GetProducers();
        Task<Response<ProducerDetailsDto>> GetProducer(int id);
        Task<Response<ProducerUpdateDto>> GetProducerForUpdate(int id);
        Task<Response<int>> CreateProducer(ProducerCreateDto producer);
        Task<Response<int>> UpdateProducer(int id,ProducerUpdateDto producer);
        Task<Response<int>> DeleteProducer(int id);
    }
}