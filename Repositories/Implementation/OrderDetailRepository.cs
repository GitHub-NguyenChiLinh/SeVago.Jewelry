using Repositories.Interfaces;

namespace Repositories.Implementation {
    public class OrderDetailRepository : BaseRepository, IOrderDetailRepository {

        public OrderDetailRepository(BaseDbContext context) : base(context) { }
        
    }
}