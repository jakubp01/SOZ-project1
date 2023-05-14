using MediatR;
using Microsoft.EntityFrameworkCore;
using SOZ_project.Controllers.Core;
using SOZ_project.Enums;
using SOZ_project.Models;
using System.Security.Claims;

namespace SOZ_project.Queries
{
    public class GetReports
    {
        public class Query : IRequest<Result<List<ReportModel>>>
        {
           public int gender { get; set; }
            public Query(int gender)
            {
                this.gender = gender;
            }
        }

        public class Handler : IRequestHandler<Query, Result<List<ReportModel>>>
        {
            private readonly ReportsDbContext _context;
            private readonly IHttpContextAccessor _httpContext;
            public Handler(ReportsDbContext context, IHttpContextAccessor httpContext)
            {
                _context = context;
                _httpContext = httpContext;
            }

            public async Task<Result<List<ReportModel>>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<ReportModel> response;
                if(request.gender==1)
                {
                    response = await _context.Reports
                        .Where(x=>x.Gender=="Kobieta")
                        .ToListAsync(cancellationToken: cancellationToken);
                }
                else if(request.gender == 2)
                {
                    response = await _context.Reports
                        .Where(x => x.Gender == "Mężczyzna")
                        .ToListAsync(cancellationToken: cancellationToken);
                }
                else
                     response = await _context.Reports
                        .ToListAsync(cancellationToken: cancellationToken);
                    if (response != null)
                    {

                        return Result<List<ReportModel>>.Success(response);

                    }
                    return Result<List<ReportModel>>.Failure("error");
                
            }
        }
    }
}
