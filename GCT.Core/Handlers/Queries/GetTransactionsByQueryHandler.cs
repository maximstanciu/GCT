using AutoMapper;
using GCT.Contracts.Data;
using GCT.Contracts.DTO;
using MediatR;

namespace GCT.Core.Handlers.Queries
{

    public class GetTransactionsByQuery : IRequest<IEnumerable<TransactionDTO>>
    {
        public int? AccountId { get; set; }
        public int? UserId { get; set; }
        public GetTransactionsByQuery(int? accountId, int? userId)
        {
            AccountId = accountId;
            UserId = userId;
        }
    }

    public class GetTransactionsByQueryHandler : IRequestHandler<GetTransactionsByQuery, IEnumerable<TransactionDTO>>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetTransactionsByQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransactionDTO>> Handle(GetTransactionsByQuery request, CancellationToken cancellationToken)
        {
            var result = _repository.Transactions.GetAll();

            if (request.AccountId.HasValue)
                result = result.Where(t => t.AccountId == request.AccountId);

            if(request.UserId.HasValue)
                result = result.Where(t=>t.UserId == request.UserId);

            var entities = await Task.FromResult(result);

            return _mapper.Map<IEnumerable<TransactionDTO>>(entities);
        }
    }

}
