using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
const string GetGameEndPointName = "GetGame";

List<GameDto> games = [
    new (
        1, 
        "Street Fighter 1",
        "Fighting",
        19.99M,
        new DateOnly(1993,7,14)
    ),
    new (
        2, 
        "Street Fighter 11",
        "Fighting",
        19.99M,
        new DateOnly(1993,7,14)
    ),
    new (
        3, 
        "Street Fighter 111",
        "Fighting",
        19.99M,
        new DateOnly(1993,7,14)
    ),

];

// GET /games
app.MapGet("games", ()=> games);

// GET /game/1
app.MapGet("games/{id}", (int id)=> games.Find(game => game.Id == id)).WithName(GetGameEndPointName);

//POST /games
app.MapPost("games", (CreateGameDto newGame)=> 
{
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate  
    );
    games.Add(game);
    return Results.CreatedAtRoute(GetGameEndPointName, new {id= game.Id}, game );
});

// PUT /games
app.MapPut("games/{id}", (int id, UpdateGameDto updateGame) => {
    var index = games.FindIndex(game => game.Id == id);
    games[index] = new GameDto(
        id,
        updateGame.Name,
        updateGame.Genre,
        updateGame.Price,
        updateGame.ReleaseDate

    );
    return Results.NoContent();
});

// DELETE /games/1
app.MapDelete("games/{id}", (int id)=> 
{
  games.RemoveAll(game => game.Id == id);

  return Results.NoContent;
});


app.Run();
