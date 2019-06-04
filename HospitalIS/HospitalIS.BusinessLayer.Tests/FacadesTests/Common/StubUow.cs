using HospitalIS.Infrastructure.UnitOfWork_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalIS.BusinessLayer.Tests.FacadesTests.Common
{
    internal class StubUow : UnitOfWorkBase
    {
        protected override Task CommitCore()
        {
            return Task.Delay(15);
        }

        public override void Dispose()
        {
        }
    }
}
