using Dometrain.Linq.Data.Models;

namespace Dometrain.Linq.Cmd.FilteringAndOrdering;

public class Ordering : QueryRunner
{
    public override void Run()
    {
        SingleOrderBy_F();
        SingleOrderBy_Q();
        
        SingleOrderByDescending_F();
        SingleOrderByDescending_Q();
        
        MultipleOrderBy_Q();
        MultipleOrderBy_F();
        
        OrderByCustomComparer_F();
    }
    
    /// <summary>
    /// Single order by, query syntax
    /// </summary>
    private void SingleOrderBy_Q()
    {
        var sourceMovies = Repository.GetAllMovies();

        var result = from movie in sourceMovies
            orderby movie.Name
            select movie;
        
        PrintAll(result);
    }
    
    /// <summary>
    /// Single order by (descending), query syntax
    /// </summary>
    private void SingleOrderByDescending_Q()
    {
        var sourceMovies = Repository.GetAllMovies();

        var result =
            from movie in sourceMovies
            orderby movie.Name descending
            select movie;
        
        PrintAll(result);
    }
    
    /// <summary>
    /// Single order by, fluent syntax
    /// </summary>
    private void SingleOrderBy_F()
    {
        var sourceMovies = Repository.GetAllMovies();

        var result = sourceMovies
            .OrderBy(movie => movie.Name);

        PrintAll(result);
    }
    
    /// <summary>
    /// Single order by (descending), fluent syntax
    /// </summary>
    private void SingleOrderByDescending_F()
    {
        var sourceMovies = Repository.GetAllMovies();

        var result = sourceMovies
            .OrderByDescending(movie => movie.Name);
        
        PrintAll(result);
    }
    
    /// <summary>
    /// Multiple order by, query syntax
    /// </summary>
    private void MultipleOrderBy_Q()
    {
        var sourceMovies = Repository.GetAllMovies();

        var result =
            from movie in sourceMovies
            orderby movie.Name, movie.ReleaseDate.Year
            select movie;

        PrintAll(result);
    }
    
    /// <summary>
    /// Multiple order by, fluent syntax
    /// </summary>
    private void MultipleOrderBy_F()
    {
        var sourceMovies = Repository.GetAllMovies();

        var result = sourceMovies
            .OrderBy(movie => movie.Name)
            .ThenByDescending(movie => movie.ReleaseDate.Year);
        
        PrintAll(result);
    }
    
    /// <summary>
    /// Single order by using a custom comparer, fluent syntax
    /// </summary>
    private void OrderByCustomComparer_F()
    {
        var sourceMovies = Repository.GetAllMovies();

        var result = sourceMovies
            .OrderBy(movie => movie, new MovieComparer());
        
        PrintAll(result);
    }
}

class MovieComparer : IComparer<Movie>
{
    public int Compare(Movie? x, Movie? y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (y is null) return 1;
        if (x is null) return -1;

        int movieIdComparison = x.MovieId.CompareTo(y.MovieId);
        if (movieIdComparison != 0) return movieIdComparison;

        int nameComparison = string.Compare(x.Name, y.Name, StringComparison.Ordinal);
        if (nameComparison != 0) return nameComparison;

        return x.ReleaseDate.CompareTo(y.ReleaseDate);
    }
}