namespace Dometrain.Linq.Cmd.PartialResults;

public class GroupedResults : QueryRunner
{
    public override void Run()
    {
        // GroupedResults_Q();
        GroupedResults_F();
    }

    /// <summary>
    /// Grouped query results, query syntax
    /// </summary>
    private void GroupedResults_Q()
    {
        var sourceMovies = Repository.GetAllMovies();

    }
    
    /// <summary>
    /// Grouped query results, fluent syntax
    /// </summary>
    private void GroupedResults_F()
    {
        var sourceMovies = Repository.GetAllMovies();

    }
}