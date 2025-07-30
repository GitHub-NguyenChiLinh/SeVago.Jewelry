public abstract class BaseRepository
{
    protected readonly BaseDbContext _context;
    public BaseRepository(BaseDbContext context)
    {
        _context = context;
    }
}