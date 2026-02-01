using System.Runtime.CompilerServices;
using Dometrain.Linq.Data.Models;

namespace Dometrain.Linq.Cmd._3_FilteringAndOrdering;

public class WhereConditions : QueryRunner
{
    public override void Run()
    {
        SingleCondition_Q();
        SingleCondition_F();
        SingleFunctionCondition_Q();
        SingleFunctionCondition_F();
        MultipleConditions_Q();
        MultiplesConditions_F();
    }

    /// <summary>
    /// Single condition, query syntax
    /// </summary>
    private void SingleCondition_Q()
    {
        var sourceMovies = Repository.GetAllMovies();
        
        var filteredMovies =
            from movie in sourceMovies
            where movie.Name.Contains("spider", StringComparison.InvariantCultureIgnoreCase)
            select movie;
        
        PrintAll(filteredMovies);
    }
    
    /// <summary>
    /// Single condition, fluent syntax
    /// </summary>
    private void SingleCondition_F()
    {
        var sourceMovies = Repository.GetAllMovies();
        
        var filteredMovies = sourceMovies
            .Where(movie => movie.Name.Contains("iron", StringComparison.InvariantCultureIgnoreCase));
        
        PrintAll(filteredMovies);
    }

    private static bool Predicate(Movie movie, string title) => movie.Name.Contains(title, StringComparison.InvariantCultureIgnoreCase);
    
    /// <summary>
    /// Single condition from a function, query syntax
    /// </summary>
    private void SingleFunctionCondition_Q()
    {
        var sourceMovies = Repository.GetAllMovies();
        
        var filteredMovies =
            from movie in sourceMovies
            where Predicate(movie, "spider")
            select movie;
        
        PrintAll(filteredMovies);
    }
    
    /// <summary>
    /// Single condition from a function, fluent syntax
    /// </summary>
    private void SingleFunctionCondition_F()
    {
        var sourceMovies = Repository.GetAllMovies();
        
        var filteredMovies = sourceMovies
            .Where(movie => Predicate(movie, "iron"));
        
        PrintAll(filteredMovies);
    }
    
    /// <summary>
    /// Multiple chained conditions, query syntax
    /// </summary>
    private void MultipleConditions_Q()
    {
        var sourceMovies = Repository.GetAllMovies();

        var filteredMovies =
            from movie in sourceMovies
            where Predicate(movie, "spider")
            where movie.ReleaseDate.Year > 2000
            select movie;
        
        PrintAll(filteredMovies);
    }
    
    /// <summary>
    /// Multiple chained conditions, fluent syntax
    /// </summary>
    private void MultiplesConditions_F()
    {
        var sourceMovies = Repository.GetAllMovies();

        var filteredMovies = sourceMovies
            .Where(movie => Predicate(movie, "iron"))
            .Where(movie => movie.ReleaseDate.Year > 2000);
        
        PrintAll(filteredMovies);
    }
}