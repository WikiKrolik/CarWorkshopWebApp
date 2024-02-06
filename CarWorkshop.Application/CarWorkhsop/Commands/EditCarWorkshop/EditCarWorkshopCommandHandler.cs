using CarWorkshop.Application.ApplicationUser;
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
        private readonly IUserContext _userContext;

        public EditCarWorkshopCommandHandler(ICarWorkshopRepository repository, IUserContext userContext)
        {
            _repository = repository;
            _userContext = userContext;
        }

        async Task<Unit> IRequestHandler<EditCarWorkshopCommand, Unit>.Handle(EditCarWorkshopCommand request, CancellationToken cancellationToken)
        {
            var workshop = await _repository.GetByEncodedName(request.EncodedName!);

            var user = _userContext.GetCurrentUser();
            var isEditable = user != null && (workshop.CreatedById == user.Id || user.IsInRole("Moderator"));

            if (!isEditable) 
            {
                return Unit.Value;

            }

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
