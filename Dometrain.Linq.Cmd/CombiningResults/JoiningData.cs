using Dometrain.Linq.Data.Models;

namespace Dometrain.Linq.Cmd.CombiningResults;

public class JoiningData : QueryRunner
{
    public override void Run()
    {
        // InnerJoin_Q();
        // InnerJoin_F();
        // InnerJoinMultiField_Q();
        // InnerJoinMultiField_F();
        // GroupJoin_Q();
        // GroupJoin_F();
        // LeftOuterJoin_Q();
        // RightOuterJoin_Q();
        LeftOuterJoin_F();
    }

    /// <summary>
    /// Inner join data, query syntax
    /// </summary>
    void InnerJoin_Q()
    {
        var allMovies = Repository.GetAllMovies();
        var castMembers = Repository.GetSomeCastMembers();

        var result =
            from movie in allMovies
            join cast in castMembers on movie.Name equals cast.Movie
            select new
            {
                Title = movie.Name,
                ReleaseYear = movie.ReleaseDate.Year,
                Actor = cast.Actor
            };

        foreach (var movie in result)
        {
            Print($"Title : {movie.Title} - Year : {movie.ReleaseYear} - Actor : {movie.Actor}");
        }
    }
    
    /// <summary>
    /// Inner join data, fluent syntax
    /// </summary>
    void InnerJoin_F()
    {
        var allMovies = Repository.GetAllMovies();
        var castMembers = Repository.GetSomeCastMembers();

        var result = allMovies
            .Join(castMembers, movie => movie.Name, castMember => castMember.Movie, (movie, castMember) => new
            {
               Title = movie.Name,
               ReleaseYear = movie.ReleaseDate.Year,
               Actor = castMember.Actor 
            });
        
        foreach (var movie in result)
        {
            Print($"Title : {movie.Title} - Year : {movie.ReleaseYear} - Actor : {movie.Actor}");
        }
    }
    
    /// <summary>
    /// Inner join data on multiple properties, query syntax
    /// </summary>
    void InnerJoinMultiField_Q()
    {
        var allMoviesDirectors = Repository.GetAllMovies()
            .SelectMany(movie => movie.Directors, (movie, director) => new { Movie = movie, Director = director });
        
        var directors = Repository.GetSomeDirectors();

        var result =
            from movieDirector in allMoviesDirectors
            join director in directors
                on new { movieDirector.Director.FirstName, movieDirector.Director.LastName } equals // custom anonymous type for multiple inner properties 
                new { director.FirstName, director.LastName } // director only accessible here
            select new
            {
                Movie = movieDirector.Movie,
                Director = director
            };

        foreach (var movieDirector in result)
        {
            Console.WriteLine($"Title : {movieDirector.Movie.Name} - Director {movieDirector.Director}");
        }
    }
    
    /// <summary>
    /// Inner join data on multiple properties, fluent syntax
    /// </summary>
    void InnerJoinMultiField_F()
    {
        var movieDirectors = Repository.GetAllMovies()
            .SelectMany(movie => movie.Directors, (movie, director)=> new
            {
                Movie = movie,
                Director = director 
            });
        
        var directors = Repository.GetSomeDirectors();

        var result = movieDirectors
            .Join(directors, movieDirector => new { movieDirector.Director.FirstName, movieDirector.Director.LastName },
                director => new { director.FirstName, director.LastName },
                (movieDirector, director) => new 
                {
                    Movie = movieDirector.Movie,
                    Director = director
                });


        foreach (var movieDirector in result)
        {
            Print($"Title : {movieDirector.Movie.Name} - Director : {movieDirector.Director.FullName}");
        }
    }
    
    /// <summary>
    /// Group join data, query syntax
    /// </summary>
    void GroupJoin_Q()
    {
        var allMovies = Repository.GetAllMovies();
        var castMembers = Repository.GetSomeCastMembers();

        var result =
            from movie in allMovies.DefaultIfEmpty()
            join castMember in castMembers
                on movie.Name equals castMember.Movie
            into cast
            select new
            {
                Movie = movie.Name,
                Cast = cast
            };

        foreach (var movieCast in result)
        {
            Print(movieCast.Movie);
            foreach (var cast in movieCast.Cast)
            {
                Print(cast.Actor);
            }
        }
    }
    
    /// <summary>
    /// Group join data, fluent syntax
    /// </summary>
    void GroupJoin_F()
    {
        var allMovies = Repository.GetAllMovies();
        var castMembers = Repository.GetSomeCastMembers();

        var result = allMovies
            .GroupJoin(castMembers,
                movie => movie.Name,
                castMember => castMember.Movie,
                (movie, cast) => new
                {
                    Movie = movie.Name,
                    Cast = cast
                });
        
        foreach (var movieCast in result)
        {
            Print(movieCast.Movie);
            foreach (var cast in movieCast.Cast)
            {
                Print(cast.Actor);
            }
        }
    }
    
    /// <summary>
    /// Left outer join data, query syntax
    /// </summary>
    void LeftOuterJoin_Q()
    {
        var allMovies = Repository.GetAllMovies();
        var castMembers = Repository.GetSomeCastMembers();

        var result =
            from movie in allMovies
            join castMember in castMembers
                on movie.Name equals
                castMember.Movie
                into cast
            from member in cast.DefaultIfEmpty()
            select new
            {
                Movie = movie.Name,
                Actor = member?.Actor
            };

        foreach (var movie in result)
        {
            Print($"Movie : {movie.Movie} - Actor : {movie.Actor}");
        }
    }
    
    /// <summary>
    /// Right outer join data, query syntax
    /// </summary>
    void RightOuterJoin_Q()
    {
        var allMovies = Repository.GetAllMovies();
        var castMembers = Repository.GetSomeCastMembers();

        var result =
            from castMember in castMembers 
            join movie in allMovies 
                on castMember.Movie equals
                movie.Name
                into movies 
            from movie in movies.DefaultIfEmpty() 
            select new
            {
                Actor = castMember.Actor,
                Movie = movie?.Name
            };

        foreach (var movie in result)
        {
            Print($"Movie : {movie.Movie} - Actor : {movie.Actor}");
        }
    }
    
    /// <summary>
    /// Left outer join data, fluent syntax
    /// </summary>
    void LeftOuterJoin_F()
    {
        var allMovies = Repository.GetAllMovies();
        var castMembers = Repository.GetSomeCastMembers();

        var result = allMovies
            .GroupJoin(castMembers,
                movie => movie.Name,
                castMember => castMember.Movie,
                (movie, cast) => new
                {
                    Movie = movie.Name,
                    Cast = cast
                })
            .SelectMany(movieCast => movieCast.Cast.DefaultIfEmpty(),
                (movie, actor) => new
                {
                    Movie = movie.Movie,
                    Actor = actor 
                });

        foreach (var movieCast in result)
        {
            Print($"Movie : {movieCast.Movie} - Actor {movieCast.Actor?.Actor}");
        } 
        
    }
}