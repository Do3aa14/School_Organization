using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SchoolOrganization.ServiceLayer.Contract.GenericServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolOrganization.ServiceLayer.Persistence.GenericServices
{
   public class GenericService : IGenericService
    {
        
        protected readonly IMapper _mapper;
        protected readonly ILogger _logger;
        public GenericService(ILogger<GenericService> logger,IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
        }
    }
}
