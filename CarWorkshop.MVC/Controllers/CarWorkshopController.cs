using AutoMapper;
using CarWorkshop.Application.CarWorkhsop;
using CarWorkshop.Application.CarWorkhsop.Commands.CreateCarWorkshop;
using CarWorkshop.Application.CarWorkhsop.Commands.EditCarWorkshop;
using CarWorkshop.Application.CarWorkhsop.Queries.GetAllCarWorkshops;
using CarWorkshop.Application.CarWorkhsop.Queries.GetCarWorkshopByEncodedName;
using CarWorkshop.Application.CarWorkshopService.Commands;
using CarWorkshop.Application.CarWorkshopService.Queries;
using CarWorkshop.MVC.Extensions;
using CarWorkshop.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarWorkshop.MVC.Controllers
{
    public class CarWorkshopController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public CarWorkshopController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var carWorkshops = await _mediator.Send(new GetAllCarWorkshopsQuery());
            return View(carWorkshops);
        }



        [Authorize(Roles = "Owner")]
        public IActionResult Create()
        {
            return View();
        }



        [Route("CarWorkshop/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName)
        {
            var dto = await _mediator.Send(new GetCarWorkshopByEncodedNameQuery(encodedName));
            if (!dto.IsEditable)
            {
                return RedirectToAction("NoAcces", "Home");
            }
            EditCarWorkshopCommand model = _mapper.Map<EditCarWorkshopCommand>(dto);


            return View(model);
        }


        [HttpPost]
        [Route("CarWorkshop/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string EncodedName, EditCarWorkshopCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);

            }
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }


        [Route("CarWorkshop/{encodedName}/Details")]
        public async Task<IActionResult> Details(string encodedName)
        {
            var dto = await _mediator.Send(new GetCarWorkshopByEncodedNameQuery(encodedName));
            return View(dto);
        }

        [Authorize(Roles = "Owner")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCarWorkshopCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);

            }

            await _mediator.Send(command);
            this.SetNotification("success", $"Created workshop: {command.Name}");

            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Owner")]
        [HttpPost]
        [Route("CarWorkshop/CarWorkshopService")]
        public async Task<IActionResult> CreateCarWorkshopService(CreateCarWorkshopServiceCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        [Route("CarWorkshop/{encodedName}/CarWorkshopService")]
        public async Task<IActionResult> GetCarWorkshopServices(string encodedName)
        {
            var data = await _mediator.Send(new GetCarWorkshopServicesQuery() { EncodedName = encodedName });
            return Ok(data);
        }

    }

}
