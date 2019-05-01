using System.Threading;
using System.Threading.Tasks;
using api;
using MediatR;

public static class Add
{
    public class Command : IRequest<int>
    {

    }

    public class Handler : IRequestHandler<Command, int>
    {
        private readonly RoomMateDbContext _dbContext;

        public Handler (RoomMateDbContext dbContext) => _dbContext = dbContext;

        public async Task<int> Handle (Command request, CancellationToken cancellationToken)
        {
            RoomMate roomMate = null;
            await _dbContext.RoomMates.AddAsync (roomMate, cancellationToken).ConfigureAwait (false);
            await _dbContext.SaveChangesAsync (cancellationToken).ConfigureAwait (false);
            return roomMate.Id;
        }
    }
}