using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicApp.DAL.Migrations
{
    public partial class SeedMusicsAndArtistsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Artists (Name) Values ('Arctic Monkeys')");
            migrationBuilder.Sql("INSERT INTO Artists (Name) Values ('Linkin Park')");
            migrationBuilder.Sql("INSERT INTO Artists (Name) Values ('Limp Bizkit')");
            migrationBuilder.Sql("INSERT INTO Artists (Name) Values ('Gruppa Skryptonite')");
                
            migrationBuilder.Sql("INSERT INTO Musics (Name, ArtistId) Values ('R U Mine?', (SELECT Id FROM Artists WHERE Name = 'Arctic Monkeys'))");
            migrationBuilder.Sql("INSERT INTO Musics (Name, ArtistId) Values ('Tranquility Base Hotel & Casino', (SELECT Id FROM Artists WHERE Name = 'Arctic Monkeys'))");
            migrationBuilder.Sql("INSERT INTO Musics (Name, ArtistId) Values ('Why Did You Only Call Me When You Are High?', (SELECT Id FROM Artists WHERE Name = 'Arctic Monkeys'))");
            migrationBuilder.Sql("INSERT INTO Musics (Name, ArtistId) Values ('505', (SELECT Id FROM Artists WHERE Name = 'Arctic Monkeys'))");
            migrationBuilder.Sql("INSERT INTO Musics (Name, ArtistId) Values ('Crying Lightning', (SELECT Id FROM Artists WHERE Name = 'Arctic Monkeys'))");
            
            migrationBuilder.Sql("INSERT INTO Musics (Name, ArtistId) Values ('In The End', (SELECT Id FROM Artists WHERE Name = 'Linkin Park'))");
            migrationBuilder.Sql("INSERT INTO Musics (Name, ArtistId) Values ('Slip', (SELECT Id FROM Artists WHERE Name = 'Linkin Park'))");
            migrationBuilder.Sql("INSERT INTO Musics (Name, ArtistId) Values ('Crawling', (SELECT Id FROM Artists WHERE Name = 'Linkin Park'))");
            migrationBuilder.Sql("INSERT INTO Musics (Name, ArtistId) Values ('Somewhere I Belong', (SELECT Id FROM Artists WHERE Name = 'Linkin Park'))");
            migrationBuilder.Sql("INSERT INTO Musics (Name, ArtistId) Values ('When They Come for Me', (SELECT Id FROM Artists WHERE Name = 'Linkin Park'))");
            
            migrationBuilder.Sql("INSERT INTO Musics (Name, ArtistId) Values ('My Way', (SELECT Id FROM Artists WHERE Name = 'Limp Bizkit'))");
            migrationBuilder.Sql("INSERT INTO Musics (Name, ArtistId) Values ('Rollin', (SELECT Id FROM Artists WHERE Name = 'Limp Bizkit'))");
            migrationBuilder.Sql("INSERT INTO Musics (Name, ArtistId) Values ('Gold Cobra', (SELECT Id FROM Artists WHERE Name = 'Limp Bizkit'))");
            migrationBuilder.Sql("INSERT INTO Musics (Name, ArtistId) Values ('Shotgun', (SELECT Id FROM Artists WHERE Name = 'Limp Bizkit'))");
            migrationBuilder.Sql("INSERT INTO Musics (Name, ArtistId) Values ('Break Stuff', (SELECT Id FROM Artists WHERE Name = 'Limp Bizkit'))");
            
            migrationBuilder.Sql("INSERT INTO Musics (Name, ArtistId) Values ('Latinskaya Muzyka', (SELECT Id FROM Artists WHERE Name = 'Gruppa Skryptonite'))");
            migrationBuilder.Sql("INSERT INTO Musics (Name, ArtistId) Values ('Podruga', (SELECT Id FROM Artists WHERE Name = 'Gruppa Skryptonite'))");
            migrationBuilder.Sql("INSERT INTO Musics (Name, ArtistId) Values ('Karusel', (SELECT Id FROM Artists WHERE Name = 'Gruppa Skryptonite'))");
            migrationBuilder.Sql("INSERT INTO Musics (Name, ArtistId) Values ('Beregom', (SELECT Id FROM Artists WHERE Name = 'Gruppa Skryptonite'))");
            migrationBuilder.Sql("INSERT INTO Musics (Name, ArtistId) Values ('Glupye I Nenuzhnye', (SELECT Id FROM Artists WHERE Name = 'Gruppa Skryptonite'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Musics");

            migrationBuilder.Sql("DELETE FROM Artists");
        }
    }
}
