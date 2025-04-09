using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Ucode.Api.Data;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Grade;
using Ucode.Core.Responses;

namespace Ucode.Api.Handlers
{
    public class GradeHandler(AppDbContext context) : IGradeHandler
    {
        public async Task<PagedResponse<List<Grade>>> GetAllAsync(GetAllGradeRequest request)
        {
            var query = context
                .Grades
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId);

            var grades = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<Grade>>(
                 grades,
                 count,
                 request.PageNumber,
                 request.PageSize);
        }

        public async Task<Response<Grade?>> GetByIdAsync(GetGradeByIdRequest request)
        {
            try
            {
                var grade = await context
                .Grades
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                return grade is null
                    ? new Response<Grade?>(null, 404, "Grade não encontrado")
                    : new Response<Grade?>(grade);

            }
            catch
            {
                return new Response<Grade?>(null, 500, "Não foi possível recuperar o grade");
            }
        }
        public async Task<Response<Grade?>> CreateAsync(CreateGradeRequest request)
        {
            try
            {
                var grade = new Grade
                {
                    UserId = request.UserId,
                    EnrollmentId = request.EnrollmentId,                
                    Value = request.Value
                };
                await context.Grades.AddAsync(grade);
                await context.SaveChangesAsync();

                return new Response<Grade?>(grade, 201, "Grade criado com sucesso");

            }
            catch
            {
                return new Response<Grade?>(null, 500, "Não foi possível criar Grade");
            }
        }
       
        public async Task<Response<Grade?>> UpdateAsync(UpdateGradeRequest request)
        {                       
            try
            {
                var grade = await context
                .Grades
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (grade is null)
                    return new Response<Grade?>(null, 404, "Grade não encontrado");


                grade.EnrollmentId = request.EnrollmentId;
                grade.Value = request.Value;
                grade.UpdatedAt = DateTime.Now;

                context.Grades.Update(grade);
                await context.SaveChangesAsync();

                return new Response<Grade?>(grade);

            }
            catch
            {
                return new Response<Grade?>(null, 500, "Não foi possível recuperar o grade");
            }                        
            
        }      

        public async Task<Response<Grade?>> DeleteAsync(DeleteGradeRequest request)
        {
            try
            {
                var grade = await context
               .Grades
               .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (grade is null)
                    return new Response<Grade?>(null, 404, "Grade não Encontrado");

                context.Grades.Remove(grade);
                await context.SaveChangesAsync();

                return new Response<Grade?>(grade);

            }
            catch
            {               
                return new Response<Grade?>(null,500,"Não foi possível excluir o grade");
            }
        }

        
    }        
}
