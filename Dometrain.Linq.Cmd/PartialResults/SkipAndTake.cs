namespace Dometrain.Linq.Cmd.PartialResults;

public class SkipAndTake : QueryRunner
{
    public override void Run()
    {
        // TakeFirstItems();
        // TakeLastItems();
        // TakeMatchingItems();
        // SkipFirstItems();
        // SkipLastItems();
        // SkipMatchingItems();
        GetChunkUsingSkipAndTake();
    }
    
    /// <summary>
    /// Take the first X items from a source
    /// </summary>
    void TakeFirstItems()
    {
        var sourceMovies = Repository.GetAllMovies();

        var result = sourceMovies
            .Take(5);
        
        PrintAll(result);
    }
    
    /// <summary>
    /// Take the last X items from a source
    /// </summary>
    void TakeLastItems()
    {
        var sourceMovies = Repository.GetAllMovies();

        var result = sourceMovies
            .TakeLast(5);
        
        PrintAll(result);
    }
    
    /// <summary>
    /// Take items from a source while a condition is true
    /// </summary>
    void TakeMatchingItems()
    {
        var sourceMovies = Repository.GetAllMovies();

        var result = sourceMovies
            .TakeWhile(movie => movie.ReleaseDate.Year > 2000);

        PrintAll(result);
    }
    
    /// <summary>
    /// Skip the first X items from a source
    /// </summary>
    void SkipFirstItems()
    {
        var sourceMovies = Repository.GetAllMovies();

        var result = sourceMovies
            .Skip(5);

        PrintAll(result);
    }
    
    /// <summary>
    /// Skip the last X items from a source
    /// </summary>
    void SkipLastItems()
    {
        var sourceMovies = Repository.GetAllMovies();

        var result = sourceMovies
            .SkipLast(5);
        
        PrintAll(result);
    }
    
    /// <summary>
    /// Skip items from a source while a condition is true
    /// </summary>
    void SkipMatchingItems()
    {
        var sourceMovies = Repository.GetAllMovies();

        var result = sourceMovies
            .SkipWhile(movie => movie.ReleaseDate.Year < 2000);

        PrintAll(result);
    }
    
    /// <summary>
    /// Combining Skip & Take to get a chunk of items from a source
    /// </summary>
    void GetChunkUsingSkipAndTake()
    {
        var sourceMovies = Repository.GetAllMovies();

        var result = sourceMovies
            .Skip(5)
            .Take(10);

        PrintAll(result);
    }
}