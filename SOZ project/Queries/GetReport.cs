using MediatR;
using Microsoft.EntityFrameworkCore;
using SOZ_project.Controllers.Core;
using SOZ_project.Models;

namespace SOZ_project.Queries
{
    public class GetReport
    {
        public class Query : IRequest<Result<ReportModel>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ReportModel>>
        {
            private readonly ReportsDbContext _context;
            public Handler(ReportsDbContext context)
            {
                _context = context;
            }

            public async Task<Result<ReportModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var response = await _context.Reports
                .FirstOrDefaultAsync(m => m.Id == request.Id);
                if (response != null)
                {

                    return Result<ReportModel>.Success(response);

                }
                return Result<ReportModel>.Failure("error");

            }
        }
    }
}
