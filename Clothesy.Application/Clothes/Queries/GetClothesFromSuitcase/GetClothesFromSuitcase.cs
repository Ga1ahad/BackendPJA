using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Clothesy.Domain.Entities;

namespace Clothesy.Application.Clothes.Queries.GetClothesFromSuitcase
{
    public class GetClothesFromSuitcase : IRequest<IEnumerable<Clothing>>
    {

    }
}
