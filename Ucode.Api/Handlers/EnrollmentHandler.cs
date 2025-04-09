using Microsoft.EntityFrameworkCore;
using Ucode.Api.Data;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Enrollments;
using Ucode.Core.Responses;

namespace Ucode.Api.Handlers
{
    public class EnrollmentHandler(AppDbContext context) : IEnrollmentHandler
    {

        public async Task<PagedResponse<List<Enrollment>>> GetAllAsync(GetAllEnrollmentRequest request)
        {
            try
            {
                var query = context
               .Enrollments
               .AsNoTracking()
               .Where(x => x.UserId == request.UserId);
            
                var enrollment = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Enrollment>>(enrollment, count, request.PageNumber, request.PageSize);

            }
            catch
            {
                return new PagedResponse<List<Enrollment>>(null, 500, "Não foi possível consultar as matriculas");
            }

        }
        public async Task<Response<Enrollment?>> GetByIdAsync(GetEnrollmentsByIdRequest request)
        {
            try
            {
                var enrollment = await context
               .Enrollments
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                return enrollment is null
                    ? new Response<Enrollment?>(null, 404, "Matricula não encontrada")
                    : new Response<Enrollment?>(enrollment);
            }
            catch
            {
                return new Response<Enrollment?>(null, 500, "Não foi possível encontrar Matrícula");
            }           
        }       
        public async Task<Response<Enrollment?>> CreateAsync(CreateEnrollmentRequest request)
        {
            try
            {
                var enrollment = new Enrollment
                {
                    UserId = request.UserId,
                    CourseId = request.CourseId,
                    StudentId = request.StudentId
                };

                await context.Enrollments.AddAsync(enrollment);
                await context.SaveChangesAsync();

                return new Response<Enrollment?>(enrollment, 201, "Matrícula criado com sucesso");
            }
            catch
            {
                return new Response<Enrollment?>(null, 500, "Não foi possível criar o matrícula");
            }
        }
        public async Task<Response<Enrollment?>> UpdateAsync(UpdateEnrollmentsRequest request)
        {
            try
            {
                var enrollment = await context
                .Enrollments
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (enrollment is null)
                    return new Response<Enrollment?>(null, 404, "Matrícula nao encontrado");

                enrollment.CourseId = request.CourseId;
                enrollment.StudentId = request.StudentId;
                enrollment.UpdatedAt = request.UpdatedAt;

                context.Enrollments.Update(enrollment);
                await context.SaveChangesAsync();

                return new Response<Enrollment?>(enrollment, message: "Matrícula atualizado com sucesso");
            }
            catch 
            {
                return new Response<Enrollment?>(null, 500, "Não foi possível alterar o Enrollment");
            }
        }
        public async Task<Response<Enrollment?>> DeleteAsync(DeleteEnrollmentsRequest request)
        {
            var enrollment = await context
                .Enrollments
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (enrollment is null)
                return new Response<Enrollment?>(null, 404, "Matricula não encontrada");
            context.Enrollments.Remove(enrollment);
            await context.SaveChangesAsync();

            return new Response<Enrollment?>(enrollment, 404, "Matricula excluida com sucesso");
        }

        

        

        
    }
}
