using AutoMapper;
using GCT.Contracts.Data;
using GCT.Contracts.DTO;
using MediatR;

namespace GCT.Core.Handlers.Queries
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserDTO>>
    {
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDTO>>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var entities = await Task.FromResult(_repository.Users.GetAll());
            return _mapper.Map<IEnumerable<UserDTO>>(entities);
        }
    }
}
