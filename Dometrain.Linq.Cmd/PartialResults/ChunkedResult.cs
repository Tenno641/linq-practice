using Dometrain.Linq.Data.Models;

namespace Dometrain.Linq.Cmd.PartialResults;

public class ChunkedResult : QueryRunner
{
    public override void Run()
    {
        // SimpleChunks();
        ChunksWithNumbers();
    }

    /// <summary>
    /// Split the source query into equally sized chunks
    /// </summary>
    void SimpleChunks()
    {
        var sourceMovies = Repository.GetAllMovies();

        IEnumerable<Movie[]> result = sourceMovies
            .Chunk(5);

        foreach (Movie[] movies in result)
        {
            foreach (Movie movie in movies)
            {
                Console.WriteLine(movie);
            }
        }
    }
    
    /// <summary>
    /// Use the (item, index) => ... syntax to number the chunks
    /// </summary>
    void ChunksWithNumbers()
    {
        var sourceMovies = Repository.GetAllMovies();

        var result = sourceMovies
            .Chunk(5)
            .Select((chunk, index) => new { Movies = chunk, Index = index + 1 });

        foreach (var chunks in result)
        {
            Print($"Chunk {chunks.Index}");
            foreach (var movie in chunks.Movies)
            {
                Print(movie); 
            }
            Console.WriteLine("");
        }
    }
}