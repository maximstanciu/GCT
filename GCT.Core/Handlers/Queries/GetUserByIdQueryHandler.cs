using AutoMapper;
using GCT.Contracts.Data;
using GCT.Contracts.DTO;
using GCT.Core.Exceptions;
using MediatR;

namespace GCT.Core.Handlers.Queries
{
    public class GetUserByIdQuery : IRequest<UserDTO>
    {
        public int UserId { get; }
        public GetUserByIdQuery(int userId)
        {
            UserId = userId;
        }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDTO>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await Task.FromResult(_repository.Users.Get(request.UserId));

            if (user == null)
            {
                throw new EntityNotFoundException($"No User found for Id {request.UserId}");
            }

            return _mapper.Map<UserDTO>(user);
        }
    }
}
