using Hexagonale.Movies.API.Dtos;
using Hexagonale.Movies.Domain.Ports;
using Microsoft.AspNetCore.Mvc;

namespace Hexagonale.Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavorisController : ControllerBase
    {
        //1- Properties
        private readonly IFavorisService _favorisService;
        private readonly ILogger<FavorisController> _logger;

        //2- Constructor
        public FavorisController(IFavorisService favorisService, ILogger<FavorisController> logger)
        {
            _favorisService = favorisService ?? throw new ArgumentNullException(nameof(favorisService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        //3- Methods
        [HttpPost("create-Favori")]
        public async Task<IActionResult> AjouterFavori([FromBody] FavoriRequest request)
        {
            await _favorisService.AjouterFavori(request.UtilisateurId, request.FilmId);
            _logger.LogInformation($"Favori ajouté pour l'utilisateur {request.UtilisateurId} et le film {request.FilmId}");
            return Ok();
        }

       
        [HttpDelete("delete-Favori")]
        public async Task<IActionResult> RetirerFavori([FromBody] FavoriRequest request)
        {
            await _favorisService.RetirerFavori(request.UtilisateurId,request.FilmId);
            _logger.LogInformation($"Favori retiré pour l'utilisateur {request.UtilisateurId} et le film {request.FilmId}");
            return Ok();
        }

        [HttpPost("marque-commeVu")]
        public async Task<IActionResult> MarquerCommeVu([FromBody] FavoriRequest request)
        {
            await _favorisService.MarquerCommeVu(request.UtilisateurId, request.FilmId);
            _logger.LogInformation($"Film {request.FilmId} marqué comme vu pour l'utilisateur {request.UtilisateurId}");
            return Ok();
        }

        [HttpGet("{utilisateurId}")]
        public async Task<IActionResult> GetListerFavoris(int utilisateurId, string tri = "date")
        {
            var films = await _favorisService.ListerFavoris(utilisateurId, tri);
            if (films == null || !films.Any())
            {
                _logger.LogInformation($"Aucun favori trouvé pour l'utilisateur {utilisateurId} avec le tri {tri}.");
                return NotFound("Aucun favori trouvé.");
            }
            return Ok(films);
        }

        [HttpGet("favoris-vus/{utilisateurId}")]
        public async Task<IActionResult> GetListerVus(int utilisateurId)
        {
            var films = await _favorisService.ListerVus(utilisateurId);
            if (films == null || !films.Any())
            {
                _logger.LogInformation($"Aucun film vu trouvé pour l'utilisateur {utilisateurId}.");
                return NotFound("Aucun film vu trouvé.");
            }
            return Ok(films);
        }

        [HttpGet("favoris-non-vus/{utilisateurId}")]
        public async Task<IActionResult> GetListerNonVus(int utilisateurId)
        {
           var films = await _favorisService.ListerNonVus(utilisateurId);
            if (films == null || !films.Any())
            {
                _logger.LogInformation($"Aucun film non vu trouvé pour l'utilisateur {utilisateurId}.");
                return NotFound("Aucun film non vu trouvé.");
            }
            return Ok(films);
        }

    }
}
