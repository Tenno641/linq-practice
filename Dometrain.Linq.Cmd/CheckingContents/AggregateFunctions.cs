namespace Dometrain.Linq.Cmd.CheckingContents;

public class AggregateFunctions : QueryRunner
{
    public override void Run()
    {
        // MinimumValue();
        // MinimumItem();
        // MaximumValue();
        // MaximumItem();
        // AverageValue();
        // SumValue();
        // CountItems();
        // AggregateFunctionsWithGroupBy_Q();
        AggregateFunctionsWithGroupBy_F();
    }
    

    /// <summary>
    /// Get the minimum value for a certain expression
    /// </summary>
    void MinimumValue()
    {
        var sourceMovies = Repository.GetAllMovies();

        var firstReleaseDate = sourceMovies
            .Min(movie => movie.ReleaseDate);
        
        Print(firstReleaseDate);
    }
    
    /// <summary>
    /// Get the item with the minimum value for a certain expression
    /// </summary>
    void MinimumItem()
    {
        var sourceMovies = Repository.GetAllMovies();

        var firstReleaseDate = sourceMovies
            .MinBy(movie => movie.ReleaseDate);

        Print(firstReleaseDate);
    }
    
    /// <summary>
    /// Get the maximum value for a certain expression
    /// </summary>
    void MaximumValue()
    {
        var sourceMovies = Repository.GetAllMovies();

        var lastReleaseDate = sourceMovies
            .Where(movie => movie.Phase == 1)
            .Max(movie => movie.ReleaseDate);
        
        Print(lastReleaseDate);
        
    }
    
    /// <summary>
    /// Get the item with the maximum value for a certain expression
    /// </summary>
    void MaximumItem()
    {
        var sourceMovies = Repository.GetAllMovies();

        var lastReleaseDate = sourceMovies
            .Where(movie => movie.Phase == 2)
            .MaxBy(movie => movie.ReleaseDate);
        
        Print(lastReleaseDate);
    }
    
    /// <summary>
    /// Get the average value for a certain expression
    /// </summary>
    void AverageValue()
    {
        var sourceMovies = Repository.GetAllMovies();

        var averageProducers = sourceMovies
            .Average(movie => movie.Producers.Count);
        
        Print(averageProducers);
    }
    
    /// <summary>
    /// Get the added value for a certain expression
    /// </summary>
    void SumValue()
    {
        var sourceMovies = Repository.GetAllMovies();

        var totalProducers = sourceMovies
            .Sum(movie => movie.Producers.Count);
        
        Print(totalProducers);
    }
    
    /// <summary>
    /// Count the number of items
    /// </summary>
    void CountItems()
    {
        var sourceMovies = Repository.GetAllMovies();

        var countProducers = sourceMovies
            .Count();

        Print(countProducers);
    }
    
    /// <summary>
    /// Combining the use of aggregate functions with a group by, query syntax
    /// </summary>
    void AggregateFunctionsWithGroupBy_Q()
    {
        var sourceMovies = Repository.GetAllMovies();

        var groupedQuery =
            from movie in sourceMovies
            group movie by movie.Phase
            into movies
            select new
            {
                Movies = movies,
                LastMovie = movies.Max(movie => movie.ReleaseDate)
            };

        foreach (var group in groupedQuery)
        {
            Print($"Phase {group.Movies.Key} Until {group.LastMovie}");
            foreach (var movie in group.Movies)
            {
                Print(movie);
            } 
        }

    }
    
    /// <summary>
    /// Combining the use of aggregate functions with a group by, fluent syntax
    /// </summary>
    void AggregateFunctionsWithGroupBy_F()
    {
        var sourceMovies = Repository.GetAllMovies();

        var groupQuery = sourceMovies
            .GroupBy(movie => movie.Phase)
            .Select(group => new { Movies = group, LastMovie = group.Max(movie => movie.ReleaseDate) });

        foreach (var group in groupQuery)
        {
            Print($"Phase {group.Movies.Key} Until {group.LastMovie}");
            foreach (var movie in group.Movies)
            {
                Print(movie);
            }
        }
    }
}