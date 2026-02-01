using System.Runtime.InteropServices;
using Dometrain.Linq.Cmd.FilteringAndOrdering;
using Dometrain.Linq.Data.Models;

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

        var result =
            from movie in sourceMovies
            group movie by movie.Phase
            into phase
            where phase.Key > 2
            select phase;
        
        foreach (IGrouping<int, string> phase in result)
        {
            foreach (string title in phase)
            {
                Print(title);
            }
        } 
    }
    
    /// <summary>
    /// Grouped query results, fluent syntax
    /// </summary>
    private void GroupedResults_F()
    {
        var sourceMovies = Repository.GetAllMovies();

        var result = sourceMovies
            .GroupBy(movie => movie.Phase)
            .Where(phase => phase.Key > 2);
        
        foreach (IGrouping<int, string> phase in result)
        {
            foreach (string title in phase)
            {
                Print(title);
            }
        }

    }
}