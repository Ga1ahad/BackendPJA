using Clothesy.Application.Persistence;
using Clothesy.Domain.Entities;
using Clothesy.WeatherApiService;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clothesy.Application.Suitcases.Commands
{
    public class CreateSuitcaseCommand : IRequest<int>
    {        
        public int idTrip { get; set; }
    }

    public class CreateClothesCommandHandler : IRequestHandler<CreateSuitcaseCommand, int>
    {
        private readonly IClothesyDb _context;
        private WeatherService _weather;
        public CreateClothesCommandHandler(IClothesyDb context)
        {
            _context = context;
            _weather = new WeatherService();
        }

        public async Task<int> Handle(CreateSuitcaseCommand request, CancellationToken cancellationToken)
        {
            var trip = _context.Trip.FirstOrDefault(x => x.idTrip == request.idTrip);
            var generalClothes = _context.Clothing.Where(x => x.idUser == trip.idUser);
            trip.SuitcaseGenerated = true;
            var suitcase = new Suitcase();
            suitcase.Name = "Walizka dla " + trip.TripName + " podrózy od " + trip.StartTrip.ToShortDateString() + " do " + trip.EndTrip.ToShortDateString();
            suitcase.idTrip = request.idTrip;

            var random = new Random();
            var weather = await _weather.LoadWeatherInformation(trip.City, trip.StartTrip.ToString(), trip.EndTrip.ToString());


            //pory roku
            var winterClothes = generalClothes.Where(x => x.ClothingTag.Any(y => y.idTagNavigation.TagName == "zima"));
            var springClothes = generalClothes.Where(x => x.ClothingTag.Any(y => y.idTagNavigation.TagName == "wiosna"));
            var summerClothes = generalClothes.Where(x => x.ClothingTag.Any(y => y.idTagNavigation.TagName == "lato"));
            var lightClothes = generalClothes.Where(x => x.ClothingTag.Any(y => y.idTagNavigation.TagName == "wiosna" || y.idTagNavigation.TagName == "lato" || y.idTagNavigation.TagName == "jesien"));

            //general clothes
            var hoodies = generalClothes.Where(x => x.idClothingTypeNavigation.Name == "bluza").ToList();
            var tshirts = generalClothes.Where(x => x.idClothingTypeNavigation.Name == "t-shirt").ToList();
            var trousers = generalClothes.Where(x => x.idClothingTypeNavigation.Name == "spodnie").ToList();
            var lightShoes = lightClothes.Where(x => x.idClothingTypeNavigation.Name == "buty").ToList();

            //zima
            
            var winterHats = winterClothes.Where(x => x.idClothingTypeNavigation.Name == "czapka").ToList();
            var winterJackets = winterClothes.Where(x => x.idClothingTypeNavigation.Name == "kurtka").ToList();            
            var winterShoes = winterClothes.Where(x => x.idClothingTypeNavigation.Name == "buty").ToList();

            //wiosna
            
            var springJackets = springClothes.Where(x => x.idClothingTypeNavigation.Name == "kurtka").ToList();

            //lato
            
            var summerHats = summerClothes.Where(x => x.idClothingTypeNavigation.Name == "czapka").ToList();            
            var shorts = generalClothes.Where(x => x.idClothingTypeNavigation.Name == "spodenki").ToList();


            var result = "";
            var countOfgenClothes = generalClothes.Count();
            var weatherCount = weather.Count();

            var siema1 = hoodies.Count();

            if (weatherCount <= hoodies.Count()
                /*&& weatherCount <= tshirts.Count()
                && weatherCount <= trousers.Count()
                && weatherCount <= lightShoes.Count()
                && weatherCount <= winterHats.Count()
                && weatherCount <= winterJackets.Count()
                && weatherCount <= winterShoes.Count()
                && weatherCount <= springJackets.Count()
                && weatherCount <= summerHats.Count()
                && weatherCount <= shorts.Count()*/
                )
            {
                foreach (var temp in weather)
                {
                    if (temp.avgTempC < 0)
                    {
                        var randomWinterHat = random.Next(winterHats.Count);
                        var randomWinterJacket = random.Next(winterJackets.Count);
                        var randomHoodie = random.Next(hoodies.Count);
                        var randomTshirt = random.Next(tshirts.Count);
                        var randomTrouser = random.Next(trousers.Count);
                        var randomWinterShoes = random.Next(winterShoes.Count);

                        var winterHatId = winterHats[randomWinterHat].idClothing.ToString();
                        var winterJacketId = winterJackets[randomWinterJacket].idClothing.ToString();
                        var hoodieId = hoodies[randomHoodie].idClothing.ToString();
                        var tshirtId = tshirts[randomTshirt].idClothing.ToString();
                        var trouserId = trousers[randomTrouser].idClothing.ToString();
                        var winterShoesId = winterShoes[randomWinterShoes].idClothing.ToString();

                        winterHats.RemoveAt(randomWinterHat);
                        winterJackets.RemoveAt(randomWinterJacket);
                        hoodies.RemoveAt(randomHoodie);
                        tshirts.RemoveAt(randomTshirt);
                        trousers.RemoveAt(randomTrouser);
                        winterShoes.RemoveAt(randomWinterShoes);

                        result = winterHatId + "," + winterJacketId + "," + hoodieId + "," + tshirtId + "," + trouserId + "," + winterShoesId;
                    }

                    if (temp.avgTempC > 0 && temp.avgTempC < 10)
                    {
                        var randomSpringJacket = random.Next(springJackets.Count);
                        var randomHoodie = random.Next(hoodies.Count);
                        var randomTshirt = random.Next(tshirts.Count);
                        var randomTrouser = random.Next(trousers.Count);
                        var randomPairOfLightShoes = random.Next(lightShoes.Count);

                        var springJacketId = springJackets[randomSpringJacket].idClothing.ToString();
                        var hoodieId = hoodies[randomHoodie].idClothing.ToString();
                        var tshirtId = tshirts[randomTshirt].idClothing.ToString();
                        var trouserId = trousers[randomTrouser].idClothing.ToString();
                        var pairOfLightShoesId = lightShoes[randomPairOfLightShoes].idClothing.ToString();

                        springJackets.RemoveAt(randomSpringJacket);
                        hoodies.RemoveAt(randomHoodie);
                        tshirts.RemoveAt(randomTshirt);
                        trousers.RemoveAt(randomTrouser);
                        lightShoes.RemoveAt(randomPairOfLightShoes);

                        result = springJacketId + "," + hoodieId + "," + tshirtId + "," + trouserId + "," + pairOfLightShoesId;
                    }

                    if (temp.avgTempC < 20 && temp.avgTempC > 10)
                    {
                        var randomHoodie = random.Next(hoodies.Count);
                        var randomTshirt = random.Next(tshirts.Count);
                        var randomTrouser = random.Next(trousers.Count);
                        var randomShoes = random.Next(lightShoes.Count);

                        var hoodieId = hoodies[randomHoodie].idClothing.ToString();
                        var tshirtId = tshirts[randomTshirt].idClothing.ToString();
                        var trouserId = trousers[randomTrouser].idClothing.ToString();
                        var pairOfLightShoesId = lightShoes[randomShoes].idClothing.ToString();

                        hoodies.RemoveAt(randomHoodie);
                        tshirts.RemoveAt(randomTshirt);
                        trousers.RemoveAt(randomTrouser);
                        lightShoes.RemoveAt(randomShoes);

                        result = hoodieId + "," + tshirtId + "," + trouserId + "," + pairOfLightShoesId;
                    }

                    if (temp.avgTempC > 20)
                    {
                        var randomSummerHat = random.Next(summerHats.Count);
                        var randomTshirt = random.Next(tshirts.Count);
                        var randomShorts = random.Next(shorts.Count);
                        var randomShoes = random.Next(lightShoes.Count);

                        var summerHatId = summerHats[randomSummerHat].idClothing.ToString();
                        var tshirtId = tshirts[randomTshirt].idClothing.ToString();
                        var pairOfShortsId = shorts[randomShorts].idClothing.ToString();
                        var pairOfLightShoesId = lightShoes[randomShoes].idClothing.ToString();

                        summerHats.RemoveAt(randomSummerHat);
                        tshirts.RemoveAt(randomTshirt);
                        shorts.RemoveAt(randomShorts);
                        lightShoes.RemoveAt(randomShoes);

                        result = summerHatId + "," + tshirtId + "," + pairOfShortsId + "," + pairOfLightShoesId;
                    }

                    string[] clothes = result.Split(',');

                    foreach (var clothing in clothes)
                    {
                        suitcase.ClothingSuitcase.Add(new ClothingSuitcase { idClothing = Int32.Parse(clothing), idSuitcase = suitcase.idSuitcase });
                    }
                    result = "";
                }
            }
            else
            {
                throw new Exception("Nie ma wystarczająco ubrań w bazie danych");
            }   

            _context.Suitcase.Add(suitcase);
            await _context.SaveChangesAsync(cancellationToken);
            return suitcase.idSuitcase;
        }
    }
}
