using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarShopApi.Migrations
{
    public partial class SeeededDefaultUsersAndRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "23caf552-39b5-4464-a221-37d264ce204c", "4fb171d1-9a82-444c-8aa0-39339a3a2ccd", "Administrator", "ADMINISTRATOR" },
                    { "a890e379-b7c7-4b9b-a928-0e847f83f6a9", "dd661e7f-1990-40f1-a797-d3d6fafc3e7a", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "55b4f1ed-8f01-4fea-a62a-1aa8cd9b6187", 0, "a1e5ad31-3b9f-4dbc-843e-5be6930128ca", "admin@carshop.com", false, false, null, "System", "ADMIN@CARSHOP.COM", "ADMIN@CARSHOP.COM", "AQAAAAEAACcQAAAAECdKc4H5ZeqzJ4zfAtfi/FJcaSVWpdWw0Wj+ReOw0KqeCMmVIm0jCM2eG8bXVtvW+Q==", null, false, "e70b68bf-d451-4723-a919-8d3da555887f", "Admin", false, "admin@carshop.com" },
                    { "d828ec99-761d-40a2-bae6-53e38cd37d5c", 0, "46cea5a7-c6f0-4302-b6c3-a46a7adba207", "user@carshop.com", false, false, null, "System", "USER@CARSHOP.COM", "USER@CARSHOP.COM", "AQAAAAEAACcQAAAAEEAUWarJLyGq3h/5xfEpib0MVbRF2j8GtBGLGjrZzzh5FCY/xK8K8CmE45nVxwcAiQ==", null, false, "59581cea-90ec-480e-bd40-f6b6f8a4567d", "User", false, "user@carshop.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "23caf552-39b5-4464-a221-37d264ce204c", "55b4f1ed-8f01-4fea-a62a-1aa8cd9b6187" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a890e379-b7c7-4b9b-a928-0e847f83f6a9", "d828ec99-761d-40a2-bae6-53e38cd37d5c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "23caf552-39b5-4464-a221-37d264ce204c", "55b4f1ed-8f01-4fea-a62a-1aa8cd9b6187" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a890e379-b7c7-4b9b-a928-0e847f83f6a9", "d828ec99-761d-40a2-bae6-53e38cd37d5c" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23caf552-39b5-4464-a221-37d264ce204c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a890e379-b7c7-4b9b-a928-0e847f83f6a9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "55b4f1ed-8f01-4fea-a62a-1aa8cd9b6187");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d828ec99-761d-40a2-bae6-53e38cd37d5c");
        }
    }
}
