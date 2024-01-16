using AutoMapper;
using CarWorkshop.Domain.Interfaces;
using MediatR;

namespace CarWorkshop.Application.CarWorkhsop.Queries.GetAllCarWorkshops
{
    public class GetAllCarWorkshopsHandler : IRequestHandler<GetAllCarWorkshopsQuery, IEnumerable<CarWorkshopDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICarWorkshopRepository _repository;

        public GetAllCarWorkshopsHandler(IMapper mapper, ICarWorkshopRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }


        public async Task<IEnumerable<CarWorkshopDto>> Handle(GetAllCarWorkshopsQuery request, CancellationToken cancellationToken)
        {
            var carworkshops = await _repository.GetAll();
            var dtos = _mapper.Map<IEnumerable<CarWorkshopDto>>(carworkshops);

            return dtos;
        }
    }
}
