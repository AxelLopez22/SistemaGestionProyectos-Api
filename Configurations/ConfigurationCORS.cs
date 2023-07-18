namespace Api_ProjectManagement.Configurations
{
    public static class ConfigurationCORS
    {
        public static void AddConfigureCORS(this IServiceCollection services,string MyAllowOrigins)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowOrigins,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                    });
            });
        }
    }
}
