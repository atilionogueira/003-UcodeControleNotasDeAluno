namespace Ucode.Api.Common.Api
{
    public static class AppExtension
    {
        // Configura o uso do Swagger e a interface do Swagger UI

        public static void ConfigureDevEnvironment(this WebApplication app) 
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API ControleAluno V1");
                c.RoutePrefix = string.Empty;
            });
            app.MapSwagger().RequireAuthorization();            
        }
        
        //Configuração da Autorização e Autenticação do Identity
        public static void UseSecurity(this WebApplication app) 
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
