using AutoMapper;
using GCT.Contracts.Data;
using GCT.Contracts.DTO;
using MediatR;

namespace GCT.Core.Handlers.Queries
{
    public class GetAllAccountsQuery : IRequest<IEnumerable<AccountDTO>>
    {
    }

    public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, IEnumerable<AccountDTO>>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetAllAccountsQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountDTO>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            var entities = await Task.FromResult(_repository.Accounts.GetAll());
            return _mapper.Map<IEnumerable<AccountDTO>>(entities);
        }
    }
}
