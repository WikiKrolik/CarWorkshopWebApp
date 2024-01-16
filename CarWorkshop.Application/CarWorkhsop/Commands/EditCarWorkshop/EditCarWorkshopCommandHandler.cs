using CarWorkshop.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkhsop.Commands.EditCarWorkshop
{
    public class EditCarWorkshopCommandHandler : IRequestHandler<EditCarWorkshopCommand>
    {
        private readonly ICarWorkshopRepository _repository;

        public EditCarWorkshopCommandHandler(ICarWorkshopRepository repository)
        {
            _repository = repository;
        }

        async Task<Unit> IRequestHandler<EditCarWorkshopCommand, Unit>.Handle(EditCarWorkshopCommand request, CancellationToken cancellationToken)
        {
            var workshop = await _repository.GetByEncodedName(request.EncodedName!);

            workshop.Description = request.Description;
            workshop.About = request.About;
            workshop.ContactDetails.City = request.City;
            workshop.ContactDetails.Street = request.Street;
            workshop.ContactDetails.PostalCode = request.PostalCode;
            workshop.ContactDetails.PhoneNumber = request.PhoneNumber;

            await _repository.Commit();

            return Unit.Value;
        }
    }
}
