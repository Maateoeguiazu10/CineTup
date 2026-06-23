using CineTup.Application.Requests;
using CineTup.Application.Responses;

namespace CineTup.Application.Abstractions
{
    public interface IShowTimeService
    {
        List<ShowTimeResponse> GetAll();
        ShowTimeResponse GetById(int id);
        ShowTimeResponse Create(ShowTimeRequest request);
        void Update(ShowTimeRequest request, int id);
        void Delete(int id);
    }
}