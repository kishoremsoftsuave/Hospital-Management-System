using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class six2_enum_delete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW vw_Appointments AS
                SELECT
                    *,
                    CASE 
                        WHEN IsDeleted = 1 THEN 'true'
                        ELSE 'false'
                    END AS IsDeletedText
                FROM appointments;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW vw_Appointments;");
        }
    }
}
