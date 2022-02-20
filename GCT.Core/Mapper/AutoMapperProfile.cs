using AutoMapper;
using GCT.Contracts.Data.Entities;
using GCT.Contracts.DTO;

namespace GCT.Core.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Transaction, TransactionDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<Account, AccountDTO>();
        }
    }
}
