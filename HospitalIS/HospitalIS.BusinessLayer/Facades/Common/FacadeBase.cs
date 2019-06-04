using HospitalIS.Infrastructure.UnitOfWork_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalIS.BusinessLayer.Facades.Common
{
    public abstract class FacadeBase
    {
        protected readonly IUnitOfWorkProvider UnitOfWorkProvider;

        protected FacadeBase(IUnitOfWorkProvider unitOfWorkProvider)
        {
            UnitOfWorkProvider = unitOfWorkProvider;
        }
    }
}
