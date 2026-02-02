namespace Dometrain.Linq.Cmd.CombiningResults;

public class JoinCollectionsFromResults : QueryRunner
{
    public override void Run()
    {
        // SelectManyFromProperty_Q();
        // SelectManyFromProperty_F();
        // SelectManyWithProjection_Q();
        SelectManyWithProjection_F();
    }

    /// <summary>
    /// Get all the items from a collection property of the model
    /// and append them into a single collection, query syntax.
    /// </summary>
    void SelectManyFromProperty_Q()
    {
        var allMovies = Repository.GetAllMovies();

        var result =
            (from movie in allMovies
            from director in movie.Directors
            orderby director.FirstName descending
            select director).Distinct();
        
        PrintAll(result);
    }
    
    /// <summary>
    /// Get all the items from a collection property of the model
    /// and append them into a single collection, fluent syntax.
    /// </summary>
    void SelectManyFromProperty_F()
    {
        var allMovies = Repository.GetAllMovies();

        var result = allMovies
            .SelectMany(movie => movie.Directors)
            .OrderByDescending(movie => movie.FirstName)
            .Distinct();
        
        PrintAll(result);
    }
    
    /// <summary>
    /// With SelectMany, it is possible to project both the source and the child item
    /// into a new model for the resulting sequence, query syntax
    /// </summary>
    void SelectManyWithProjection_Q()
    {
        var allMovies = Repository.GetAllMovies();

        var result =
            from movie in allMovies
            from director in movie.Directors
            select new
            {
                Movie = movie,
                Director = director
            };
        
        PrintAll(result);
    }
    
    /// <summary>
    /// With SelectMany, it is possible to project both the source and the child item
    /// into a new model for the resulting sequence, fluent syntax
    /// </summary>
    void SelectManyWithProjection_F()
    {
        var allMovies = Repository.GetAllMovies();

        var result = allMovies
            .SelectMany(movie => movie.Directors, (movie, director) => 
                new
                {
                    Movie = movie,
                    Director = director
                });
    }
}