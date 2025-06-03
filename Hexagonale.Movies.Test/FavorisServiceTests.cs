using Hexagonale.Movies.Application.Services;
using Hexagonale.Movies.Domain.Entities;
using Hexagonale.Movies.Domain.Ports;
using Moq;

namespace Hexagonale.Movies.Test
{
    public class FavorisServiceTests
    {

        //1. Ajout d’un film aux favoris
        [Fact]
        public async Task AjouterFavori_AjouteUnFavoriPourUtilisateur()
        {
            // Arrange
            var favorisRepoMock = new Mock<IFavorisRepository>();
            var filmRepoMock = new Mock<IFilmRepository>();
            var service = new FavorisService(favorisRepoMock.Object, filmRepoMock.Object);
            var utilisateurId = 2;
            var filmId = 1;

            // Act
            await service.AjouterFavori(utilisateurId, filmId);

            // Assert
            favorisRepoMock.Verify(r => r.AjouterFavori(It.Is<Favori>(f => f.UtilisateurId == utilisateurId && f.FilmId == filmId)), Times.Once);
            favorisRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        //2. Retrait d’un film des favoris
        [Theory]
        [InlineData(1, 4)]
        [InlineData(1, 2)]
        [InlineData(2, 3)]
        public async Task RetirerFavori_SupprimeLeFavori(int utilisateurId, int filmId)
        {
            //Arranger
            var favorisRepoMock = new Mock<IFavorisRepository>();
            var filmRepoMock = new Mock<IFilmRepository>();
            var service = new FavorisService(favorisRepoMock.Object, filmRepoMock.Object);


            // Act
            await service.RetirerFavori(utilisateurId, filmId);

            // Assert
            favorisRepoMock.Verify(r => r.RetirerFavori(utilisateurId, filmId), Times.Once);
            favorisRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        //3. Marquer un film comme vu
        [Theory]
        [InlineData(1, 4)]
        [InlineData(1, 2)]
        [InlineData(2, 3)]
        public async Task MarquerCommeVu_MetAJourLeFavori(int utilisateurId, int filmId)
        {
            //Arranger
            var favorisRepoMock = new Mock<IFavorisRepository>();
            var filmRepoMock = new Mock<IFilmRepository>();
          
            var favori = new Favori { UtilisateurId = utilisateurId, FilmId = filmId, Vu = false };

            favorisRepoMock.Setup(r => r.GetFavori(utilisateurId, filmId)).ReturnsAsync(favori);
            var service = new FavorisService(favorisRepoMock.Object, filmRepoMock.Object);

            // Act
            await service.MarquerCommeVu(utilisateurId, filmId);

            // Assert
            Assert.True(favori.Vu);
            Assert.NotNull(favori.DateVu);
            favorisRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        //4. Lister les favoris triés par note
        [Theory]
        [InlineData(1, 4)]
        [InlineData(1, 2)]
        [InlineData(2, 3)]
        public async Task ListerFavoris_TriParNote_OrdreCorrect(int utilisateurId, int filmId)
        {
            // Arrange
            var favorisRepoMock = new Mock<IFavorisRepository>();
            var filmRepoMock = new Mock<IFilmRepository>();
           
            var favoris = new List<Favori>
            {
                new Favori { UtilisateurId = utilisateurId, FilmId = filmId },
                new Favori { UtilisateurId = utilisateurId, FilmId = filmId }
            };
                    var films = new List<Film>
            {
                new Film { Id = filmId, Titre = "A", NoteGlobale = 7.0f },
                new Film { Id = filmId, Titre = "B", NoteGlobale = 9.0f }
            };

            //Act

            favorisRepoMock.Setup(r => r.GetFavoris(utilisateurId)).ReturnsAsync(favoris);
            filmRepoMock.Setup(r => r.GetFilms(It.IsAny<IEnumerable<int>>())).ReturnsAsync(films);
            var service = new FavorisService(favorisRepoMock.Object, filmRepoMock.Object);

            var result = (await service.ListerFavoris(utilisateurId, "note")).ToList();

            // Assert
            Assert.Equal("B", result[0].Titre); // Le film avec la meilleure note doit être en premier
        }

    }
}