using Clothesy.Application.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Clothesy.Api.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected IClothesyDb Context { get; }

        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public BaseController(IClothesyDb context)
        {
            Context = context;
        }

    }
}
