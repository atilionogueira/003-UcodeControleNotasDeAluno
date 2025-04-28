using Microsoft.EntityFrameworkCore;
using Ucode.Api.Data;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Students;
using Ucode.Core.Responses;

namespace Ucode.Api.Handlers
{
    public class StudentHandler(AppDbContext context) : IStudentHandler
    {

        public async Task<PagedResponse<List<Student>>> GetAllAsync(GetAllStudentRequest request)
        {
            try
            {
                var query = context
               .Students
               .AsNoTracking()
               .Where(x => x.UserId == request.UserId)
               .OrderBy(x => x.Name);

                var student = await query                            
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Student>>(student,count,request.PageNumber,request.PageSize);   

            }
            catch
            {
                return new PagedResponse<List<Student>>(null, 500, "Não foi possível consultar os estudantes");               
            }            

        }

        public async Task<Response<Student?>> GetByIdAsync(GetStudentByIdRequest request)
        {
            try
            {
                var student = await context
                .Students
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                return student is null
                    ? new Response<Student?>(null, 404, message: "Student nao encontrado")
                    : new Response<Student?>(student);
            }
            catch 
            {
                return new Response<Student?>(null, 500, message: "Não foi possível encontrar Student"); 
            }

        }
        public async Task<Response<Student?>> CreateAsync(CreateStudentRequest request)
        {
            try
            {
                var student = new Student
                {
                    UserId = request.UserId,
                    Name = request.Name,
                    Email = request.Email,
                    BirthDate = request.BirthDate,
                    Gender = request.Gender                 
                };

                await context.Students.AddAsync(student);
                await context.SaveChangesAsync();

                return new Response<Student?>(student, 201,"Estudante criado com sucesso");
            }
            catch
            {
                return new Response<Student?>(null, 500, "Não foi possível criar o estudante");
            }

        }
        public async Task<Response<Student?>> UpdateAsync(UpdateStudentRequest request)
        {
            try
            {
                var student = await context
                .Students
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (student is null)
                    return new Response<Student?>(null, 404, "Student não encontrado");

                student.Name = request.Name;
                student.Email = request.Email;
                student.BirthDate = request.BirthDate;
                student.Gender = request.Gender;
                student.UpdatedAt = DateTime.Now;

                context.Students.Update(student);
                await context.SaveChangesAsync();

                return new Response<Student?>(student, message: "Student atualizado com sucesso");
            }
            catch 
            {
                return new Response<Student?>(null,500,"Nao foi possível alterar o Student");
            }
                    
        }
        public async Task<Response<Student?>> DeleteAsync(DeleteStudentRequest request)
        {
            try
            {
                var student = await context
                    .Students
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (student is null)
                    return new Response<Student?>(null, 404, "Student não encontrado");

                context.Students.Remove(student);
                await context.SaveChangesAsync();

                return new Response<Student?>(student, 404, "Student excluido com sucesso");
            }
            catch
            {
                return new Response<Student?>(null, 500, message: "Não foi possível excluir o student");
            }
        }

    }
}
