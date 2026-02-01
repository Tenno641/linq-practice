namespace Dometrain.Linq.Data.Models;

public class Movie : IComparable<Movie>
{
    public required Guid MovieId { get; init; }
    public required string Name { get; init; }
    public required DateOnly ReleaseDate { get; init; }
    public int Phase { get; init; }
    public List<Person> Directors { get; set; } = [];
    public List<Person> Producers { get; set; } = [];

    public int CompareTo(Movie? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        
        if (ReferenceEquals(null, this)) return 1;
        if (ReferenceEquals(null, other)) return -1;

        if (ReleaseDate.Year > other.ReleaseDate.Year) return -1;
        if (ReleaseDate.Year < other.ReleaseDate.Year) return 1;

        return string.Compare(Name, other.Name, StringComparison.InvariantCultureIgnoreCase);
    }
    public override string ToString()
    {
        return $"[{MovieId}] {Name} ({ReleaseDate.Year}) - {string.Join(", ", Directors)}";
    }
}