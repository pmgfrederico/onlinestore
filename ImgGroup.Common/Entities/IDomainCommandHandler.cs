using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImgGroup.Common.Entities
{
    public interface IDomainCommandHandler<TCommand> where TCommand : IDomainCommand
    {
        Task HandleAsync(IDomainCommand cmd, CancellationToken ct);
    }
}
